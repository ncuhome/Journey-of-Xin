using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{
    #region Properties
    public static MenuOptions Instance {get; private set;}
    private GameObject saveCanvas;
    private GameObject settingCanvas;
    private GameObject menuCanvas;
    private GameObject menuOptions;
    public GameObject animatorLoading;//过场动画预制件

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
        menuCanvas = GameObject.Find("Menu").gameObject;
        saveCanvas = menuCanvas.transform.Find("Save").Find("SaveCanvas").gameObject;
        settingCanvas = menuCanvas.transform.Find("SettingsCanvas").gameObject;
        menuOptions = menuCanvas.transform.Find("MenuOptions").gameObject;
    }

    private void Start() 
    {
        saveCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        menuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (!menuCanvas.activeInHierarchy)
            {
                OpenMenu();
            }
            else
            {
                if (saveCanvas.activeInHierarchy || settingCanvas.activeInHierarchy)
                {
                    ReturnToMenu();
                }
                else
                {
                    ReturnToGame();
                }
            }
        }
    }


    #endregion

    #region Menu

    public void ShowSaveCanvas()
    {
        saveCanvas.SetActive(true);
        menuOptions.SetActive(false);
    }

    public void ShowSettingsCanvas()
    {
        settingCanvas.SetActive(true);
        menuOptions.SetActive(false);
    }

    public void ReturnToMenu()
    {
        saveCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void ReturnToGame()
    {
        menuCanvas.SetActive(false);
    }

    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()//开始新的游戏
    {
        Debug.Log("开始加载场景");
        LoadingScript.Scene = 8;//设置转入场景的索引值
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }

    #endregion
}
