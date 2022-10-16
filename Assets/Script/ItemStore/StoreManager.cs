using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//����������
public class StoreManager : MonoBehaviour
{
    #region ����������
    private int[][] store = new int[][] {new int[8] , new int[8] , new int[8] };//��Ʒid��
    private GameObject[] buttons = new GameObject[10];//��ť��
    public TMP_Text textDesccription;//��Ʒ�����ı�
    public GameObject check;//������
    public GameObject managerSystem;

    #endregion  ����������

    #region ����Ա���
    private int cursor = 0;//��ǰ���λ�� (0~7)
    private int page = 0;//��ǰҳ��(0~2)
    private bool moving = false;//�Ƿ����˶�״̬
    #endregion ����Ա���

    #region ���ú���
    /// <summary>
    /// ������Ʒ������ͼ
    /// </summary>
    public void UpdateSprite()
    {
        for(int i=0;i<8;i++)
        {
            buttons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = ItemSystem.ItemSprite(store[page][i]);
        }
    }
    /// <summary>
    /// ������ʾ����Ʒ����
    /// </summary>
    public void UpdateDescription()
    {
        moving = true;
        textDesccription.text = "<b>"+ItemSystem.ItemName(store[page][cursor])+"</b> "
            + ItemSystem.ItemDescription(store[page][cursor]);
    }
    /// <summary>
    /// ���±�����Ʒ�б�
    /// </summary>
    public void UpdateStoreSystem()
    {
        int[] storeItem = StoreSystem.IdAll();
        for(int i=0;i<storeItem.Length;i++)
        {
            store[i / 8][i % 8] = storeItem[i];
        }
    }
    #endregion ���ú���

    #region ��ť�����
    public void LastPage()//��һҳ
    {
        if (page > 0) { page--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void NextPage()//��һҳ
    {
        if (page < 2) { page++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void ActiveButton0()//���ť0
    {
        cursor = 0;
        UpdateDescription();
    }
    public void ActiveButton1()//���ť1
    {
        cursor = 1;
        UpdateDescription();
    }
    public void ActiveButton2()//���ť2
    {
        cursor = 2;
        UpdateDescription();
    }
    public void ActiveButton3()//���ť3
    {
        cursor = 3;
        UpdateDescription();
    }
    public void ActiveButton4()//���ť4
    {
        cursor = 4;
        UpdateDescription();
    }
    public void ActiveButton5()//���ť5
    {
        cursor = 5;
        UpdateDescription();
    }
    public void ActiveButton6()//���ť6
    {
        cursor = 6;
        UpdateDescription();
    }
    public void ActiveButton7()//���ť7
    {
        cursor = 7;
        UpdateDescription();
    }
    public void UnActive()//�˳�����̨����
    {
        managerSystem.SetActive(false);
        gameObject?.SetActive(false);
    }

    #endregion ��ť�����

    private Vector3 AimPosition()//������Ŀ��λ��
    {
        return new Vector3((435 + (cursor % 4) * 350), (840 - (cursor / 4) * 325), 0);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        #region �������ڵİ�ť���������

        for (int i = 0; i < 8; i++)
        {
            buttons[i] = GameObject.Find("Item" + i);
        }
        buttons[8] = GameObject.Find("LastPage");
        buttons[9] = GameObject.Find("NextPage");

        #endregion �������ڵİ�ť���������
        /*****/
        StoreSystem.Add(1);
        StoreSystem.Add(2);
        /*****/
        UpdateStoreSystem();//���±�����Ʒ�б�;
    }

    private void OnEnable()
    {
        UpdateStoreSystem();//���±�����Ʒ�б�;
    }

    void Start()
    {
        UpdateSprite();
        UpdateDescription();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LastPage")) { LastPage(); }
        else if (Input.GetButtonDown("NextPage")) { NextPage(); }
        else if (Input.GetButtonDown("Cancel")) { UnActive(); }
        else if (moving)
        {
            check.transform.position += (AimPosition() - check.transform.position) * Time.deltaTime * 10;
            if ((AimPosition() - check.transform.position).magnitude < 10)
            {
                check.transform.position = AimPosition();
                moving = false;
            }
        }
    }
}
