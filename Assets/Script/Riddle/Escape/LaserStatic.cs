using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStatic : MonoBehaviour
{
    public GameObject servant;//所属使魔
    public float activeTimer = 1.5f;//存活时间
    public float waitTimer = 1.5f;//预警时间
    private int status = 0;
    private void OnTriggerExit2D(Collider2D collision)//碰撞检测
    {
        if(status == 1)
        {
            GameObject gobj = collision.gameObject;
            if (gobj.tag == "Player")//如果碰撞的对象是 玩家 的话，则触发玩家受击函数
            {
                EscapePlayer ep = collision.GetComponent<EscapePlayer>();
                if (!ep.invincible && !ep.deform)//非无敌,判定为中弹
                {
                    ep.GetHit();
                    servant.GetComponent<ServantC>().PlayerGetHit();
                }
               // Destroy(gameObject);
            }
            else if (gobj.tag == "Deform")//如果碰撞对象为 护盾的话，则销毁子弹
            {
              //  Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("STATUS:"+status);
        switch(status)
        {
            case 0://预警
                transform.position = servant.transform.position;
                if (waitTimer <= 0)
                {
                    status = 1;
                    gameObject.GetComponent<Animator>().SetTrigger("attack");//停止闪烁
                }
                else
                {
                    waitTimer -= Time.deltaTime;
                }
                break;
            case 1:
                transform.position = servant.transform.position;
                if (activeTimer <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    activeTimer -= Time.deltaTime;
                }
                break;
        }
    }
}
