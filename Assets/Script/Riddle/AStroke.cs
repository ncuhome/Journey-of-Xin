using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class AStroke : MonoBehaviour
{
    private float timer = 0;//计时器
    private int[] blockList = new int[45];//0:无色彩   1：彩色    2：空气墙   3：源色彩方块
    private GameObject[] gameObjectList = new GameObject[45];
    public GameObject ASCanvas;//画布
    public GameObject endImage;//终止提示框
    public GameObject text0;//失败提示
    public GameObject text1;//成功提示
    public GameObject progressBar;//进度条
    public GameObject textTime;//时间
    public GameObject blockOrigin;//源块动画块
    private int  index= 24;//当前彩色移动光标位置
    private bool start = false;//判定是否游戏开始
    private bool stop = false;
    #region 输入处理&核心逻辑


    private bool pick = false;
    public void MouseDown(float mx,float my)//鼠标按下判定
    {
        float ix = gameObjectList[index].transform.position.x;
        float iy = gameObjectList[index].transform.position.y;
        Debug.Log("鼠标位置：X:" +  + mx+"  Y:" + my +"彩块位置：X:" + ix + "  Y:" + iy);
        if (mx >= ix-100 && mx <= ix+100 && my >= iy-100 && my<=iy+100)
        {
            pick = true;
            Debug.Log("抓住！");
        }
        else
        {
            pick = false;
        }
    }
    public void MouseUp()//鼠标松开
    {
        pick = false;
        blockOrigin.transform.position = new Vector3(-1500, 0, -50);
    }
    public void MouseDrag(float mx, float my)//鼠标拖动
    {
        if (pick)
        {
            blockOrigin.transform.position = new Vector3(mx,my,-20);
            float ix = 0;
            float iy = 0;
            #region 判定左移
            if(index % 9 >0)
            {
                ix = gameObjectList[index - 1].transform.position.x;
                iy = gameObjectList[index - 1].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Left();
                    return;
                }
            }
            #endregion
            #region 判定右移
            if (index % 9 < 8)
            {
                ix = gameObjectList[index + 1].transform.position.x;
                iy = gameObjectList[index + 1].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Right();
                    return;
                }
            }
            #endregion
            #region 判定上移
            if(index / 9>0)
            {
                ix = gameObjectList[index - 9].transform.position.x;
                iy = gameObjectList[index - 9].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Up();
                    return;
                }
            }
            #endregion
            #region 判定下移
            if (index / 9 < 4)
            {

                ix = gameObjectList[index + 9].transform.position.x;
                iy = gameObjectList[index + 9].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Down();
                    return;
                }
            }
            #endregion
        }
    }
    private void Up()//向上移动源
    {
        start = true;
        if(index/9 > 0)//不在第一行且去路为空色彩
        {
            if(blockList[index - 9] == 0)
            {
                blockList[index] = 1;//当前位置改为彩色
                index -= 9;
                blockList[index] = 3;//移动彩色源
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Left()//向左移动源
    {
        start = true;
        if (index % 9 > 0)//不在第一行且去路为空色彩
        {
            if(blockList[index - 1] == 0)
            {
                blockList[index] = 1;//当前位置改为彩色
                index--;
                blockList[index] = 3;//移动彩色源
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Down()//向下移动源
    {
        start = true;
        if (index / 9 < 4)//不在第一行且去路为空色彩
        {
           if(blockList[index + 9] == 0)
           {
                blockList[index] = 1;//当前位置改为彩色
                index += 9;
                blockList[index] = 3;//移动彩色源
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Right()//向右移动源
    {
        start = true;
        if (index % 9 < 8)//不在第一行且去路为空色彩
        {
            if(blockList[index + 1] == 0)
            {
                blockList[index] = 1;//当前位置改为彩色
                index++;
                blockList[index] = 3;//移动彩色源
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    public void Cancel()//取消并退出
    {
        InputManager.Instance.sceneState = SceneState.MainScene;
        SceneManager.UnloadSceneAsync(9);
    }
    private void End()//游戏结束（失败）
    {
        EventSystem.Instance.ActiveEvent(33);
        stop = true;
        endImage.SetActive(true);
        text0.SetActive(true);
    }
    private void EndSuccess()//游戏结束（成功）
    {
        //EventSystem.changeStaticEvent(83,true);
        EventSystem.Instance.ActiveEvent(33);
        stop = true;
        endImage.SetActive(true);
        text1.SetActive(true);
    }
    private bool Next()//检测是否游戏可继续（未失败）
    {
        if (timer > 20) { return false; }//失败
        else
        {
            if(index / 9 > 0)//不在第一层时
            {
                if (blockList[index - 9] == 0)
                {
                    return true;
                }
            }
            if(index % 9 > 0)//不在最左侧时
            {
                if (blockList[index-1] == 0)
                {
                    return true;
                }
            }
            if(index / 9 < 4)//不在最下层
            {
                if (blockList[index+9] == 0)
                {
                    return true;
                }
            }
            if(index % 9 < 8)//不在最右边
            {
                if (blockList[index+1] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool Success()//检测游戏是否成功
    {
        for(int i = 0; i < blockList.Length;i++)
        {
            if (blockList[i] == 0)
            {
                return false;
            }
        }
        return true;
    }
    private void UpdateSprite()//更新贴图
    {
        for(int i=0;i< gameObjectList.Length;i++)
        {
            try { Animator animator = gameObjectList[i].GetComponent<Animator>(); animator.SetInteger("status", blockList[i]); }
            catch (Exception e) { Debug.Log("错误位置编号："+i); }
            
        }
    }
    private void ResetData()//重置数据
    {
        ASCanvas.SetActive(true);
        timer = 0;
        index = 24;
        start = false;
        stop = false;
        for (int i = 0; i < blockList.Length; i++)
        {
            blockList[i] = 0;
        }

        blockList[2] = 2; blockList[5] = 2; blockList[9] = 2; blockList[11] = 2;
        blockList[14] = 2; blockList[16] = 2; blockList[18] = 2; blockList[30] = 2;
        blockList[33] = 2; blockList[39] = 2;
        blockList[24] = 3;
        UpdateSprite();
        endImage.SetActive(false);
        text0.SetActive(false);
        text1.SetActive(false);
    }
    #endregion
    private void Awake()
    {
        for (int i = 0; i < gameObjectList.Length; i++)
        {
            gameObjectList[i] = GameObject.Find("ASCanvas/Block" + i);//关联游戏物体
            blockOrigin.GetComponent<Animator>().SetInteger("status", 3);
        }
    }
    private void OnEnable()
    {
        ResetData();
    }
    void Update()
    {
        if(!stop)
        {
            if (start)
            {
                timer += Time.deltaTime;
                //Debug.Log("Time:" + timer);
                float progress = (20.0f - timer) / 20;
                textTime.GetComponent<TMP_Text>().text = (int)(20.0f - timer) + "s";
                progressBar.transform.localScale = new Vector3(progress,1,1);
                progressBar.transform.localPosition = new Vector3(750*(progress-1),0,0);
            }
            if (Input.GetButtonDown("Up"))
            {
                Up();
            }
            else if (Input.GetButtonDown("Down"))
            {
                Down();
            }
            else if (Input.GetButtonDown("Left"))
            {
                Left();
            }
            else if (Input.GetButtonDown("Right"))
            {
                Right();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
            if(timer > 20) { End(); }
        }
    }
}
