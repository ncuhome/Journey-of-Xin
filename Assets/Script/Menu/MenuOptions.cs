using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{
    #region Properties
    public static MenuOptions Instance {get; private set;}
    public GameObject saveCanvas;
    public GameObject settingCanvas;
    public GameObject menuCanvas;
    public GameObject menuOptions;
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
            Debug.Log("有另外的实例");
            Destroy(this.gameObject);
        }
        // menuCanvas = GameObject.Find("Menu").gameObject;
        // saveCanvas = GameObject.Find("Save").transform.Find("SaveCanvas").gameObject;
        // settingCanvas = GameObject.Find("SettingsCanvas").gameObject;
        // menuOptions = menuCanvas.transform.Find("MenuOptions").gameObject;
        saveCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        menuCanvas.SetActive(false);
    }

    private void Start() 
    {

    }

    private void Update()
    {
        
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
        InputManager.Instance.sceneState = SceneState.MainScene;
        menuCanvas.SetActive(false);
    }

    public void OpenMenu()
    {
        InputManager.Instance.sceneState = SceneState.Menu;
        menuCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()//开始新的游戏
    {
        Debug.Log("开始加载场景");
        LoadingScript.Scene = 0;//设置转入场景的索引值
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }

    #endregion

    #region input

    public void InputDetect()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (InputManager.Instance.sceneState == SceneState.MainScene)
            {
                OpenMenu();
            }
            else
            {
                if (settingCanvas.activeInHierarchy || saveCanvas.activeInHierarchy)
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
}
