using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int status = 0;//状态 0：起始状态 1：完全状态
    public Vector3 dirction;//方向
    public GameObject player;//玩家
    public Boss boss;//boss机
    public int lengthMax = 100;//最大激光长度
    public float speed = 1000;//速度

    private void OnTriggerEnter2D(Collider2D collision)//碰撞发生
    {
        GameObject gobj = collision.gameObject;
        if (gobj.tag == "Player")//如果碰撞的对象是 玩家 的话，则触发玩家受击函数
        {
            EscapePlayer ep = player.GetComponent<EscapePlayer>();
            if (!ep.invincible && !ep.deform)//非无敌,判定为中弹
            {
                ep.GetHit();
                boss.PlayerGetHit();
            }
            Destroy(gameObject);
        }
        else if(gobj.tag == "Deform")//如果碰撞对象为 护盾的话，则销毁子弹
        {
            Destroy(gameObject);
        }
    }
    public void UpdateDirction()//更新自机当前相对自身的方向
    {
        float px = player.transform.localPosition.x;
        float py = player.transform.localPosition.y;
        float lx = transform.localPosition.x;
        float ly = transform.localPosition.y;
        dirction = new Vector3(px - lx, py - ly, 0);
        #region 使激光朝向玩家
        float angle = Mathf.Atan2(dirction.y, dirction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        #endregion
    }


    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0:
                if (transform.localScale.x < lengthMax)//延伸激光长度
                {
                    transform.localScale += new Vector3(speed / 2,0,0)*Time.deltaTime;
                    transform.localPosition += dirction.normalized * (speed / 2f) * Time.deltaTime;
                }
                else
                {
                    status = 1;
                }
                break;
            case 1://激光延伸
                transform.localPosition += dirction.normalized * speed * Time.deltaTime;
                float fx = transform.localPosition.x;
                float fy = transform.localPosition.y;
                if(fx <= -5000 || fx >= 5000 || fy <= -2000 || fy >= 2000)//超出区域后销毁
                {
                    Destroy(gameObject);
                }
                break;
        }

    }
}
