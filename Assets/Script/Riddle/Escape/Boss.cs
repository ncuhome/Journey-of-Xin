using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour//boss行为脚本
{
    private Vector3 aimPosition;//目标位置
    private int status = 1;//行为状态
    //0:起始状态
    private bool stop = false;//是否停止

    public GameObject servantA;//使魔机A
    public GameObject servantB;//使魔机B
    public GameObject servantC;//使魔机C
    public GameObject Laser;//激光
    public GameObject player;//自机

    private GameObject sc1, sc2;//使魔C
    private float servantCCDtimer = 4;

    private float usualTimer = 5;//通用计时器
    private float moveCDTimer = 0;//改变位置的cd计时器
    private int maxCountA = 3,countA = 0;//使魔机A数量上限
    private int maxCountB = 0,countB = 0;//使魔机B数量上限
    private int maxCountC = 0,countC = 0;//使魔机C数量上限

    private int hitCount = 0;//玩家成功中弹计数，每中弹三次Boss向x移动200

    private float speed = 100;//boss移动速度

    public void PlayerGetHit()//玩家中弹
    {
        hitCount++;
    }
    private void UpdateStatus()//状态更新
    {
        // Debug.Log("使魔A数量：" + countA + "    MAX:" + maxCountA);
        EscapePlayer playerSI = player.GetComponent<EscapePlayer>();
        if (status == 0)
        {
            if (countA < maxCountA)//如果使魔数量未达到上限，则召唤使魔
            {
                status = 1;
            }
            if (countB < maxCountB)//如果使魔数量未达到上限，则召唤使魔
            {
                status = 2;
            }
            if(countC < maxCountC)//如果使魔数量未达到上限，则召唤使魔
            {
                status = 3;
            }
        }
        if(playerSI.Energy > 105)//增加使魔机C的上限至2
        {
            maxCountC = 2;
        }
        if(playerSI.Energy > 90)//增加使魔机B的上限至4
        {
            maxCountB = 4;
        }
        else if (playerSI.Energy > 80)//增加使魔机A的上限至4
        {
            maxCountA = 4;
        }
        else if (playerSI.Energy > 60)//增加使魔机B的上限至2
        {
            maxCountB = 2;
        }
    }

    private void ServantCAttack()
    {
        if(sc1 != null && sc2 != null)
        {
            if (servantCCDtimer <= 0)//冷却完成
            {
                sc1.GetComponent<ServantC>().Attack();
                sc2.GetComponent<ServantC>().Attack();
                servantCCDtimer = 4 + 4 * Random.value;
            }
            else
            {
                servantCCDtimer -= Time.deltaTime;
            }
        }
    }
    private void Behaviour()//行为
    {
        switch (status)
        {
            case 0://通常状态
                Behaviour0();
                break;
            case 1://准备A状态
                Behaviour1();
                break;
            case 2://准备B状态
                Behaviour2();
                break;
            case 3://准备C状态
                Behaviour3();
                break;
            case 4://移动
                Behaviour4();
                break;
            case 5://主炮轰击
                Behaviour5();
                break;
        }
        ServantCAttack();
    }
    private void Behaviour0()//行为0：寻找移动位置
    {
      //  Debug.Log("冷却时间："+moveCDTimer);
        if (moveCDTimer <= 0)//移动冷却完毕
        {
            
            //  Debug.Log("前往移动状态");
            status = 4;//前往移动状态
            //Debug.Log("状态："+status);
            Vector3 nowPosition = transform.localPosition;//当前位置
            
            aimPosition = new Vector3(nowPosition.x, Random.value*500 + 300, -50);
            if(aimPosition.x > 0)
            {
                if(Random.value > 0.6)
                {
                    aimPosition -= new Vector3(200, 0, 0);
                }
            }
            if(aimPosition.x < 600)
            {
                if (Random.value > 0.6)
                {
                    aimPosition += new Vector3(200, 0, 0);
                }
            }
            // Debug.Log("目标位置："+aimPosition);
            return;
        }
        else
        {
            moveCDTimer -= Time.deltaTime;
        }
    }
    private void Behaviour1()//行为1：准备使魔机A
    {
        //Debug.Log("使魔A数量：" + countA + "MAX:" + maxCountA);
        //行为1
        if (usualTimer < 0)//冷却完毕
        {
            if (countA < maxCountA)
            {
                //Debug.Log("生成使魔A");
                GameObject A = Instantiate(servantA, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                A.transform.SetParent(transform);
                A.GetComponent<ServantA>().boss = gameObject;
                A.GetComponent<ServantA>().player = player;
                countA++;
                usualTimer = 0.7f;
            }
            status = 0;
            return;
        }
        else
        {
            usualTimer -= Time.deltaTime;
        }
    }
    private void Behaviour2()//行为2：准备使魔机B
    {
        if (usualTimer < 0)//冷却完毕
        {
            if (countB < maxCountB)
            {
                //Debug.Log("生成使魔A");
                GameObject B = Instantiate(servantB, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                B.transform.SetParent(transform);
                B.GetComponent<ServantB>().boss = gameObject;
                countB++;
                usualTimer = 2f;
            }
            status = 0;
            return;
        }
        else
        {
            usualTimer -= Time.deltaTime;
        }
    }
    private void Behaviour3()//行为3：准备使魔机C
    {
        if (usualTimer < 0)//冷却完毕
        {
            if (countC < maxCountC)
            {
                //Debug.Log("生成使魔A");
                sc1 = Instantiate(servantC, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                sc1.transform.SetParent(transform);
                sc1.GetComponent<ServantC>().dirction = new Vector3(Random.value,-1+2*Random.value);
                sc1.GetComponent<ServantC>().boss = gameObject;

                sc2 = Instantiate(servantC, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                sc2.transform.SetParent(transform);
                sc2.GetComponent<ServantC>().dirction = new Vector3(Random.value, -1 + 2 * Random.value);
                sc2.GetComponent<ServantC>().boss = gameObject;
                countC+=2;
                usualTimer = 2f;
            }
            status = 0;
            return;
        }
        else
        {
            usualTimer -= Time.deltaTime;
        }
    }
    private void Behaviour4()//行为4：移动
    {
        Vector3 move = new Vector3(aimPosition.x - transform.localPosition.x, aimPosition.y - transform.localPosition.y,0);
        //Debug.Log("移动中："+move);
        if (move.magnitude <= 1)
        {
            transform.localPosition = aimPosition;
            moveCDTimer = 6 + Random.value * 10;
            if (Random.value > 0.9) { status = 0; }
            else { status = 5; }
        }
        transform.localPosition += move.normalized * speed * Time.deltaTime;
    }
    private void Behaviour5()//行为5：主炮轰击
    {
        GameObject laser = Instantiate(Laser, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        laser.transform.localScale = new Vector3(10,150,1);
        Laser laserSI = laser.GetComponent<Laser>();
        laserSI.dirction = Vector3.right;
        laserSI.lengthMax = 2000;
        laserSI.player = player;
        laserSI.speed = 5000;
        laserSI.boss = this;
        status = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (stop) { return; }
        UpdateStatus();
        Behaviour();
    }
}
