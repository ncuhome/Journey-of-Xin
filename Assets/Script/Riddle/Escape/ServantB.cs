using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantB : MonoBehaviour
{
    private int status = 0;//状态

    private float timer = 0.3f;//动画计时器
    private float attackCDTimer = 0;//攻击cd计时器
    private int countB = 5;//子弹槽

    public GameObject boss;//boss机
    public GameObject bullet;//子弹
    public float speed = 100;//移动速度

    //0：准备状态  1：通常状态 2：受干扰状态

    private void Attack()//攻击
    {
        GameObject bullet0 = Instantiate(bullet, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        GameObject bullet1 = Instantiate(bullet, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet, transform.position + new Vector3(0, 0, 1), Quaternion.identity);

        Bullet bulletSI = bullet0.GetComponent<Bullet>();
        bulletSI.boss = boss.GetComponent<Boss>();
        bulletSI.dirction = new Vector3(1, 0, 0);

        bulletSI = bullet1.GetComponent<Bullet>();
        bulletSI.boss = boss.GetComponent<Boss>();
        bulletSI.dirction = new Vector3(1.73205f, 1, 0);

        bulletSI = bullet2.GetComponent<Bullet>();
        bulletSI.boss = boss.GetComponent<Boss>();
        bulletSI.dirction = new Vector3(1.73205f, -1, 0);

    }

    void Update()
    {
        switch(status)
        {
            case 0://准备状态
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                if(timer <= 0)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("start");
                    status = 1;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case 1://通常状态（向上移动）
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                if(transform.position.y < 1000)
                {
                    transform.position += new Vector3(0,speed,0) * Time.deltaTime;
                }
                else
                {
                    speed += 5 + 5 * Random.value;
                    attackCDTimer -= (0.5f + Random.value);///////////////////////////
                    status = 2;
                }
                if (attackCDTimer <= 0)//冷却完全 触发攻击
                {
                    Attack();
                    countB--;
                    if (countB > 0) 
                    {
                        attackCDTimer = 0.2f;
                    }
                    else 
                    {
                        attackCDTimer = 3 + Random.value;
                        countB = 5;
                    }
                }
                else
                {
                    attackCDTimer -= Time.deltaTime;
                }
                break;
            case 2://通常状态（向下移动）
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                if (transform.position.y > 100)
                {
                    transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
                }
                else
                {
                    speed += 5 + 5 * Random.value;
                    attackCDTimer -= (0.5f + Random.value);///////////////////////////
                    status = 1;
                }
                if (attackCDTimer <= 0)//冷却完全 触发攻击
                {
                    Attack();
                    countB--;
                    if (countB > 0)
                    {
                        attackCDTimer = 0.2f;
                    }
                    else
                    {
                        attackCDTimer = 1 + 2*Random.value;
                        countB = 5;
                    }
                }
                else
                {
                    attackCDTimer -= Time.deltaTime;
                }
                break;
            case 3://受干扰状态
                break;

        }
    }
}
