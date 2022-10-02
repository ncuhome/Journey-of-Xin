using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/***未实现播放过场动画后自动销毁动画物体**/
//用于管理主界面的UI互动
public class TitleSystem : MonoBehaviour
{
    public Animator animator;//动画播放组件
    public Animator animatorLoading;//播放过场动画器
    private AsyncOperation operation;//异步加载组件
    private int cursor = 0;//当前光标位置
    private bool nextScene = false;//是否进入下一个场景
    private float timer = 0;//计时器
    //0：Xin（制作人员名单）     1：开始新的游戏    2：游戏设置      3：回忆游戏      4：退出返回游戏

    void Start()
    {
    }

    void Update()
    {
        if(!nextScene)//在当前场景 下对玩家操作的监控
        {
            if (Input.GetButtonDown("Up"))//选项向上
            {
                animator.SetTrigger("up");
                cursor--;
                if (cursor < 0) { cursor = 4; }

            }
            else if (Input.GetButtonDown("Down"))//选项向下
            {
                animator.SetTrigger("down");
                cursor++;
                if (cursor > 4) { cursor = 0; }
            }
            else if (Input.GetButtonDown("Confirm"))//确认
            {
                switch (cursor)
                {
                    case 0://显示制作者名单
                        toProducer();
                        break;
                    case 1://开始新的游戏
                        toNewGame();
                        break;
                    case 2://游戏设置
                        toSettings();
                        break;
                    case 3://回顾剧情和欣赏音乐
                        toMemory();
                        break;
                    case 4://退出游戏
                        Application.Quit();
                        break;
                }
            }

        }
        else
        {
            Debug.Log("场景加载进度：" + operation.progress);
            timer += Time.deltaTime;//计时器
            if (operation.progress == 0.9f && timer >= 3.0)//加载完毕后 且 动画播放完成 后跳转
            {
                Debug.Log("加载场景完毕");
                DontDestroyOnLoad(animatorLoading.gameObject);//加载新场景时不销毁过场动画物体
                animatorLoading.SetTrigger("nextScene");//播放过场消失动画
                operation.allowSceneActivation = true;//跳转至新场景
            }
        }

    }

    private void toProducer()//前往制作者名单画面
    {

    }
    private void toNewGame()//开始新的游戏
    {
        nextScene = true;
        Debug.Log("开始加载场景");
        animatorLoading.SetTrigger("nextScene");//播放过场开始动画
        StartCoroutine(LoadScene());//使用异步加载场景
    }
    private IEnumerator LoadScene()//异步加载（使用协程）
    {
        operation = SceneManager.LoadSceneAsync("SceneState1");
        operation.allowSceneActivation = false;
        yield return operation;
    }
    private void toSettings()//前往游戏设置画面
    {

    }
    private void toMemory()//前往回忆与欣赏画面
    {

    }


}
