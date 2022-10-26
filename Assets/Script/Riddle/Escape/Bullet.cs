using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dirction;//方向
    public Boss boss;//boss机
    public float speed = 400;//速度
    private void OnTriggerEnter2D(Collider2D collision)//碰撞发生
    {
        GameObject gobj = collision.gameObject;
        if (gobj.tag == "Player")//如果碰撞的对象是 玩家 的话，则触发玩家受击函数
        {
            EscapePlayer ep = collision.GetComponent<EscapePlayer>();
            if (!ep.invincible && !ep.deform)//非无敌,判定为中弹
            {
                ep.GetHit();
                boss.PlayerGetHit();
            }
            Destroy(gameObject);
        }
        else if (gobj.tag == "Deform")//如果碰撞对象为 护盾的话，则销毁子弹
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += dirction.normalized * speed * Time.deltaTime;
        float fx = transform.localPosition.x;
        float fy = transform.localPosition.y;
        if (fx <= -3000 || fx >= 3000 || fy <= -2000 || fy >= 2000)//超出区域后销毁
        {
            Destroy(gameObject);
        }
    }
}
