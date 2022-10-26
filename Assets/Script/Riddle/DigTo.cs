using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DigTo : MonoBehaviour
{
    private GameObject[] buttons = new GameObject[54];
    public TMP_Text textScore;
    private int[] upper = new int[54];//0：岩石 -1：岩浆 1：小型矿 2：中型矿 3：大型矿 -2:空 4 :炫彩矿脉
    private int[] lower = new int[54];
    private int score;//计分

    public void MouseDown(float mx,float my)
    {
        int index = GetIndex(mx-960,my-540);
        if(index > -1)
        {
            Break(index);
            UpdateSprite();
        }
    }
    private int GetIndex(float x,float y)
    {
        int hx = -1;
        int hy = -1;
        float minX = -675;
        float maxY = 450;
        while(x > minX)
        {
            hx++;
            minX += 150;
        }
        while(y < maxY)
        {
            hy++;
            maxY -= 150;
        }
        if (hx == -1 || hy == -1) { return -1; }
        int index = hx + hy * 9;
        Debug.Log("Position:(" + hx + "," + hy + ")   " + index);
        return index;
    }

    private void Break(int index)//破坏
    {
        if (upper[index] == -2)//上层为空
        {
            switch(lower[index])
            {
                case -2:break;//空
                case -1://岩浆
                    if(index%9 > 0)//不在最左
                    {
                        if (upper[index - 1] != -2)//不为空
                        {
                            upper[index - 1] = 0;
                        }
                        if (lower[index - 1] != -2)
                        {
                            lower[index - 1] = 0;
                        }
                    }
                    if(index%9 < 8)//不在最右
                    {
                        if (upper[index + 1] != -2)//不为空
                        {
                            upper[index + 1] = 0;
                        }
                        if (lower[index + 1] != -2)
                        {
                            lower[index + 1] = 0;
                        }
                    }
                    if(index/9 > 0)//不在最上
                    {
                        if (upper[index - 9] != -2)//不为空
                        {
                            upper[index - 9] = 0;
                        }
                        if (lower[index - 9] != -2)
                        {
                            lower[index - 9] = 0;
                        }
                    }
                    if(index/9 < 8)//不在最下
                    {
                        if (upper[index + 9] != -2)//不为空
                        {
                            upper[index + 9] = 0;
                        }
                        if (lower[index + 9] != -2)
                        {
                            lower[index + 9] = 0;
                        }
                    }
                    break;
                case 0:
                    lower[index] = -2;
                    break;
                case 2://中型矿脉
                    if(index == 36 || index == 37 || index == 45 || index == 46)
                    {
                        if ((lower[36] == 2 && lower[37] == 2 && lower[45] == 2 && lower[46] == 2)//矿脉完整
                            && (upper[36] == -2 && upper[37] == -2 && upper[45] == -2 && upper[46] == -2))//完全露出
                        {
                            lower[36] = -2; lower[37] = -2;
                            lower[45] = -2; lower[46] = -2;
                            score += 5;
                        }
                        else//否则按小矿计分
                        {
                            lower[index] = -2;
                            score++;
                        }
                    }
                    else if(index == 14 || index == 15 || index == 23 || index == 24)
                    {
                        if ((lower[14] == 2 && lower[15] == 2 && lower[23] == 2 && lower[24] == 2)//矿脉完整
                            && (upper[14] == -2 && upper[15] == -2 && upper[23] == -2 && upper[24] == -2))//完全露出
                        {
                            lower[14] = -2; lower[15] = -2;
                            lower[23] = -2; lower[24] = -2;
                            score += 5;
                        }
                        else//否则按小矿计分
                        {
                            lower[index] = -2;
                            score++;
                        }
                    }
                    break;
                case 3://大型矿脉
                    if(index == 0 || index == 1 || index == 2 || index == 9 || index == 10 || index == 11 || index == 18 || index == 19 || index == 20)
                    {
                       /* Debug.Log("大矿脉"+ (lower[0] == 2 && lower[1] == 2 && lower[2] == 2 && lower[9] == 2 && lower[10] == 2 && lower[11] == 2 && lower[18] == 2 && lower[19] == 2 && lower[20] == 2)+"  "
                            + (upper[0] == -2 && upper[1] == -2 && upper[2] == -2 && upper[9] == -2 && upper[10] == -2 && upper[11] == -2 && upper[18] == -2 && upper[19] == -2 && upper[20] == -2));
                        Debug.Log("0"+ (lower[0] == 2)); Debug.Log("1" + (lower[1] == 2));
                        Debug.Log("2" + (lower[2] == 2)); Debug.Log("9" + (lower[9] == 2));
                        Debug.Log("10" + (lower[10] == 2)); Debug.Log("11" + (lower[11] == 2));
                        Debug.Log("18" + (lower[18] == 2)); Debug.Log("19" + (lower[19] == 2));
                        Debug.Log("20" + (lower[20] == 2));*/
                        if ((lower[0] == 3 && lower[1] == 3 && lower[2] == 3 && lower[9] == 3 && lower[10] == 3 && lower[11] == 3 && lower[18] == 3 && lower[19] == 3 && lower[20] == 3)//矿脉完整
                            && (upper[0] == -2 && upper[1] == -2 && upper[2] == -2 && upper[9] == -2 && upper[10] == -2 && upper[11] == -2 && upper[18] == -2 && upper[19] == -2 && upper[20] == -2))//完全露出
                        {
                            lower[0] = -2; lower[1] = -2; lower[2] = -2;
                            lower[9] = -2; lower[10] = -2; lower[11] = -2;
                            lower[18] = -2; lower[19] = -2; lower[20] = -2;
                            score += 10;
                        }
                        else//否则按小矿计分
                        {
                            Debug.Log("按删除小矿");
                            lower[index] = -2;
                            score++;
                        }
                    }
                    else if(index == 31 || index == 32 || index == 33 || index == 40 || index == 41 || index == 42 || index == 49 || index == 50 || index == 51)
                    {
                        if ((lower[31] == 3 && lower[32] == 3 && lower[33] == 3 && lower[40] == 3 && lower[41] == 3 && lower[42] == 3 && lower[49] == 3 && lower[50] == 3 && lower[51] == 3)//矿脉完整
                           && (upper[31] == -2 && upper[32] == -2 && upper[33] == -2 && upper[40] == -2 && upper[41] == -2 && upper[42] == -2 && upper[49] == -2 && upper[50] == -2 && upper[51] == -2))//完全露出
                        {
                            lower[31] = -2; lower[32] = -2; lower[33] = -2;
                            lower[40] = -2; lower[41] = -2; lower[42] = -2;
                            lower[49] = -2; lower[50] = -2; lower[51] = -2;
                            score += 10;
                        }
                        else//否则按小矿计分
                        {
                            lower[index] = -2;
                            score++;
                        }
                    }
                    break;
                case 4:
                    lower[index] = -2;
                    score += 25;
                    break;
            }
        }
        else //上层不为空
        {
            if (upper[index] == 1) { score++; }
            upper[index] = -2;
        }
    }
    private void UpdateSprite()//更新贴图
    {
        textScore.text = "当前得分：" + score;
        for (int i = 0;i<upper.Length;i++)
        {
            switch(upper[i])
            {
                case -2://空
                    switch (lower[i])
                    {
                        case -2://空
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/透明");
                            break;
                        case -1:
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/岩浆");
                            break;
                        case 0:
                            buttons[i].transform.localEulerAngles = new Vector3(0,0,90);
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/岩石");
                            break;
                        case 1:
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/小型矿脉");
                            break;
                        case 2:
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/中型矿脉");
                            break;
                        case 3:
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/大型矿脉");
                            break;
                        case 4:
                            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/炫彩矿脉");
                            break;
                    }
                    break;
                case 0://岩石
                    buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/岩石");
                    break;
                case 1:
                    buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-DigTo/小型矿脉");
                    break;
            }
        }
    }
    private void Awake()
    {
        for(int i=0;i<buttons.Length;i++)//匹配物体
        {
            buttons[i] = GameObject.Find("Canvas/0/Button"+i);
        }
        RestData();//重设数据
        UpdateSprite();
    }
    private void RestData()//重置数据
    {
        upper[4] = 1; upper[10] = 1; upper[15] = 1; upper[20] = 1; upper[22] = 1; upper[26] = 1;
        upper[28] = 1; upper[33] = 1; upper[40] = 1; upper[46] = 1; upper[52] = 1; upper[53] = 1;

        lower[0] = lower[1] = lower[2] = lower[9] = lower[10] = lower[11] = lower[18] = lower[19] = lower[20] = 3;
        lower[31] = lower[32] = lower[33] = lower[40] = lower[41] = lower[42] = lower[49] = lower[50] = lower[51] = 3;
        lower[14] = lower[15] = lower[23] = lower[24] = 2;
        lower[36] = lower[37] = lower[45] = lower[46] = 2;
        lower[4] = -1; lower[16] = -1; lower[27] = -1; lower[30] = -1; lower[44] = -1; lower[48] = -1;
        lower[39] = 4;
    }
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))//退出
        {
            if(score > 25)//成功
            {
                if(EventSystem.Instance.isStaticEvent(35))
                {
                    EventSystem.Instance.ActiveEvent(106);
                }
                else
                {
                    EventSystem.Instance.ActiveEvent(107);
                }
                InputManager.Instance.sceneState = SceneState.MainScene;
                SceneManager.UnloadSceneAsync(11);
            }
            else
            {
                RestData();
            }
        }
    }
}
