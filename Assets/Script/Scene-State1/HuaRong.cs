using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HuaRong : MonoBehaviour
{
    private int[] square = new int[] { 0, 2, 3, 2, 2, 4, 6, 6, 1, 3, 6, 6, 5, 4, 1, 4 };
    private GameObject[] blockList;//游戏对象块组
    private int move = 0;//0:等待输入 1：向上移动白块中 2：向左移动 3：向下移动 4：向右移动
    int cursor = 0;//当前光标（空格）的索引值

    private Vector3 oldPosition;//旧坐标
    //左下角为起点，右上角为终点
    void Start()
    {
        blockList = new GameObject[16];
        for(int i=0;i<16;i++)
        {
            blockList[i] = GameObject.Find("Block ("+i+")");//初始化：载入游戏物体
        }
        
    }

    

    void Update()
    {
        switch(move)
        {
            case 0:
                if (isSuccess()) { Debug.Log("解谜成功！！"); }
                if (Input.GetButtonDown("Up"))
                {
                    Down();
                }
                else if (Input.GetButtonDown("Down"))
                {
                    Up();
                }
                else if (Input.GetButtonDown("Left"))
                {
                    Right();
                }
                else if (Input.GetButtonDown("Right"))
                {
                    Left();
                }
                else if (Input.GetButtonDown("Cancel"))
                {
                    Cancel();
                }
                
                break;
            case 1://白块向上移动
                blockList[cursor].transform.position += new Vector3(0, -Time.deltaTime*20, 0);//向下移动块
                if (blockList[cursor].transform.position.y <= oldPosition.y-2)//移动到指定的位置后
                {
                    move = 0;//恢复等待状态
                    blockList[cursor].transform.position = oldPosition + new Vector3(0, -2, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor + 4];
                    blockList[cursor + 4] = gobj;
                }
                break;
            case 2://白块向左移动
                blockList[cursor].transform.position += new Vector3(Time.deltaTime*20, 0, 0);//向右移动块
                if (blockList[cursor].transform.position.x >= oldPosition.x+2)//移动到指定的位置后
                {
                    move = 0;//恢复等待状态
                    blockList[cursor].transform.position = oldPosition + new Vector3(2, 0, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor + 1];
                    blockList[cursor + 1] = gobj;
                }
                break;
            case 3://白块向下移动
                blockList[cursor].transform.position += new Vector3(0, Time.deltaTime*20, 0);//向上移动块
                if (blockList[cursor].transform.position.y >= oldPosition.y+2)//移动到指定的位置后
                {
                    move = 0;//恢复等待状态
                    blockList[cursor].transform.position = oldPosition + new Vector3(0, 2, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor - 4];
                    blockList[cursor - 4] = gobj;
                }
                break;
            case 4://向右移动白块
                blockList[cursor].transform.position += new Vector3(-Time.deltaTime*20, 0, 0);//向左移动块
                if (blockList[cursor].transform.position.x <= oldPosition.x-2)//移动到指定的位置后
                {
                    move = 0;//恢复等待状态
                    blockList[cursor].transform.position = oldPosition + new Vector3(-2, 0, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor - 1];
                    blockList[cursor - 1] = gobj;
                }
                break;


        }
       
    }

    private void Up()//空格向上交换
    {
        if (cursor > 3)//光标不在最顶上
        {
            square[cursor] = square[cursor - 4];
            square[cursor - 4] = 0;
            cursor -= 4;
            oldPosition = blockList[cursor].transform.position;
            move = 1;
        }
    }

    private void Down()//空格向下交换
    {
        if (cursor < 12)//光标不在最下方
        {
            square[cursor] = square[cursor + 4];
            square[cursor + 4] = 0;
            cursor += 4;
            oldPosition = blockList[cursor].transform.position;
            move = 3;
        }
    }

    private void Left()//空格向左交换
    {
        if (cursor % 4 != 0)//光标不在最左边
        {
            square[cursor] = square[cursor - 1];
            square[cursor - 1] = 0;
            cursor--;
            oldPosition = blockList[cursor].transform.position;
            move = 2;
        }
    }

    private void Right()//空格向右交换
    {
        if (cursor % 4 != 3)//光标不在最右边
        {
            square[cursor] = square[cursor + 1];
            square[cursor + 1] = 0;
            cursor++;
            oldPosition = blockList[cursor].transform.position;
            move = 4;
        }
    }

    private void Cancel()//取消键 退出
    {

    }
    private bool isSuccess()//判定是否解谜成功
    {
        for(int i=0;i<16;i++)
        {
            changeSprite(i,false);
        }
        int aimIndex = 12;//当前索引值
        int from = 1;//来自方向 0：上 1:左 2：下 3：右
        bool switchBoll = true;//循环开关
        while (switchBoll)//左下角（左面）为起点，右上角（右面）为终点
        {
            switch(from)
            {
                case 0://来自上
                    if(square[aimIndex] == 1)//转向右
                    {
                        changeSprite(aimIndex, true);
                        from = 1;
                        if(aimIndex%4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else if(square[aimIndex] == 4)//转向左
                    {
                        changeSprite(aimIndex, true);
                        from = 3;
                        if(aimIndex%4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else if(square[aimIndex] == 6)//转向下
                    {
                        changeSprite(aimIndex, true);
                        from = 0;
                        if(aimIndex/4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex+=4;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 1://来自左
                    if (square[aimIndex] == 3)//转向下
                    {
                        changeSprite(aimIndex, true);
                        from = 0;
                        if (aimIndex / 4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex += 4;
                    }
                    else if (square[aimIndex] == 4)//转向上
                    {
                        changeSprite(aimIndex, true);
                        from = 2;
                        if(aimIndex/4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -=4;
                    }
                    else if (square[aimIndex] == 5)//转向右
                    {
                        changeSprite(aimIndex, true);
                        from = 1;
                        if (aimIndex % 4 == 3)
                        {
                            if (aimIndex == 4)
                            {
                                switchBoll = false;
                            }
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 2://来自下
                    if (square[aimIndex] == 2)//转向右
                    {
                        changeSprite(aimIndex, true);
                        from = 1;
                        if (aimIndex % 4 == 3)
                        {
                            if (aimIndex == 4)
                            {
                                switchBoll = false;
                            }
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else if (square[aimIndex] == 3)//转向左
                    {
                        changeSprite(aimIndex, true);
                        from = 3;
                        if (aimIndex % 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else if (square[aimIndex] == 6)//转向上
                    {
                        changeSprite(aimIndex, true);
                        from = 2;
                        if (aimIndex / 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -= 4;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 3://来自右
                    if (square[aimIndex] == 1)//转向上
                    {
                        changeSprite(aimIndex, true);
                        from = 2;
                        if (aimIndex / 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -= 4;
                    }
                    else if (square[aimIndex] == 2)//转向下
                    {
                        changeSprite(aimIndex, true);
                        from = 0;
                        if (aimIndex / 4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex += 4;
                    }
                    else if (square[aimIndex] == 5)//转向左
                    {
                        changeSprite(aimIndex, true);
                        from = 3;
                        if (aimIndex % 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
            }
        }
        if (blockList[3].GetComponent<Animator>().GetBool("active"))
        {
            if (square[3] == 1 || square[3] == 2 || square[3] == 5)
            {
                return true;
            }
        }
        return false;
    }
    private void changeSprite(int index,bool full)
    {
        if(full)
        {
            blockList[index].GetComponent<Animator>().SetBool("active",true);
        }
        else
        {
            blockList[index].GetComponent<Animator>().SetBool("active",false);
        }
    }
}
