
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{

    // 需要转化成存档的数据（随需要存档的数据而修改
    #region Fields
    public static PlayerData Instance { get; private set; } // 单例模式

    // 将所有的存档数据放入 SaveData 类中方便转化成 Json 文件
    [System.Serializable]
    public class SaveData
    {
        public int[] staticEventList = new int[100];
        public bool[] canEnterDialog = new bool[100];
        public int[] itemList = new int[24];
        public ItemState[] itemStates = new ItemState[100];
        public int roomIndex;
        public int planetIndex;
        public bool timeRecordStart = false;
        public float targetTime = 0f;
        public int timeRoomIndex = 0;
        public int timePlanetIndex = 0;
        public int callbackIndex = 0;
        public bool needStay = false;
        public int ceState = 0;
    }
    public GameObject animatorLoading;//过场动画预制件

    public string saveDataFileName = "SaveData1.sav";

    #endregion


    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    #endregion


    #region Save and Load

    public void AutoSave()
    {
        saveDataFileName = "SaveData0.sav";
        SaveByJson();
    }

    public void Save(int saveIndex)
    {
        saveDataFileName = "SaveData" + saveIndex.ToString() + ".sav";
        SaveByJson();
    }

    public void Load(int saveIndex)
    {
        LoadingScript.Scene = 3;//设置转入场景的索引值
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
        saveDataFileName = "SaveData" + saveIndex.ToString() + ".sav";
        LoadFromJson();
        StartCoroutine("LoadingTimeManager", saveIndex);
    }

    // 将数据存入 SaveData 类中（随需要存档的数据而修改
    private SaveData SavingData()
    {
        var saveData = new SaveData();
        saveData.staticEventList = (int[])EventSystem.Instance.staticEventList.Clone();
        saveData.canEnterDialog = (bool[])DialogueSystem.Instance.canEnterDialog.Clone();
        saveData.itemList = StoreSystem.IdAll();//背包物品存入变更
        saveData.itemStates = (ItemState[])SceneItemManager.Instance.itemStates.Clone();
        saveData.roomIndex = RoomManager.Instance.roomIndex;
        saveData.planetIndex = RoomManager.Instance.planetIndex;

        saveData.timeRecordStart = TimeManager.Instance.timeRecordStart;
        saveData.targetTime = TimeManager.Instance.targetTime;
        saveData.timeRoomIndex = TimeManager.Instance.roomIndex;
        saveData.planetIndex = TimeManager.Instance.planetIndex;
        saveData.callbackIndex = TimeManager.Instance.callbackIndex;
        saveData.needStay = TimeManager.Instance.needStay;
        
        saveData.ceState = CeController.Instance.state;
        //saveData.itemList = StoreManager.Instance.IdAll();//已修改
        return saveData;
    }

    // 将数据从 SaveData 类中读取出来（随需要存档的数据而修改
    private IEnumerator LoadingData(SaveData saveData)
    {
        yield return new WaitForSeconds(4f);

        Debug.Log("Load");

        EventSystem.Instance.staticEventList = (int[])saveData.staticEventList.Clone();
        DialogueSystem.Instance.canEnterDialog = (bool[])saveData.canEnterDialog.Clone();
        StoreSystem.SetStore(saveData.itemList);//背包物品读取变更
        SceneItemManager.Instance.itemStates = (ItemState[])saveData.itemStates.Clone();
        RoomManager.Instance.roomIndex = saveData.roomIndex;
        RoomManager.Instance.planetIndex = saveData.planetIndex;

        TimeManager.Instance.time = 0;
        TimeManager.Instance.timeRecordStart = saveData.timeRecordStart;
        TimeManager.Instance.targetTime = saveData.targetTime;
        TimeManager.Instance.roomIndex = saveData.timeRoomIndex;
        TimeManager.Instance.planetIndex = saveData.planetIndex;
        TimeManager.Instance.callbackIndex = saveData.callbackIndex;
        TimeManager.Instance.needStay = saveData.needStay;
        TimeManager.Instance.callBack = TimeManager.GetCallback(saveData.callbackIndex);
        
        CeController.Instance.state = saveData.ceState;
        CeController.Instance.ControlCEs();

        SceneItemManager.Instance.FindItems();
        //StoreManager.Instance.SetStore(saveData.itemList);//已修改
    }

    private IEnumerator LoadingTimeManager(int saveIndex)
    {
        yield return new WaitForSeconds(4f);

    }

    public bool FindPath(int saveIndex)
    {
        var saveFileName = "SaveData" + saveIndex.ToString() + ".sav";
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }

    #endregion


    #region Json

    //将数据转换成 SaveData 类并进行存档
    private void SaveByJson()
    {
        SaveSystem.SaveByJson(saveDataFileName, SavingData());
    }

    //将 Json 文件转化成 SaveData 类并进行读取数据
    private void LoadFromJson()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(saveDataFileName);

        StartCoroutine("LoadingData", saveData);
    }

    #endregion

    #region  Deleting
    public void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(saveDataFileName);
    }

    #endregion
}
