using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//背包管理类
public class StoreManager : MonoBehaviour
{
    #region 管理类属性
    public static StoreManager Instance {get; private set;}
    private int[][] store = new int[][] {new int[8] , new int[8] , new int[8] };//物品id组
    private GameObject[] buttons = new GameObject[10];//按钮组
    public TMP_Text textDesccription;//物品描述文本
    public GameObject check;//高亮框
    public GameObject managerSystem;

    #endregion  管理类属性

    #region 标记性变量
    private int cursor = 0;//当前光标位置 (0~7)
    private int page = 0;//当前页码(0~2)
    private bool moving = false;//是否处于运动状态
    #endregion 标记性变量

    #region 配置函数
    /// <summary>
    /// 更新物品栏的贴图
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
    /// 更新显示的物品描述
    /// </summary>
    public void UpdateDescription()
    {
        moving = true;
        textDesccription.text = "<b>"+ItemSystem.ItemName(store[page][cursor])+"</b> "
            + ItemSystem.ItemDescription(store[page][cursor]);
    }
    /// <summary>
    /// 更新背包物品列表
    /// </summary>
    public void UpdateStoreSystem()
    {
        int[] storeItem = StoreSystem.IdAll();
        for(int i=0;i<storeItem.Length;i++)
        {
            store[i / 8][i % 8] = storeItem[i];
        }
    }
    #endregion 配置函数

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
    public void UnActive()//退出工作台界面
    {
        // managerSystem.SetActive(false);
        // gameObject?.SetActive(false);
        ManagerSystem.Instance.HideItemStore();
    }

    #endregion 按钮激活函数

    private Vector3 AimPosition()//高亮框目标位置
    {
        return new Vector3((435 + (cursor % 4) * 350), (840 - (cursor / 4) * 325), 0);
    }

    // Start is called before the first frame update
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
        
        #region 将场景内的按钮载入管理类

        for (int i = 0; i < 8; i++)
        {
            buttons[i] = GameObject.Find("Item" + i);
        }
        buttons[8] = GameObject.Find("LastPage");
        buttons[9] = GameObject.Find("NextPage");

        #endregion 将场景内的按钮载入管理类
        /*****/
        StoreSystem.Add(3);
        StoreSystem.Add(3);
        StoreSystem.Add(4);
        StoreSystem.Add(4);
        /*****/
        UpdateStoreSystem();//更新背包物品列表;
    }

    private void OnEnable()
    {
        UpdateStoreSystem();//更新背包物品列表;
    }

    void Start()
    {
        UpdateSprite();
        UpdateDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputDetect()
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
