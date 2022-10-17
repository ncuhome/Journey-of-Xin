using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveButton : MonoBehaviour
{
    #region Properties

    public GameObject confirmOptionNode;
    public TMP_Text confirmText;
    public TMP_Text saveSceneText;

    public int saveDataIndex;
    private bool waitForConfirm;
    private int effectIndex;
    private bool hasChoose;

    #endregion

    #region Unity Methods

    #endregion

    #region  Click

    public void SaveButtonClick()
    {
        if (waitForConfirm) 
        {
            return;
        }
        effectIndex = 1;
        WaitForConfirm();
    }

    public void LoadButtonClick()
    {
        if (waitForConfirm) 
        {
            return;
        }
        effectIndex = 2;
        WaitForConfirm();
    }

    public void DeleteButtonClick()
    {
        if (waitForConfirm) 
        {
            return;
        }
        effectIndex = 3;
        WaitForConfirm();
    }

    #endregion

    #region Confirm

    public void WaitForConfirm()
    {
        switch (effectIndex)
        {
            case 1:
                if (PlayerData.Instance.FindPath(saveDataIndex))
                {
                    confirmText.text = "您是否确认要覆盖记忆备份";
                } 
                else
                {
                    confirmText.text = "您是否确认要进行记忆备份";
                }
                break;
            case 2:
                if (PlayerData.Instance.FindPath(saveDataIndex))
                {
                    confirmText.text = "您是否确认要进行记忆下载";
                    break;
                }
                else 
                {
                    return;
                }
            case 3:
                if (PlayerData.Instance.FindPath(saveDataIndex))
                {
                    confirmText.text = "您是否确认要格式化记忆备份";
                    break;
                }
                else 
                {
                    return;
                }
        }
        confirmOptionNode.SetActive(true);
        waitForConfirm = true;
        hasChoose = false;
    }

    public void Confirm()
    {
        if (!waitForConfirm)
        {
            return;
        }
        switch (effectIndex)
        {
            case 1:
                PlayerData.Instance.Save(saveDataIndex);
                break;
            case 2:
                PlayerData.Instance.Load(saveDataIndex);
                break;
            case 3:
                PlayerData.Instance.saveDataFileName = "SaveData"+ saveDataIndex.ToString() + ".sav";
                PlayerData.Instance.DeletePlayerDataSaveFile();
                break;
        }
        confirmOptionNode.SetActive(false);
        waitForConfirm = false;
        hasChoose = true;
    }

    public void Refuse()
    {
        if (!waitForConfirm)
        {
            return;
        }
        confirmOptionNode.SetActive(false);
        waitForConfirm = false;
        hasChoose = true;
    }

    #endregion

    #region SaveScene

    public void UpdateSaveScene()
    {
        
    }

    #endregion
}
