using TMPro;
using UnityEngine;

//用于管理主界面的UI互动
public class System : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;//动画播放组件
    private int cursor = 0;//当前光标位置
   
    //0：Xin（制作人员名单）     1：开始新的游戏    2：游戏设置      3：回忆游戏      4：退出返回游戏
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Up"))//选项向上
        {
            animator.SetTrigger("up");
            cursor--;
            if (cursor < 0) { cursor = 4; }
           
        }
        else if(Input.GetButtonDown("Down"))//
        {
            animator.SetTrigger("down");
            cursor++;
            if(cursor > 4) { cursor = 0; }
        }
        else if(Input.GetButtonDown("Confirm"))//确认
        {
            switch(cursor)
            {
                case 0://显示制作者名单
                    break;
                case 1://开始新的游戏
                    break;
                case 2://游戏设置
                    break;
                case 3://回顾剧情和欣赏音乐
                    break;
                case 4://退出游戏
                    Application.Quit();
                    break;
            }
        }
    }


}
