using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//解谜游戏1：圆盘归柱 
public class PlateOnThePost : MonoBehaviour
{
    private int[] rightKey = new int[] { 3, 0, 1, 2 };
    private int status = -1;//动画状态 -1：待机    0：取起    1：移动    2：放下
    private int aimX;//目标柱子
    public GameObject Canvas;
    private Plate[] plateList = new Plate[4];
    private Plate getPlate;//当前抓住的物体

    #region 核心逻辑与输入处理
    public void setPillar0()//点击柱子0
    {
        if (status == -1)
        {
            if(getPlate == null)
            {
                getTopPlate(0);
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(0);
                aimX = 0;
                status = 1;
            }
        }
    }
    public void setPillar1()//点击柱子1
    {
        if (status == -1)
        {
            if (getPlate == null)
            {
                getTopPlate(1);
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(1);
                aimX = 1;
                status = 1;
            }
        }
    }
    public void setPillar2()//点击柱子2
    {
        if (status == -1)
        {
            if (getPlate == null)
            {
                getTopPlate(2);                                   
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(2);
                aimX = 2;
                status = 1;
            }
        }
    }
    private void getTopPlate(int x)//获取一个柱子上的顶部圆盘
    {
        getPlate = null;
        for(int i=0;i<4;i++)
        {
            if (plateList[i].x == x)
            {
                if (getPlate == null)
                {
                    getPlate = plateList[i];
                }
                else if(getPlate.index < plateList[i].index)
                {
                    getPlate = plateList[i];
                }
            }
        }
    }
    private int getMaxIndex(int x)//获取一根柱子上的最高的index+1
    {
        int index = 0;
        for(int i=0;i<4;i++)
        {
           // Debug.Log("[" + i + "]" + plateList[i].x + ";");
            if (plateList[i] != getPlate && plateList[i].x == x )
            {
                index++;
            }
        }
        return index;
    }
    public void Cancel()//退出并检查是否成功解谜
    {
        InputManager.Instance.sceneState = SceneState.MainScene;
        EventSystem.Instance.ActiveEvent(30);//完成小游戏1
        EventSystem.Instance.ActiveEvent(27);//触发对话节点2
        SceneManager.UnloadSceneAsync(14);//卸载本场景
    }
    private bool isCompleteve()//判定是否完成游戏
    {
        for(int i=0;i<4;i++)
        {
            if (plateList[i].x != 1 || plateList[i].index != rightKey[i] )
            {
                return false;
            }
        }
        return true;
    }

    #endregion 核心逻辑与输入处理

    #region 动画显示

    private void Moving()//播放移动盘子的动画
    {
        switch(status)
        {
            case 0://取起盘子
                Debug.Log(getPlate == null);
                Vector3 vector = new Vector3(0, 400 - getPlate.plateObject.transform.localPosition.y, 0);
                if (vector.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector;
                    status = -1;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector / 5;
                }
                break;
            case 1:
                Vector3 vector1 = new Vector3((aimX*600-600)- getPlate.plateObject.transform.localPosition.x, 0,0);
                if (vector1.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector1;
                    status = 2;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector1 / 5;
                }
                break;
            case 2:
                Vector3 vector2 = new Vector3(0,(-400+getPlate.index*100) - getPlate.plateObject.transform.localPosition.y, 0);
                if (vector2.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector2;
                    getPlate.x = aimX;
                 //   Debug.Log(getPlate.index);
                    getPlate = null;
                    status = -1;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector2 / 5;
                }
                break;
        }
    }
    #endregion 动画显示

    void Start()
    {
        for(int i=0;i<4;i++)
        {
            plateList[i] = new Plate(0,i,GameObject.Find("Canvas/Backboard/"+i),"Plate00"+i);//将游戏物体封装进列表里
        }
    }
    private void OnEnable()
    {
        Canvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        Moving();
        if(Input.GetButtonDown("Cancel"))
        {
            Cancel();
        }
    }

    private class Plate//将抽象位置与游戏物体封装成一体的类
    {
        public int x, index;//盘子所在的柱子，柱子上的位置
        public string name;
        public GameObject plateObject;//游戏物体
        public Plate(int x, int index, GameObject plateObject,string name)
        {
            this.x = x;
            this.index = index;
            this.plateObject = plateObject;
            this.name = name;
        }
    }
}
