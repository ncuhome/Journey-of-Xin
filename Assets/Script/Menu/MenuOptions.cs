using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{
    #region Properties

    private GameObject saveCanvas;
    private GameObject settingCanvas;
    private GameObject menuCanvas;
    private GameObject menuOptions;
    public Animator animatorLoading;
    private AsyncOperation operation;
    private bool nextScene = false;
    private float timer = 0;

    #endregion

    #region Unity Methods

    private void Start() {
        menuCanvas = GameObject.Find("Menu").gameObject;
        saveCanvas = menuCanvas.transform.Find("Save").Find("SaveCanvas").gameObject;
        settingCanvas = menuCanvas.transform.Find("SettingsCanvas").gameObject;
        menuOptions = menuCanvas.transform.Find("MenuOptions").gameObject;
        
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

        if(nextScene)
        {
            Debug.Log("场景加载进度：" + operation.progress);
            timer += Time.deltaTime;//计时器
            if (operation.progress == 0.9f && timer >= 3.0)//加载完毕后 且 动画播放完成 后跳转
            {
                Debug.Log("加载场景完毕");
                DontDestroyOnLoad(animatorLoading.gameObject);//加载新场景时不销毁过场动画物体
                animatorLoading.SetTrigger("nextScene");//播放过场消失动画
                operation.allowSceneActivation = true;//跳转至新场景
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
        nextScene = true;
        Debug.Log("开始加载场景");
        animatorLoading.SetTrigger("nextScene");//播放过场开始动画
        StartCoroutine(LoadScene());//使用异步加载场景
    }
    private IEnumerator LoadScene()//异步加载（使用协程）
    {
        operation = SceneManager.LoadSceneAsync("StartTitle");
        operation.allowSceneActivation = false;
        yield return operation;
    }

    #endregion
}
