using System;
using System.Security.AccessControl;
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
    [System.Serializable] class SaveData
    {
        public List<int> staticEventList;
        public List<bool> canEnterDialog; 
        public int[] itemList = new int[24];
    }

    public string saveDataFileName = "SaveData1.sav";
    public List<bool> canEnterDialog;

    #endregion


    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < 100; i++)
        {
            canEnterDialog.Add(true);
        }
    }

    #endregion


    #region Save and Load

    public void Save(int saveIndex)
    {
        saveDataFileName = "SaveData"+ saveIndex.ToString() + ".sav";
        SaveByJson();
    }

    public void Load(int saveIndex)
    {
        saveDataFileName = "SaveData"+ saveIndex.ToString() + ".sav";
        LoadFromJson();
    }

    // 将数据存入 SaveData 类中（随需要存档的数据而修改
    private SaveData SavingData()
    {
        var saveData = new SaveData();

        saveData.staticEventList = EventSystem.Instance.staticEventList;
        saveData.canEnterDialog = canEnterDialog;
        saveData.itemList = StoreManager.Instance.IdAll();//已修改
        return saveData;
    }

    // 将数据从 SaveData 类中读取出来（随需要存档的数据而修改
    private void LoadingData(SaveData saveData)
    {
        EventSystem.Instance.staticEventList = saveData.staticEventList;
        canEnterDialog = saveData.canEnterDialog;
        StoreManager.Instance.SetStore(saveData.itemList);//已修改
    }

    public bool FindPath(int saveIndex)
    {
        var saveFileName =  "SaveData"+ saveIndex.ToString() + ".sav";
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
        SaveSystem.SaveByJson(saveDataFileName , SavingData());
    }

    //将 Json 文件转化成 SaveData 类并进行读取数据
    private void LoadFromJson()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(saveDataFileName);

        LoadingData(saveData);
    }

    #endregion

    #region  Deleting

    // 删除玩家设置（可以用于以后的设置保存
    // public static void DeletePlayerDataPrefs()
    // {
    //     PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
    // }

    // 删除指定名字的存档
    [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
    public void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(saveDataFileName);
    }

    #endregion
}
