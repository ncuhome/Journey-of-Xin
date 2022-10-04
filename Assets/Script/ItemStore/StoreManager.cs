using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//背包管理类
public class StoreManager : MonoBehaviour
{
    #region 管理类属性
    public static StoreManager Instance;
    private int[][] store = new int[][] {new int[8] , new int[8] , new int[8] };//物品id组
    private GameObject[] buttons = new GameObject[10];//按钮组
    private TMP_Text textDesccription;//物品描述文本
    public GameObject description;
    public ItemSystem itemSystem = new ItemSystem();//所引用的物品系统
    public GameObject check;//高亮框

    #endregion  管理类属性

    #region 标记性变量
    private int cursor = 0;//当前光标位置 (0~7)
    private int page = 0;//当前页码(0~2)
    private bool moving = false;//是否处于运动状态
    #endregion 标记性变量

    #region 背包物品与UI管理
    public int[] IdAll()//获取背包内物品的id组
    {
        int[] storeAll = new int[24];
        for(int i=0;i<24;i++)
        {
            storeAll[i] = store[i / 8][i % 8];
        }
        return storeAll;
    }
    public void SetStore(int[] idList)//设置背包物品
    {
        for(int i=0;i < idList.Length && i <24;i++)
        {
            store[i / 8][i % 8] = idList[i];
        }
    }
    public void UpdateSprite()//更新物品栏的贴图
    {
        for(int i=0;i<8;i++)
        {
            buttons[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite
                = itemSystem.ItemSprite(store[page][i]);
        }
    }
    public void UpdateDescription()//更新显示的物品描述
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
    #endregion 背包物品与UI管理

    #region 按钮激活函数
    public void LastPage()//上一页
    {
        if (page > 0) { page--; }
        UpdateSprite();
        UpdateDescription();
    }
    public void NextPage()//下一页
    {
        if (page < 2) { page++; }
        UpdateSprite();
        UpdateDescription();
    }
    public void ActiveButton0()//激活按钮0
    {
        cursor = 0;
        UpdateDescription();
    }
    public void ActiveButton1()//激活按钮1
    {
        cursor = 1;
        UpdateDescription();
    }
    public void ActiveButton2()//激活按钮2
    {
        cursor = 2;
        UpdateDescription();
    }
    public void ActiveButton3()//激活按钮3
    {
        cursor = 3;
        UpdateDescription();
    }
    public void ActiveButton4()//激活按钮4
    {
        cursor = 4;
        UpdateDescription();
    }
    public void ActiveButton5()//激活按钮5
    {
        cursor = 5;
        UpdateDescription();
    }
    public void ActiveButton6()//激活按钮6
    {
        cursor = 6;
        UpdateDescription();
    }
    public void ActiveButton7()//激活按钮7
    {
        cursor = 7;
        UpdateDescription();
    }
    #endregion 按钮激活函数
    private Vector3 AimPosition()//高亮框目标位置
    {
        return new Vector3((435 + (cursor % 4) * 350), (840 - (cursor / 4) * 325), 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        #region 将场景内的按钮载入管理类

        for (int i=0;i<8;i++)
        {
            buttons[i] = GameObject.Find("Item"+i);
        }
        buttons[8] = GameObject.Find("LastPage");
        buttons[9] = GameObject.Find("NextPage");

        #endregion 将场景内的按钮载入管理类
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
