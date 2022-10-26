using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public Vector3 dirction = Vector3.left;//石方向
    public int status = 1;//伤害强度
    private float timer = 0;//销毁倒计时器
    private bool destroy = false;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy)
        {
            timer += Time.deltaTime;
            if (timer > 1) { Destroy(gameObject); }
        }
        else
        {
            gameObject.transform.position += dirction;
            if (gameObject.transform.position.x < -500) { Destroy(gameObject); }
        }
            
    }
    public void Disappear()
    {
        destroy = true;
        gameObject.GetComponent<Animator>().SetTrigger("disappear");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("发生碰撞");
        if (destroy) { return; }
        GameObject gobj = collision.gameObject;
        if(gobj.tag == "Player")//如果碰撞的对象是 玩家 的话，则触发玩家受击函数
        {
            if(!gobj.GetComponent<InterstellarPassagePlayer>().invincible)//非无敌状态
            {
               // Debug.Log("非无敌");
                if(status == 1 && gobj.GetComponent<InterstellarPassagePlayer>().deform)//处于护盾状态
                {
                    Disappear();
                }
                else
                {
                    gobj.GetComponent<InterstellarPassagePlayer>().GetHit();
                    Disappear();
                }
            }
        }
    }
}
