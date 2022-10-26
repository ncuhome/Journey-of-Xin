using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantC : MonoBehaviour
{
    private int status = 0;//状态

    private float timer = 0.3f;//动画计时器

    public GameObject boss;//boss机
    public GameObject laserStatic;//子弹
    public Vector3 dirction;//方向
    public float speed = 200;//移动速度

    //0：准备状态  1：通常状态 2：受干扰状态
    public void Attack()//攻击
    {
        Debug.Log("Boss位置："+boss.transform.position);
        GameObject laserStatic0 = 
            Instantiate(laserStatic, gameObject.transform.position, Quaternion.identity);
        laserStatic0.transform.eulerAngles = new Vector3(0,0,90);
        GameObject laserStatic1 =
            Instantiate(laserStatic, gameObject.transform.position,Quaternion.identity);

        Boss bossC = boss.GetComponent<Boss>();
        LaserStatic laserStaticSI = laserStatic0.GetComponent<LaserStatic>();
        laserStaticSI.servant = gameObject;

        laserStaticSI = laserStatic1.GetComponent<LaserStatic>();
        laserStaticSI.servant = gameObject;
    }

    public void PlayerGetHit()
    {
        boss.GetComponent<Boss>().PlayerGetHit();
    }
    private void Move()//移动
    {
        transform.position += dirction.normalized * speed * Time.deltaTime;
        if(transform.position.y > 1000 && dirction.y > 0)
        {
            dirction = new Vector3 (-1+Random.value*2, -Random.value, 0);
        }
        if(transform.position.y < 100 && dirction.y < 0)
        {
            dirction = new Vector3(-1 + Random.value * 2, Random.value, 0);
        }
        if(transform.position.x < 50 && dirction.x < 0)
        {
            dirction = new Vector3(Random.value,-1 + Random.value * 2, 0);
        }
        if(transform.position.x > 1800 && dirction.x > 0)
        {
            dirction = new Vector3(-Random.value, -1 + Random.value * 2, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0://准备状态
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                if (timer <= 0)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("start");
                    status = 1;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case 1://通常状态
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                Move();
                //攻击部分在boss机内
                break;
            case 2://受干扰状态
                break;
        }
    }
}
