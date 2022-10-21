using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**<summary>
 * 使用过场动画前，需设置动画将要转入的场景索引值.
 * 并在调用函数中，添加LoadingCanvas预制件.
 * 需要播放动画时，只需生成LoadingCanvas预制件.
 * LoadingCanvas预制件会定时销毁.
 * </summary>
 */
public class LoadingScript : MonoBehaviour
{
    private static int scene;//所需要加载的场景索引值
    private AsyncOperation operation;//异步加载组件
    public Animator animatorLoading;//播放过场动画器
    private float timer = 0;//计时器
    private int status = 0;//动画播放状态 0：准备播放渐入画面  1：准备播放渐出画面
    public static int Scene { get; set; }
    void Start()
    {
        toNewScene();
    }


    void Update()
    {
        Debug.Log("场景加载进度：" + operation.progress);
        timer += Time.deltaTime;//计时器

        if (InputManager.Instance != null)
        {
            InputManager.Instance.sceneState = SceneState.Animation;
        }

        if (operation.progress == 0.9f && timer >= 3.0 && status == 0)//加载完毕后 且 动画播放完成 后跳转
        {

            Debug.Log("加载场景完毕");
            DontDestroyOnLoad(animatorLoading.gameObject);//加载新场景时不销毁过场动画物体
            status = 1;
            timer = 0;//归零计时器
            animatorLoading.SetTrigger("nextScene");//播放过场消失动画
            operation.allowSceneActivation = true;//跳转至新场景
        }
        else if (status == 1 && timer >= 5.0)//定时销毁加载动画
        {
            InputManager.Instance.sceneState = SceneState.MainScene;
            Destroy(this.gameObject);
        }
    }

    private void toNewScene()//启动新的场景（异步）
    {
        //animatorLoading.SetTrigger("nextScene");//播放过场启动动画
        Debug.Log("开始加载场景");
        animatorLoading.SetTrigger("nextScene");//播放过场开始动画
        StartCoroutine(LoadScene());//使用异步加载场景
    }

    private IEnumerator LoadScene()//异步加载（使用协程）
    {
        operation = SceneManager.LoadSceneAsync(Scene);
        operation.allowSceneActivation = false;
        yield return operation;
    }
}