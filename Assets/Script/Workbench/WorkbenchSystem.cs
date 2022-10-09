using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchSystem : MonoBehaviour
{
    #region 管理类属性
    private int[][] itemStore = new int[6][];//背包物品id列
    private int[][] formulaList = new int[3][];//合成配方表
    private GameObject[] itemButtons = new GameObject[6];//背包按钮组
    private GameObject[] formulaButtons = new GameObject[9];//配方按钮组
    public TMP_Text textDesccription;//物品描述文本
    #endregion 管理类属性
    #region 标记性变量
    private int pageItem = 0;//背包页
    private int pageFormula = 0;//配方页
    private int cursor = 1;//光标位置 >0为背包物品栏  <0为配方物品栏
    #endregion 标记性变量


    #region 按钮激活函数
    #region 背包按钮
    public void UpPage()//背包上一页
    {
        if (pageItem > 0) { pageItem--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void DownPage()//背包下一页
    {
        if (pageItem < 2) { pageItem++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void ActiveButtonStore0()//激活按钮0
    {
        cursor = 1;
        UpdateDescription();
    }
    public void ActiveButtonStore1()//激活按钮1
    {
        cursor = 2;
        UpdateDescription();
    }
    public void ActiveButtonStore2()//激活按钮2
    {
        cursor = 3;
        UpdateDescription();
    }
    public void ActiveButtonStore3()//激活按钮3
    {
        cursor = 4;
        UpdateDescription();
    }
    #endregion 背包按钮
    #region 配方按钮
    public void ActiveButtonFormula0()//激活按钮0
    {
        cursor = -1;
        UpdateDescription();
    }
    public void ActiveButtonFormula1()//激活按钮1
    {
        cursor = -2;
        UpdateDescription();
    }
    public void ActiveButtonFormula2()//激活按钮2
    {
        cursor = -3;
        UpdateDescription();
    }
    public void ActiveButtonFormula3()//激活按钮3
    {
        cursor = -4;
        UpdateDescription();
    }
    public void ActiveButtonFormula4()//激活按钮4
    {
        cursor = -5;
        UpdateDescription();
    }
    public void ActiveButtonFormula5()//激活按钮5
    {
        cursor = -6;
        UpdateDescription();
    }
    public void LeftPage()//配方上一页
    {
        if (pageFormula > 0) { pageFormula--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void RightPage()//配方下一页
    {
        if (pageFormula < 2) { pageFormula++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void Make()//制作物品
    {
        if(cursor < 0)
        {
            
            int id = formulaList[pageFormula][-cursor + -1];//合成物品的id
            if(ItemSystem.MakeFormula(id,StoreSystem.IdAll()))//判定是否可合成
            {
                Debug.Log("合成");
                int[] fomula = ItemSystem.FormulaItem(id);//获取所需原料的id表
                for(int i=0;i<fomula.Length;i++)
                {
                    int fomulaId = fomula[i];//当前遍历原料的id
                    StoreSystem.Remove(fomulaId);
                }
                StoreSystem.Add(id);
                UpdateFormItemStore();//更新合成台中的背包物品栏
            }
        }
        UpdateSprite();
        UpdateDescription();
    }
    #endregion 配方按钮

    #endregion 按钮激活函数
    #region 配置函数
    public void ToActive()//激活工作台界面
    {
        GameObject.Find("WorkbenchCanvas").SetActive(true);
    }
    public void UnActive()//退出工作台界面
    {
        GameObject.Find("WorkbenchCanvas").SetActive(false);
    }
    public void UpdateDescription()//更新描述
    {
        if(cursor<0)//配方描述
        {
            textDesccription.text = "<b>" + ItemSystem.ItemName(formulaList[pageFormula][-cursor+-1]) + "</b> "
            + ItemSystem.ItemDescription(formulaList[pageFormula][-cursor-1]);
        }
        else if(cursor>0)//背包物品描述
        {
            textDesccription.text = "<b>" + ItemSystem.ItemName(itemStore[pageItem][cursor-1]) + "</b> "
            + ItemSystem.ItemDescription(itemStore[pageItem][cursor-1]);
        }
    }
    public void UpdateSprite()//更新贴图
    {
        for (int i = 0; i < 4; i++)//更新背包物品贴图
        {
            itemButtons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = ItemSystem.ItemSprite(itemStore[pageItem][i]);
        }
        for (int i = 0; i < 6; i++)//更新配方物品贴图
        {
            formulaButtons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = ItemSystem.ItemSprite(formulaList[pageFormula][i]);
        }
    }
    public void UpdateFormItemStore()//从背包中更新物品列表
    {
        int[] store = StoreSystem.IdAll();
        for (int i = 0; i < store.Length; i++)
        {
            itemStore[i / 4][i % 4] = store[i];//读取
        }
    }
    #endregion 配置函数

    private void Awake()/*******/
    {
        #region 初始化数组
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
        #endregion 初始化数组
        #region 匹配按钮
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
        #endregion 匹配按钮
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
