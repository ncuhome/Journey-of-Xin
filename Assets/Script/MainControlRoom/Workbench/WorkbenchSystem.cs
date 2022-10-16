using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchSystem : MonoBehaviour
{
    #region ����������
    private int[][] itemStore = new int[6][];//������Ʒid��
    private int[][] formulaList = new int[3][];//�ϳ��䷽��
    private GameObject[] itemButtons = new GameObject[6];//������ť��
    private GameObject[] formulaButtons = new GameObject[9];//�䷽��ť��
    public TMP_Text textDesccription;//��Ʒ�����ı�
    #endregion ����������
    #region ����Ա���
    private int pageItem = 0;//����ҳ
    private int pageFormula = 0;//�䷽ҳ
    private int cursor = 1;//���λ�� >0Ϊ������Ʒ��  <0Ϊ�䷽��Ʒ��
    #endregion ����Ա���


    #region ��ť�����
    #region ������ť
    public void UpPage()//������һҳ
    {
        if (pageItem > 0) { pageItem--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void DownPage()//������һҳ
    {
        if (pageItem < 2) { pageItem++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void ActiveButtonStore0()//���ť0
    {
        cursor = 1;
        UpdateDescription();
    }
    public void ActiveButtonStore1()//���ť1
    {
        cursor = 2;
        UpdateDescription();
    }
    public void ActiveButtonStore2()//���ť2
    {
        cursor = 3;
        UpdateDescription();
    }
    public void ActiveButtonStore3()//���ť3
    {
        cursor = 4;
        UpdateDescription();
    }
    #endregion ������ť
    #region �䷽��ť
    public void ActiveButtonFormula0()//���ť0
    {
        cursor = -1;
        UpdateDescription();
    }
    public void ActiveButtonFormula1()//���ť1
    {
        cursor = -2;
        UpdateDescription();
    }
    public void ActiveButtonFormula2()//���ť2
    {
        cursor = -3;
        UpdateDescription();
    }
    public void ActiveButtonFormula3()//���ť3
    {
        cursor = -4;
        UpdateDescription();
    }
    public void ActiveButtonFormula4()//���ť4
    {
        cursor = -5;
        UpdateDescription();
    }
    public void ActiveButtonFormula5()//���ť5
    {
        cursor = -6;
        UpdateDescription();
    }
    public void LeftPage()//�䷽��һҳ
    {
        if (pageFormula > 0) { pageFormula--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void RightPage()//�䷽��һҳ
    {
        if (pageFormula < 2) { pageFormula++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void Make()//������Ʒ
    {
        if(cursor < 0)
        {
            
            int id = formulaList[pageFormula][-cursor + -1];//�ϳ���Ʒ��id
            if(ItemSystem.MakeFormula(id,StoreSystem.IdAll()))//�ж��Ƿ�ɺϳ�
            {
                Debug.Log("�ϳ�");
                int[] fomula = ItemSystem.FormulaItem(id);//��ȡ����ԭ�ϵ�id��
                for(int i=0;i<fomula.Length;i++)
                {
                    int fomulaId = fomula[i];//��ǰ����ԭ�ϵ�id
                    StoreSystem.Remove(fomulaId);
                }
                StoreSystem.Add(id);
                UpdateFormItemStore();//���ºϳ�̨�еı�����Ʒ��
            }
        }
        UpdateSprite();
        UpdateDescription();
    }
    #endregion �䷽��ť

    #endregion ��ť�����
    #region ���ú���
    public void ToActive()//�����̨����
    {
        GameObject.Find("WorkbenchCanvas").SetActive(true);
    }
    public void UnActive()//�˳�����̨����
    {
        GameObject.Find("WorkbenchCanvas").SetActive(false);
    }
    public void UpdateDescription()//��������
    {
        if(cursor<0)//�䷽����
        {
            textDesccription.text = "<b>" + ItemSystem.ItemName(formulaList[pageFormula][-cursor+-1]) + "</b> "
            + ItemSystem.ItemDescription(formulaList[pageFormula][-cursor-1]);
        }
        else if(cursor>0)//������Ʒ����
        {
            textDesccription.text = "<b>" + ItemSystem.ItemName(itemStore[pageItem][cursor-1]) + "</b> "
            + ItemSystem.ItemDescription(itemStore[pageItem][cursor-1]);
        }
    }
    public void UpdateSprite()//������ͼ
    {
        for (int i = 0; i < 4; i++)//���±�����Ʒ��ͼ
        {
            itemButtons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = ItemSystem.ItemSprite(itemStore[pageItem][i]);
        }
        for (int i = 0; i < 6; i++)//�����䷽��Ʒ��ͼ
        {
            formulaButtons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = ItemSystem.ItemSprite(formulaList[pageFormula][i]);
        }
    }
    public void UpdateFormItemStore()//�ӱ����и�����Ʒ�б�
    {
        int[] store = StoreSystem.IdAll();
        for (int i = 0; i < store.Length; i++)
        {
            itemStore[i / 4][i % 4] = store[i];//��ȡ
        }
    }
    #endregion ���ú���

    private void Awake()/*******/
    {
        #region ��ʼ������
        for (int i = 0; i < itemStore.Length; i++)
        {
            itemStore[i] = new int[4];
        }
        for (int i = 0; i < formulaList.Length; i++)
        {
            formulaList[i] = new int[6];
        }
        /********/
        StoreSystem.Add(1);
        /********/
        UpdateFormItemStore();
        #endregion ��ʼ������
        #region ƥ�䰴ť
        for (int i=0;i<4;i++)
        {
            itemButtons[i] = GameObject.Find("Item"+i);
        }
        itemButtons[4] = GameObject.Find("UpPage");
        itemButtons[5] = GameObject.Find("DownPage");
        for(int i=0;i<6;i++)
        {
            formulaButtons[i] = GameObject.Find("Formula" + i);
        }
        formulaButtons[6] = GameObject.Find("LeftPage");
        formulaButtons[7] = GameObject.Find("RightPage");
        formulaButtons[8] = GameObject.Find("MakeButton");
        #endregion ƥ�䰴ť
        /***********/
        formulaList[0][0] = 2;
        /***********/
        UpdateDescription();
        UpdateSprite();
    }
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
