using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // 需要转化成存档的数据（随需要存档的数据而修改
    #region Fields

    [SerializeField] string playerName = "Player Name";
    [SerializeField] int level = 0;
    [SerializeField] int coin = 0;

    // 将所有的存档数据放入 SaveData 类中方便转化成 Json 文件
    [System.Serializable] class SaveData
    {
        public string playerName;
        public int playerLevel;
        public int playerCoin;
    }

    const string PLAYER_DATA_KEY = "PlayerData";
    const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";

    #endregion


    #region  Properties

    public string Name => playerName;
    public int Level => level;
    public int Coin => coin;

    #endregion


    #region Save and Load

    public void Save()
    {
        SaveByJson();
    }

    public void Load()
    {
        LoadFromJson();
    }

    // 将数据存入 SaveData 类中（随需要存档的数据而修改
    SaveData SavingData()
    {
        var saveData = new SaveData();

        saveData.playerName = playerName;
        saveData.playerLevel = level;
        saveData.playerCoin = coin;
        return saveData;
    }

    // 将数据从 SaveData 类中读取出来（随需要存档的数据而修改
    void LoadingData(SaveData saveData)
    {
        playerName = saveData.playerName;
        level = saveData.playerLevel;
        coin = saveData.playerCoin;
    }

    #endregion


    #region Json
    
    //将数据转换成 SaveData 类并进行存档
    void SaveByJson()
    {
        SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME , SavingData());
    }

    //将 Json 文件转化成 SaveData 类并进行读取数据
    void LoadFromJson()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);

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
    public static void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    }

    #endregion
}
