using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ڹ����������UI����
/// </summary>
public class TitleSystem : MonoBehaviour
{
    private bool canChoose = true;
    public Animator animator;//�����������
    public GameObject animatorLoading;//��������Ԥ�Ƽ�
    private int cursor = 0;//��ǰ���λ��
    private bool nextScene = false;//�Ƿ������һ������
    //0��Xin��������Ա������     1����ʼ�µ���Ϸ    2����Ϸ����      3��������Ϸ      4���˳�������Ϸ
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
        if (!nextScene && canChoose)//�ڵ�ǰ���� �¶���Ҳ����ļ��
        {
            if (Input.GetButtonDown("Up"))//ѡ������
            {
                animator.SetTrigger("up");
                cursor--;
                if (cursor < 0) { cursor = 4; }

            }
            else if (Input.GetButtonDown("Down"))//ѡ������
            {
                animator.SetTrigger("down");
                cursor++;
                if (cursor > 4) { cursor = 0; }
            }
            else if (Input.GetButtonDown("Submit"))//ȷ��
            {
                switch (cursor)
                {
                    case 0://��ȡ�浵
                        toLoadGame();
                        break;
                    case 1://��ʼ�µ���Ϸ
                        toNewGame();
                        break;
                    case 2://��Ϸ����
                        toSettings();
                        break;
                    case 3://�ع˾������������
                        toMemory();
                        break;
                    case 4://�˳���Ϸ
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
    /// ǰ����ȡ�浵����
    /// </summary>
    private void toLoadGame()
    {
        canChoose = false;
        background.SetActive(true);
        saveCanvas.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// ��ʼ�µ���Ϸ
    /// </summary>
    private void toNewGame()
    {
        nextScene = true;
        Debug.Log("��ʼ���س���");
        LoadingScript.Scene = 7;//����ת�볡��������ֵ
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }
    /// <summary>
    /// ǰ����Ϸ���û���
    /// </summary>
    private void toSettings()
    {
        canChoose = false;
        settingsCanvas.SetActive(true);
        background.SetActive(true);
        startTitleCanvas.enabled = false;
    }
    /// <summary>
    /// ǰ�����������ͻ���
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
