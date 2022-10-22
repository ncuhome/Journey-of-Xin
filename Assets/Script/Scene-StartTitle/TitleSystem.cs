using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ?UI
/// </summary>
public class TitleSystem : MonoBehaviour
{
    private bool canChoose = true;
    public Animator animator;//
    public GameObject animatorLoading;//??
    private int cursor = 0;//??
    private bool nextScene = false;//??
    //0Xin?     1?µ?    2?      3?      4??
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
        StoreSystem.Clear();
        settingsCanvas.SetActive(false);
        saveCanvas.SetActive(false);
        background.SetActive(false);
        startTitleCanvas.enabled = true;
    }

    void Update()
    {
        if (!nextScene && canChoose)//?? ¶??
        {
            if (Input.GetButtonDown("Up"))//?
            {
                animator.SetTrigger("up");
                cursor--;
                if (cursor < 0) { cursor = 4; }

            }
            else if (Input.GetButtonDown("Down"))//?
            {
                animator.SetTrigger("down");
                cursor++;
                if (cursor > 4) { cursor = 0; }
            }
            else if (Input.GetButtonDown("Submit"))//?
            {
                switch (cursor)
                {
                    case 0://??
                        toLoadGame();
                        break;
                    case 1://?µ?
                        toNewGame();
                        break;
                    case 2://?
                        toSettings();
                        break;
                    case 3://??
                        toMemory();
                        break;
                    case 4://??
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
    /// ???
    /// </summary>
    private void toLoadGame()
    {
        canChoose = false;
        background.SetActive(true);
        saveCanvas.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// ?µ?
    /// </summary>
    private void toNewGame()
    {
        nextScene = true;
        Debug.Log("??????");
        LoadingScript.Scene = 3;//??
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }
    /// <summary>
    /// ??û
    /// </summary>
    private void toSettings()
    {
        canChoose = false;
        settingsCanvas.SetActive(true);
        background.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// ??
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