using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantA : MonoBehaviour//浮游炮A
{
    public GameObject player;//自机
    public GameObject boss;//boss机
    public GameObject laser;//发射的激光

    private float attackCDTimer = 2;//攻击cd
    private int status = 0;//状态模式 0：起始准备   1:等待准备动画   2：通常   3：受干扰
    private float timer = 0.3f;//临时计时器
    private bool stop = false;
    private void Attack()
    {
        GameObject laser0 = Instantiate(laser, transform.localPosition += new Vector3(0, 40, 0), Quaternion.identity);
        GameObject laser1 = Instantiate(laser, transform.localPosition += new Vector3(0, -40, 0), Quaternion.identity);

        laser0.transform.localPosition = transform.position + Vector3.up * 50;
        Laser laseSI = laser0.GetComponent<Laser>();
        laseSI.player = player;
        laseSI.boss = boss.GetComponent<Boss>();
        laseSI.lengthMax = 100;
        laseSI.UpdateDirction();

        laser1.transform.localPosition = transform.position + Vector3.down * 50;
        laseSI = laser1.GetComponent<Laser>();
        laseSI.player = player;
        laseSI.boss = boss.GetComponent<Boss>();
        laseSI.lengthMax = 100;
        laseSI.UpdateDirction();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stop) { return; }
        switch(status)
        {
            case 0://准备状态
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                if(transform.localPosition.magnitude < 3)
                {
                    transform.localPosition += new Vector3(3,0,0) * Time.deltaTime;
                }
                else
                {
                    transform.localPosition = new Vector3(3,0,0);
                    gameObject.GetComponent<Animator>().SetTrigger("start");
                    status = 1;
                }
                break;
            case 1:
                if(timer <= 0)
                {
                    status = 2;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case 2://通常状态
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//自转
                transform.RotateAround(boss.transform.position, new Vector3(0, 0, 1), 180 * Time.deltaTime);//公转
                if (attackCDTimer <= 0)//冷却完全 触发攻击
                {
                    Attack();
                    attackCDTimer = 2 + Random.value;
                }
                else
                {
                    attackCDTimer -= Time.deltaTime;
                }
                break;
        }
        
    }
}
