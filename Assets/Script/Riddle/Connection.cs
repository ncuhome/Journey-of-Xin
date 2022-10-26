using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    private int stability = 2;//稳定性
    private float timer = 0;//计时器
    public GameObject Canvas;
    public GameObject Collider;//触发器
    public GameObject BarF1, BarF2;//稳定条 时间条
    public GameObject EndL,EndS;//结束按钮
    private Block[] blockList;
    private Block cursor;//当前选中的方块
    private int index;//暂留方块索引值

    private bool stop = false;
    private bool timerStart = false;
    // Start is called before the first frame update
  
    private void ResetData()//重置数据
    {
        //重置地图
        Canvas.SetActive(true);
        for (int i=0;i<blockList.Length;i++)
        {
            blockList[i].color = -1;
            blockList[i].status = 0;
            blockList[i].style = 1; 
        }
        blockList[15].style = 0;   blockList[15].color = 0;
        blockList[19].style = 0;   blockList[19].color = 0;
        blockList[18].style = 0;   blockList[18].color = 1;
        blockList[34].style = 0;   blockList[34].color = 1;
        blockList[24].style = 0;   blockList[24].color = 2;
        blockList[26].style = 0;   blockList[26].color = 2;
        blockList[33].style = 0;   blockList[33].color = 3;
        blockList[41].style = 0;   blockList[41].color = 3;
        blockList[36].style = 0;   blockList[36].color = 4;
        blockList[40].style = 0;   blockList[40].color = 4;
        //不会清除芯片稳定性
        timer = 0;
        BarF2.GetComponent<Image>().transform.localPosition = new Vector3(0, -450 * (timer / 20), 0);
        BarF2.GetComponent<Image>().transform.localScale = new Vector3(1, (1 - timer / 20f), 0);
        EndL.SetActive(false);
        EndS.SetActive(false);
        stop = false;
    }

    #region 启用函数
    private void Awake()
    {
        blockList = new Block[49];

        for (int i = 0; i < blockList.Length; i++)
        {
            blockList[i] = new Block();
            blockList[i].blockObject = GameObject.Find("Canvas/Map/Block (" + i + ")");//关联物体
        }
        Canvas.SetActive(true);
    }
    private void OnEnable()//再次激活后
    {
        ResetData();
        UpdateSprite();
    }
    private void Update()
    {
        if(timerStart)
        {
            if (stability == 0)//不稳定时，开始计时
            {
                timer += Time.deltaTime;
                if (timer > 20) { End(); }
                BarF2.GetComponent<Image>().transform.localPosition = new Vector3(0, -450 * (timer / 20), 0);
                BarF2.GetComponent<Image>().transform.localScale = new Vector3(1, (1 - timer / 20f), 0);
            }
        }
        BarF1.GetComponent<Image>().transform.localPosition = new Vector3(0,-150*(3-stability),0);
        BarF1.GetComponent<Image>().transform.localScale = new Vector3(1,  ( stability / 3f), 0);
        if (Input.GetButtonDown("Cancel")){ Cancel(); }
    }
    #endregion

    #region 输入控制
    public void MouseDown(float mx, float my)//鼠标按下判定
    {
        if (stop) { return; }
        timerStart = true;
        // Debug.Log("鼠标位置：X" + mx + "  Y:" + my);
        index = InsideBlock(mx, my);//获取暂留方块索引
        if(index > -1 && index <49)
        {
            Debug.Log("当前位置："+index);
            if (blockList[index].color > -1)
            {
                if(index == 15 || index == 19 || index == 18 || index == 34 || index == 24
                    || index == 26 || index == 33 || index == 41 || index == 36 || index == 40)
                {

                    Debug.Log("准备清除！！");
                    if (Surrd(index))
                    {
                        Debug.Log("清除！！");
                        clearCp();//清除当前线路
                        UpdateSprite();
                        index = -1;
                        return;
                    }
                }
                cursor = blockList[index];//获取抓取的方块
            }
            else
            {
                index = -1;
            }
        }
        else
        {
            index = -1;
        }
    }
    public void MouseUp()//鼠标松开
    {
        if (stop) { return; }
        cursor = null;
        index = -1;
        toEnd();
    }
    public void MouseDrag(float mx, float my)//鼠标拖动
    {
        if (stop) { return; }
        if (index > -1)//在已经选中方块时
        {
            int will = InsideBlock(mx, my);//获取当前鼠标所在方块索引
            Debug.Log("目标位置："+will+"   当前位置："+index );
            if(index/7 > 0)//暂留方块不在最上
            {
                if(will == index - 7)
                {
                    Debug.Log("上");
                    Up();
                }
            }
            if(index/7 < 6)//暂留方块不在最下
            {
                if(will == index + 7)
                {
                    Down();
                }
            }
            if(index%7 > 0)//暂离方块不在最左
            {
                if(will == index -1)
                {
                    Left();
                }
            }
            if(index%7 < 6)//暂留方块不在最右
            {
                if(will == index + 1)
                {
                    Right();
                }
            }
        }
    }
    #endregion
    #region 功能
    private void clearCp()//清除线路
    {
        int nowIndex = index;//当前索引值
        int direction = -1;//0上 1下 2左 3右
        Activation(index, 0);

        #region 初始配置
        bool clear = false;
        if (nowIndex / 7 > 0)//上
        {
            if (blockList[nowIndex - 7].status == 2)//右
            {
                nowIndex -= 7;
                direction = 3;
                clear = true;
            }
            else if (blockList[nowIndex - 7].status == 3)//左
            {
                nowIndex -= 7;
                direction = 2;
                clear = true;
            }
            else if (blockList[nowIndex - 7].status == 6)//上
            {
                nowIndex -= 7;
                direction = 0;
                clear = true;
            }

        }
        if (nowIndex / 7 < 6 && !clear)//下
        {
            if (blockList[nowIndex + 7].status == 1)//右
            {
                nowIndex += 7;
                direction = 3;
                clear = true;
            }
            else if (blockList[nowIndex + 7].status == 4)//左
            {
                nowIndex += 7;
                direction = 2;
                clear = true;
            }
            else if (blockList[nowIndex + 7].status == 6)//下
            {
                nowIndex += 7;
                direction = 1;
                clear = true;
            }

        }
        if (nowIndex % 7 > 0 && !clear)//左
        {
            if (blockList[nowIndex - 1].status == 1)//上
            {
                nowIndex--;
                direction = 0;
                clear = true;
            }
            else if (blockList[nowIndex - 1].status == 2)//下
            {
                nowIndex--;
                direction = 1;
                clear = true;
            }
            else if (blockList[nowIndex - 1].status == 5)//左
            {
                nowIndex--;
                direction = 2;
                clear = true;
            }

        }
        if (nowIndex % 7 < 6 && !clear)//右
        {
            if (blockList[nowIndex + 1].status == 3)//下
            {
                nowIndex++;
                direction = 1;
                clear = true;
            }
            else if (blockList[nowIndex + 1].status == 4)//上
            {
                nowIndex++;
                direction = 0;
                clear = true;
            }
            else if (blockList[nowIndex + 1].status == 5)//右
            {
                nowIndex++;
                direction = 3;
                clear = true;
            }

        }

        #endregion
        while (true)
        {
            switch (direction)
            {
                case 0://向上转
                    if (nowIndex / 7 == 0)
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex-7].color == blockList[nowIndex].color && blockList[nowIndex - 7].style == 1)//颜色相同
                    {
                        //获取下一次的方向
                        if (blockList[nowIndex - 7].status == 2)//右
                        {
                            direction = 3;
                        }
                        else if (blockList[nowIndex - 7].status == 3)//左
                        {
                            direction = 2;
                        }
                        else if (blockList[nowIndex - 7].status == 6)//上
                        {
                            direction = 0;
                        }
                        //清除当前颜色并移动至新位置
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex -= 7;
                    }
                    else//颜色不同则终止
                    {
                        //仅擦除当前颜色
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 1://向下转
                    if (nowIndex / 7 == 6)
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex + 7].color == blockList[nowIndex].color && blockList[nowIndex + 7].style == 1)//颜色相同
                    {
                        //获取下一次的方向
                        if (blockList[nowIndex + 7].status == 1)//右
                        {
                            direction = 3;
                        }
                        else if (blockList[nowIndex + 7].status == 4)//左
                        {
                            direction = 2;
                        }
                        else if (blockList[nowIndex + 7].status == 6)//下
                        {
                            direction = 1;
                        }
                        //清除当前颜色并移动至新位置
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex += 7;
                    }
                    else//颜色不同则终止
                    {
                        //仅擦除当前颜色
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 2://向左转
                    if (nowIndex % 7 == 0) 
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex - 1].color == blockList[nowIndex].color && blockList[nowIndex - 1].style == 1)//颜色相同
                    {
                        //获取下一次的方向
                        if (blockList[nowIndex - 1].status == 1)//上
                        {
                            direction = 0;
                        }
                        else if (blockList[nowIndex - 1].status == 2)//下
                        {
                            direction = 1;
                        }
                        else if (blockList[nowIndex - 1].status == 5)//左
                        {
                            direction = 2;
                        }
                        //清除当前颜色并移动至新位置
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex --;
                    }
                    else//颜色不同则终止
                    {
                        //仅擦除当前颜色
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 3://向右转
                    if (nowIndex % 7 == 6) 
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex + 1].color == blockList[nowIndex].color && blockList[nowIndex + 1].style == 1)//颜色相同
                    {
                        //获取下一次的方向
                        if (blockList[nowIndex + 1].status == 3)//下
                        {
                            direction = 1;
                        }
                        else if (blockList[nowIndex + 1].status == 4)//上
                        {
                            direction = 0;
                        }
                        else if (blockList[nowIndex + 1].status == 5)//右
                        {
                            direction = 3;
                        }
                        //清除当前颜色并移动至新位置
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex++;
                    }
                    else//颜色不同则终止
                    {
                        //仅擦除当前颜色
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case -1:
                    return;
            }
        }
    }
    private int InsideBlock(float mx, float my)//获取鼠标当前所在方块的索引值
    {
        int dx = 0;//行
        int startX = 435;//x起始遍历坐标
        while (true)
        {
            startX += 150;
            if (startX > mx) { break; }
            dx++;
        }
        int dy = 0;//列
        int startY = 1065;//y起始遍历坐标
        while (true)
        {
            startY -= 150;
            if (startY < my) { break; }
            dy++;
        }
        int indexC = dy * 7 + dx;//计算索引值

       // Debug.Log("抓取方块:" + index + "  X:" + dx + "  Y:" + dy);
        if (indexC > -1 && indexC < 49) { return indexC; }
        return -1;
    }
    private void UpdateSprite()//更新贴图动画
    {
        for (int i = 0; i < blockList.Length; i++)
        {
            GameObject obj = blockList[i].blockObject;

            switch (blockList[i].color)
            {
                case -1:
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(1,1,1,1);
                    break;
                case 0://红
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(230 / 255f, 25 / 255f, 15 / 255f, 255 / 255f);
                    break;
                case 1://黄
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(245 / 255f, 230 / 255f, 30 / 255f, 255 / 255f);
                    break;
                case 2://绿
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(55 / 255f, 250 / 255f, 50 / 255f, 255 / 255f);
                    break;
                case 3://蓝
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(50 / 255f, 250 / 255f, 250 / 255f, 255 / 255f);
                    break;
                case 4://紫
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(180 / 255f, 50 / 255f, 250 / 255f, 255 / 255f);
                    break;

            }
            if (blockList[i].style == 0)//为芯片
            {
                blockList[i].blockObject.GetComponent<Animator>().SetInteger("class", 0);
            }
            else//为连接点
            {
                blockList[i].blockObject.GetComponent<Animator>().SetInteger("class", 1);
            }
            blockList[i].blockObject.GetComponent<Animator>().SetInteger("status", blockList[i].status);
        }
    }
    private void Activation(int aim,int active)//激活/关闭一对芯片(active为0是非激活)
    {
        switch(aim)
        {
            case 15:
            case 19:
                blockList[19].status = blockList[15].status = active;
                break;
            case 18:
            case 34:
                blockList[18].status = blockList[34].status = active;
                break;
            case 24:
            case 26:
                blockList[24].status = blockList[26].status = active;
                break;
            case 33:
            case 41:
                blockList[33].status = blockList[41].status = active;
                break;
            case 36:
            case 40:
                blockList[36].status = blockList[40].status = active;
                break;

        }
    }
    private bool Surrd(int indexP)//判定周围是否有同色出口
    {
        int number = 0;
        if(indexP/7 > 0)//上
        {
            if(indexP - 7 != index)
            {
                if (blockList[indexP - 7].status == 2
                    || blockList[indexP - 7].status == 3
                    || blockList[indexP - 7].status == 6)
                {
                    if (blockList[indexP - 7].color == blockList[indexP].color) { number++; }
                   // Debug.Log("上有接口");
                }
            }
        }
        if(indexP/7 < 6)//下
        {
            if (indexP + 7 != index)
            {
                if (blockList[indexP + 7].status == 1
                || blockList[indexP + 7].status == 4
                || blockList[indexP + 7].status == 6)
                {
                    if (blockList[indexP + 7].color == blockList[indexP].color) { number++; }
                    //  Debug.Log("下有接口");
                }
            }
        }
        if(indexP%7 > 0)//左
        {
            if (indexP - 1 != index)
            {
                if (blockList[indexP - 1].status == 1
                || blockList[indexP - 1].status == 2
                || blockList[indexP - 1].status == 5)
                {
                    if (blockList[indexP - 1].color == blockList[indexP].color) { number++; }
                    //  Debug.Log("左有接口");
                }
            }
        }
        if(indexP%7 < 6)//右
        {
            if (indexP + 1 != index)
            {
                if (blockList[indexP + 1].status == 3
                || blockList[indexP + 1].status == 4
                || blockList[indexP + 1].status == 5)
                {
                    if (blockList[indexP + 1].color == blockList[indexP].color) { number++; }
                    // Debug.Log("右有接口");
                }
            }
        }
       // Debug.Log("NUmber:" + number);
        if(number >= 1)
        {
            return true;
        }
        return false;
    }

    private void Up()//向上延伸
    {
        int aimIndex = index - 7;
        if(blockList[aimIndex].style == 1)//为连接点
        {
           // Debug.Log("符合条件上，且符合条件为连接点");
           // Debug.Log("目标颜色："+ blockList[aimIndex].color);
            if (blockList[aimIndex].color == -1)//目标位置为无颜色
            {
               // Debug.Log("符合条件上，且符合条件为连接点，且符合目标无颜色");
                #region 变更目标位置方块的信息
                blockList[aimIndex].status = 6;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region 改变连接点的状态
                if (blockList[index].status == 5)
                {
                    if (index % 7 > 0)//检查左边
                    {
                        if (blockList[index - 1].color == cursor.color)//左边连接方块为相同颜色
                        {
                            if (blockList[index - 1].style == 1)//连接点必须向右连接
                            {
                                if (blockList[index - 1].status == 1
                                    || blockList[index - 1].status == 2
                                    || blockList[index - 1].status == 5)
                                {
                                    cursor.status = 4;//本方块变更成左上
                                }
                            }
                            else//芯片无方向要求
                            {
                                cursor.status = 4;//本方块变更成左上
                            }
                        }
                    }
                    if (index % 7 < 6)//检查右边
                    {
                        if (blockList[index + 1].color == cursor.color)//右边连接方块为相同颜色
                        {
                            if (blockList[index + 1].style == 1)//连接点必须向左连接
                            {
                                if (blockList[index + 1].status == 3
                                    || blockList[index + 1].status == 4
                                    || blockList[index + 1].status == 5)
                                {
                                    cursor.status = 1;//本方块变更成右上
                                }

                            }
                            else
                            {
                                cursor.status = 1;//本方块变更成右上
                            }

                        }
                    }
                }
                #endregion

                #region 更新选择的方块
                index -= 7;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//为芯片（同颜色）
        {

            #region 变更目标位置方块的信息
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//激活芯片
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region 改变连接点的状态
            if (blockList[index].status == 5)
            {
                if (index % 7 > 0)//检查左边
                {
                    if (blockList[index - 1].color == cursor.color)//左边连接方块为相同颜色
                    {
                        if (blockList[index - 1].style == 1)//连接点必须向右连接
                        {
                            if (blockList[index - 1].status == 1
                                || blockList[index - 1].status == 2
                                || blockList[index - 1].status == 5)
                            {
                                cursor.status = 4;//本方块变更成左上
                            }
                        }
                        else//芯片无方向要求
                        {
                            cursor.status = 4;//本方块变更成左上
                        }
                    }
                }
                if (index % 7 < 6)//检查右边
                {
                    if (blockList[index + 1].color == cursor.color)//右边连接方块为相同颜色
                    {
                        if (blockList[index + 1].style == 1)//连接点必须向左连接
                        {
                            if (blockList[index + 1].status == 3
                                || blockList[index + 1].status == 4
                                || blockList[index + 1].status == 5)
                            {
                                cursor.status = 1;//本方块变更成右上
                            }

                        }
                        else
                        {
                            cursor.status = 1;//本方块变更成右上
                        }

                    }
                }
            }
            #endregion

            #region 更新选择的方块
            MouseUp();//释放方块
            #endregion
        }
        UpdateSprite();
    }
    private void Down()//向下延伸
    {
        int aimIndex = index + 7;
        if (blockList[aimIndex].style == 1)//为连接点
        {
            if (blockList[aimIndex].color == -1)//目标位置为无颜色
            {
                #region 变更目标位置方块的信息
                blockList[aimIndex].status = 6;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region 改变连接点的状态
                if (blockList[index].status == 5)
                {
                    if (index % 7 > 0)//检查左边
                    {
                        if (blockList[index - 1].color == cursor.color)//左边连接方块为相同颜色
                        {
                            if (blockList[index - 1].style == 1)//连接点必须向右连接
                            {
                                if (blockList[index - 1].status == 1
                                    || blockList[index - 1].status == 2
                                    || blockList[index - 1].status == 5)
                                {
                                    cursor.status = 3;//本方块变更成左下
                                }
                            }
                            else//芯片无方向要求
                            {
                                cursor.status = 3;//本方块变更成左下
                            }

                        }
                    }
                    if (index % 7 < 6)//检查右边
                    {
                        if (blockList[index + 1].color == cursor.color)//右边连接方块为相同颜色
                        {
                            if (blockList[index + 1].style == 1)//连接点必须向左连接
                            {
                                if (blockList[index + 1].status == 3
                                    || blockList[index + 1].status == 4
                                    || blockList[index + 1].status == 5)
                                {
                                    cursor.status = 2;//本方块变更成右下
                                }

                            }
                            else
                            {
                                cursor.status = 2;//本方块变更成右下
                            }

                        }
                    }
                }
                
                #endregion

                #region 更新选择的方块
                index += 7;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//为芯片（同颜色）
        {
            #region 变更目标位置方块的信息
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//激活芯片
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region 改变连接点的状态
            if (blockList[index].status == 5)
            {
                if (index % 7 > 0)//检查左边
                {
                    if (blockList[index - 1].color == cursor.color)//左边连接方块为相同颜色
                    {
                        if (blockList[index - 1].style == 1)//连接点必须向右连接
                        {
                            if (blockList[index - 1].status == 1
                                || blockList[index - 1].status == 2
                                || blockList[index - 1].status == 5)
                            {
                                cursor.status = 3;//本方块变更成左下
                            }
                        }
                        else//芯片无方向要求
                        {
                            cursor.status = 3;//本方块变更成左下
                        }

                    }
                }
                if (index % 7 < 6)//检查右边
                {
                    if (blockList[index + 1].color == cursor.color)//右边连接方块为相同颜色
                    {
                        if (blockList[index + 1].style == 1)//连接点必须向左连接
                        {
                            if (blockList[index + 1].status == 3
                                || blockList[index + 1].status == 4
                                || blockList[index + 1].status == 5)
                            {
                                cursor.status = 2;//本方块变更成右下
                            }

                        }
                        else
                        {
                            cursor.status = 2;//本方块变更成右下
                        }

                    }
                }
            }

            #endregion

            #region 更新选择的方块
            MouseUp();//释放方块
            #endregion
        }
        UpdateSprite();
    }
    private void Left()
    {
        int aimIndex = index - 1;
        if (blockList[aimIndex].style == 1)//为连接点
        {
            if (blockList[aimIndex].color == -1)//目标位置为无颜色
            {
                #region 变更目标位置方块的信息
                blockList[aimIndex].status = 5;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region 改变连接点的状态
                if (blockList[index].status == 6)
                {

                    if (index / 7 > 0)//检查上边
                    {
                        if (blockList[index - 7].color == cursor.color)//上边连接方块为相同颜色
                        {
                            if (blockList[index - 7].style == 1)//连接点必须向下连接
                            {
                                if (blockList[index - 7].status == 2
                                    || blockList[index - 7].status == 3
                                    || blockList[index - 7].status == 6)
                                {
                                    cursor.status = 4;//本方块变更成左上
                                }
                            }
                            else
                            {
                                cursor.status = 4;//本方块变更成左上
                            }

                        }
                    }
                    if (index / 7 < 6)//检查下边
                    {
                        if (blockList[index + 7].color == cursor.color)//右边连接方块为相同颜色
                        {
                            if (blockList[index + 7].style == 1)//连接点必须向上连接
                            {
                                if (blockList[index + 7].status == 1
                                    || blockList[index + 7].status == 4
                                    || blockList[index + 7].status == 6)
                                {
                                    cursor.status = 3;//本方块变更成左下
                                }
                            }
                            else
                            {
                                cursor.status = 3;//本方块变更成左下
                            }
                        }
                    }
                }
                #endregion

                #region 更新选择的方块
                index --;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//为芯片（同颜色）
        {
            #region 变更目标位置方块的信息
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//激活芯片
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region 改变连接点的状态
            if (blockList[index].status == 6)
            {

                if (index / 7 > 0)//检查上边
                {
                    if (blockList[index - 7].color == cursor.color)//上边连接方块为相同颜色
                    {
                        if (blockList[index - 7].style == 1)//连接点必须向下连接
                        {
                            if (blockList[index - 7].status == 2
                                || blockList[index - 7].status == 3
                                || blockList[index - 7].status == 6)
                            {
                                cursor.status = 4;//本方块变更成左上
                            }
                        }
                        else
                        {
                            cursor.status = 4;//本方块变更成左上
                        }

                    }
                }
                if (index / 7 < 6)//检查下边
                {
                    if (blockList[index + 7].color == cursor.color)//右边连接方块为相同颜色
                    {
                        if (blockList[index + 7].style == 1)//连接点必须向上连接
                        {
                            if (blockList[index + 7].status == 1
                                || blockList[index + 7].status == 4
                                || blockList[index + 7].status == 6)
                            {
                                cursor.status = 3;//本方块变更成左下
                            }
                        }
                        else
                        {
                            cursor.status = 3;//本方块变更成左下
                        }
                    }
                }
            }
            #endregion

            #region 更新选择的方块
            MouseUp();//释放方块
            #endregion
        }
        UpdateSprite();
    }
    private void Right()
    {
        int aimIndex = index + 1;
        if (blockList[aimIndex].style == 1)//为连接点
        {
            if (blockList[aimIndex].color == -1)//目标位置为无颜色
            {
                #region 变更目标位置方块的信息
                blockList[aimIndex].status = 5;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region 改变连接点的状态
                if (blockList[index].status == 6)
                {
                    if (index / 7 > 0)//检查上边
                    {
                        if (blockList[index - 7].color == cursor.color)//上边连接方块为相同颜色
                        {
                            if (blockList[index - 7].style == 1)//连接点必须向下连接
                            {
                                if (blockList[index - 7].status == 2
                                    || blockList[index - 7].status == 3
                                    || blockList[index - 7].status == 6)
                                {
                                    cursor.status = 1;//本方块变更成右上
                                }
                            }
                            else
                            {
                                cursor.status = 1;//本方块变更成右上
                            }
                        }
                    }
                    if (index / 7 < 6)//检查下边
                    {
                        if (blockList[index + 7].color == cursor.color)//右边连接方块为相同颜色
                        {
                            if (blockList[index + 7].style == 1)//连接点必须向上连接
                            {
                                if (blockList[index + 7].status == 1
                                    || blockList[index + 7].status == 4
                                    || blockList[index + 7].status == 6)
                                {
                                    cursor.status = 2;//本方块变更成右下
                                }
                            }
                            else
                            {
                                cursor.status = 2;//本方块变更成右下
                            }
                        }
                    }
                }
                #endregion

                #region 更新选择的方块
                index++;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//为芯片（同颜色）
        {
            #region 变更目标位置方块的信息
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//激活芯片
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region 改变连接点的状态
            if (blockList[index].status == 6)
            {

                if (index / 7 > 0)//检查上边
                {
                    if (blockList[index - 7].color == cursor.color)//上边连接方块为相同颜色
                    {
                        if (blockList[index - 7].style == 1)//连接点必须向下连接
                        {
                            if (blockList[index - 7].status == 2
                                || blockList[index - 7].status == 3
                                || blockList[index - 7].status == 6)
                            {
                                cursor.status = 1;//本方块变更成右上
                            }
                        }
                        else
                        {
                            cursor.status = 1;//本方块变更成右上
                        }
                    }
                }
                if (index / 7 < 6)//检查下边
                {
                    if (blockList[index + 7].color == cursor.color)//右边连接方块为相同颜色
                    {
                        if (blockList[index + 7].style == 1)//连接点必须向上连接
                        {
                            if (blockList[index + 7].status == 1
                                || blockList[index + 7].status == 4
                                || blockList[index + 7].status == 6)
                            {
                                cursor.status = 2;//本方块变更成右下
                            }
                        }
                        else
                        {
                            cursor.status = 2;//本方块变更成右下
                        }
                    }
                }
            }
            #endregion

            #region 更新选择的方块
            MouseUp();//释放方块
            #endregion
        }
        UpdateSprite();
    }
    private void Cancel()//退出中断游戏
    {
        End();
    }
    public void Close()//退出游戏
    {
        Canvas.SetActive(false);
        gameObject.SetActive(false);
    }
    private void toEnd()//判定是否结束并跳转至相应结果
    {
        if (blockList[15].status == 1 && blockList[19].status == 1 && blockList[18].status == 1
              && blockList[34].status == 1 && blockList[24].status == 1 && blockList[26].status == 1
              && blockList[33].status == 1 && blockList[41].status == 1 && blockList[36].status == 1
              && blockList[40].status == 1)//所有芯片被激活，进入结束判断
        {
            for (int i = 0; i < blockList.Length; i++)
            {
                if (blockList[i].status == 0)//存在没有激活的节点时失败
                {
                    End();
                    return;
                }
            }
            //全部填充完毕成功
            EndSuccess();
            return;
        }
    }
    private void End()//失败退出
    {
        stop = true;
        timerStart = false;
        if (stability > 0)
        {
            stability--;
        }//芯片损坏
       // Thread.Sleep(1500);
        EndL.SetActive(true);
    }
    private void EndSuccess()//成功退出（需记录事件）
    {
        stop = true;
        timerStart = false;
      //  Thread.Sleep(1500);
        EndS.SetActive(true);
    }
    #endregion
    private class Block//单元对象
    {
        public GameObject blockObject = null;//对应的游戏物体
        public int style = 0;//块的类型 0：芯片  1：连接点
        public int status = 0;//当前块状态 00：芯片未连接 01：芯片已连接 ；
        public int color = -1;//光色 0：红 1：黄 2：绿 3：蓝 4：紫  -1:无颜色
    }
}
