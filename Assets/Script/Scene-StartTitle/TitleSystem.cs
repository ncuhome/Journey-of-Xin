using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于管理主界面的UI互动
/// </summary>
public class TitleSystem : MonoBehaviour
{
    private bool canChoose = true;
    public Animator animator;//动画播放组件
    public GameObject animatorLoading;//过场动画预制件
    private int cursor = 0;//当前光标位置
    private bool nextScene = false;//是否进入下一个场景
    //0：Xin（制作人员名单）     1：开始新的游戏    2：游戏设置      3：回忆游戏      4：退出返回游戏
    private GameObject settingsCanvas = null;
    private GameObject saveCanvas = null;
    public GameObject background = null;
    private Canvas startTitleCanvas = null;
    void Awake()
    {   
        settingsCanvas = GameObject.Find("SettingsCanvas");
        saveCanvas = GameObject.Find("Save");
        background = GameObject.Find("/Background");
        startTitleCanvas = GameObject.Find("StartTitleCanvas").GetComponent<Canvas>();
    }
    void Start()
    {
        settingsCanvas.SetActive(false);
        saveCanvas.SetActive(false);
        background.SetActive(false);
        startTitleCanvas.enabled = true;
    }

    void Update()
    {
        if (!nextScene && canChoose)//在当前场景 下对玩家操作的监控
        {
            if (Input.GetButtonDown("Up"))//选项向上
            {
                animator.SetTrigger("up");
                cursor--;
                if (cursor < 0) { cursor = 4; }

            }
            else if (Input.GetButtonDown("Down"))//选项向下
            {
                animator.SetTrigger("down");
                cursor++;
                if (cursor > 4) { cursor = 0; }
            }
            else if (Input.GetButtonDown("Submit"))//确认
            {
                switch (cursor)
                {
                    case 0://读取存档
                        toLoadGame();
                        break;
                    case 1://开始新的游戏
                        toNewGame();
                        break;
                    case 2://游戏设置
                        toSettings();
                        break;
                    case 3://回顾剧情和欣赏音乐
                        toMemory();
                        break;
                    case 4://退出游戏
                        Application.Quit();
                        break;
                }
            }
        }

        if (settingsCanvas.activeInHierarchy && Input.GetButtonDown("Cancel"))
        {
            ReturnFromSettings();
        }

        if (saveCanvas.activeInHierarchy && Input.GetButtonDown("Cancel"))
        {
            ReturnFromSave();
        }
    }

    /// <summary>
    /// 前往读取存档界面
    /// </summary>
    private void toLoadGame()
    {
        canChoose = false;
        background.SetActive(true);
        saveCanvas.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// 开始新的游戏
    /// </summary>
    private void toNewGame()
    {
        nextScene = true;
        Debug.Log("开始加载场景");
        LoadingScript.Scene = 7;//设置转入场景的索引值
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }
    /// <summary>
    /// 前往游戏设置画面
    /// </summary>
    private void toSettings()
    {
        canChoose = false;
        settingsCanvas.SetActive(true);
        background.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// 前往回忆与欣赏画面
    /// </summary>
    private void toMemory()
    {

    }

    private void ReturnFromSettings()
    {
        canChoose = true;
        settingsCanvas.SetActive(false);
        background.SetActive(false);
        startTitleCanvas.enabled = true;
    }

    private void ReturnFromSave()
    {
        canChoose = true;
        saveCanvas.SetActive(false);
        background.SetActive(false);
        startTitleCanvas.enabled = true;
    }

}
