using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    #region Save & Load

    //将数据转换为 Json 文件并存到 Unity 默认存档路径，名字为 saveFileName
    public static void SaveByJson(string saveFileName, object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.WriteAllText(path, json);

        #if UNITY_EDITOR
            Debug.Log($"Successfully saved data to {path}.");
        #endif
        }
        catch (System.Exception exception)
        {
        #if UNITY_EDITOR
            Debug.LogError($"Failed to save data to {path}. \n{exception}");
        #endif
        }
    }

    //从 Unity 默认存档路径读取名为 saveFileName 的存档并转化为数据返回
    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);

            return data;
        }
        catch (System.Exception exception)
        {
        #if UNITY_EDITOR
            Debug.LogError($"Failed to load data from {path}. \n{exception}");
        #endif

            return default;
        }
    }

    #endregion


    #region  Deleting

    // 删除 Unity 默认路径的名为 saveFileName 的存档
    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
        #if UNITY_EDITOR
            Debug.LogError($"Failed to delete {path}. \n{exception}");
        #endif
        }
    }

    #endregion
}
