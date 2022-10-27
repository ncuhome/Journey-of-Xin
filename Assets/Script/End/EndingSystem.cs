using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSystem : MonoBehaviour
{
    public Animator animator;//结束动画
    public TMP_Text text;//结束标题
    public GameObject Loading;
    private float timer = 0;

    private void Start()
    {
        if(EventSystem.Instance)
        {
            switch (EventSystem.Instance.END)
            {
                case 1:
                    text.text = "结局一：安眠"; break;
                case 2:
                    text.text = "结局二：探险继续"; break;
                case 3:
                    text.text = "结局三：审判"; break;
                case 4:
                    text.text = "结局四：游戏结束"; break;
                case 5:
                    text.text = "结局五：反客为主"; break;
                case 6:
                    text.text = "结局六：安稳的梦"; break;
            }
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>4)
        {
            if(Input.anyKeyDown)//任意键继续
            {
                LoadingScript.Scene = 0;
                Instantiate(Loading, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
