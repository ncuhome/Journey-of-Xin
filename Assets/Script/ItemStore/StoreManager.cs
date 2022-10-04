using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//����������
public class StoreManager : MonoBehaviour
{
    #region ����������
    public static StoreManager Instance;
    private int[][] store = new int[][] {new int[8] , new int[8] , new int[8] };//��Ʒid��
    private GameObject[] buttons = new GameObject[10];//��ť��
    private TMP_Text textDesccription;//��Ʒ�����ı�
    public GameObject description;
    public ItemSystem itemSystem = new ItemSystem();//�����õ���Ʒϵͳ
    public GameObject check;//������

    #endregion  ����������

    #region ����Ա���
    private int cursor = 0;//��ǰ���λ�� (0~7)
    private int page = 0;//��ǰҳ��(0~2)
    private bool moving = false;//�Ƿ����˶�״̬
    #endregion ����Ա���

    #region ������Ʒ��UI����
    public int[] IdAll()//��ȡ��������Ʒ��id��
    {
        int[] storeAll = new int[24];
        for(int i=0;i<24;i++)
        {
            storeAll[i] = store[i / 8][i % 8];
        }
        return storeAll;
    }
    public void SetStore(int[] idList)//���ñ�����Ʒ
    {
        for(int i=0;i < idList.Length && i <24;i++)
        {
            store[i / 8][i % 8] = idList[i];
        }
    }
    public void UpdateSprite()//������Ʒ������ͼ
    {
        for(int i=0;i<8;i++)
        {
            buttons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = itemSystem.ItemSprite(store[page][i]);
        }
    }
    public void UpdateDescription()//������ʾ����Ʒ����
    {
        /*
        Debug.Log(store[page][0]+" "+
            store[page][1] + " " +
            store[page][2] + " " +
            store[page][3] + " " +
            store[page][4] + " " +
            store[page][5] + " " +
            store[page][6] + " " +
            store[page][7] + " " +cursor
                );*/
        moving = true;
        textDesccription.text = "<b>"+itemSystem.ItemName(store[page][cursor])+"</b> "+itemSystem.ItemDescription(store[page][cursor]);
    }
    #endregion ������Ʒ��UI����

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
    #endregion ��ť�����
    private Vector3 AimPosition()//������Ŀ��λ��
    {
        return new Vector3((435 + (cursor % 4) * 350), (840 - (cursor / 4) * 325), 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        #region �������ڵİ�ť���������

        for (int i=0;i<8;i++)
        {
            buttons[i] = GameObject.Find("Item"+i);
        }
        buttons[8] = GameObject.Find("LastPage");
        buttons[9] = GameObject.Find("NextPage");

        #endregion �������ڵİ�ť���������
        /*****/
        store[0][0] = 1;
        store[0][1] = 2;
        /*****/
        textDesccription = description.GetComponent<TMP_Text>();
        UpdateSprite();
        UpdateDescription();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LastPage")) { LastPage(); }
        else if(Input.GetButtonDown("NextPage")){ NextPage(); }
        else if(moving)
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
