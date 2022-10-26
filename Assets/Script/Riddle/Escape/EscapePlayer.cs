using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapePlayer : MonoBehaviour
{
    private int speedMax = 10;//机体速度上限
    private float energy = 1;//能量(上限200)
    private bool stop = false;

    public bool invincible = false;//是否无敌
    public bool deform = false;//是否处于护盾模式
    public bool invincibleTF = false;//测试使用的无敌

    public GameObject form;//护盾
    public GameObject bomb;//技能
    public GameObject FormTimeBar;//护盾CD条
    public GameObject BobmTimeBar;//技能CD条
    public GameObject EnergyBar;//能量条
    public Animator ImageAnimator;//贴图的动画器

    public Escape systemRiddle;//所属管理系统

    private float timer = 0;//无敌计时器
    private float formCDTimer = 0;//护盾cd计时器
    private float bombCDTimer = 0;//技能cd计时器

    private Vector3 aimPosition = new Vector3(900, 0, -20);//默认位置
    // Start is called before the first frame update

    public float Energy { get { return energy; } }

    public void GetHit()//受到撞击
    {
        ImageAnimator.SetTrigger("hit");//播放受击动画
        if (invincibleTF) { return; }
        if (energy > 0) { energy -= 10; }//能量-10
        UpdateBar();//更新状态条
        invincible = true;//无敌
        timer = 1.5f;
    }
    private void Move()//机体向目标位置移动
    {
        // Debug.Log("AIM:" + aimPosition + "   NOW:" + gameObject.transform.localPosition);
        Vector3 aimVector = (aimPosition - gameObject.transform.localPosition);
        gameObject.transform.localPosition += aimVector * speedMax / 100;
    }
    private void UpdateBar()//更新条长度
    {
        EnergyBar.transform.localScale = new Vector3(energy/200f,1,1);
        EnergyBar.transform.localPosition = new Vector3(-500+500*(energy/200f),0,0);

        FormTimeBar.transform.localScale = new Vector3(1-(formCDTimer / 7f), 1, 1);
        FormTimeBar.transform.localPosition = new Vector3(-150 * (formCDTimer / 7f), 0, 0);

        BobmTimeBar.transform.localScale = new Vector3(1-(bombCDTimer / 15f), 1, 1);
        BobmTimeBar.transform.localPosition = new Vector3(-150 * (bombCDTimer / 15f), 0, 0);
    }
    public void Defom()//开启防御盾
    {
       // Debug.Log("开启护盾");
        form.SetActive(true);
        invincible = true;//无敌
        timer = 2.5f;
    }
    public void Bomb()//开启技能
    {
        bomb.SetActive(true);
        invincible = true;//无敌
        timer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("STOP:"+stop);
        if (!stop)
        {
            #region 状态
            if (formCDTimer>0)//护盾cd计时
            {
                formCDTimer -= Time.deltaTime;
            }
            if(bombCDTimer>0)//技能cd计时
            {
                bombCDTimer -= Time.deltaTime;
            }
            if (timer > 0)//短暂无敌
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
                invincible = false;
            }
            if(deform)//护盾模式
            {
                if(formCDTimer > 2)
                {
                    deform = false;
                }
            }
            if (energy <= 0)//失败游戏判定
            {
                energy = 0;
                UpdateBar();
                stop = true;
                systemRiddle.End();
                return;
            }
            if (energy < 200)
            {
                energy += Time.deltaTime * 3.5f;
            }
            else//通关游戏判定
            {
                energy = 200;
                stop = true;
                systemRiddle.EndSuccess();
            }
            UpdateBar();
            #endregion 

            #region 技能
            if (Input.GetMouseButtonDown(0))//按下左键
            {
                if (energy > 18 && formCDTimer <= 0)//冷却结束
                {
                    energy -= 16;
                    formCDTimer = 6;//重新计时cd
                    Defom();//激活护盾
                }
            }
            else if(Input.GetMouseButtonDown(1))//按下右键
            {
                if(energy > 20 && bombCDTimer <= 0)//冷却结束
                {
                    energy -= 24;
                    bombCDTimer = 8;
                    Bomb();//开启冲击波
                }
            }
            #endregion

            #region  移动相关
            
            aimPosition.x = Input.mousePosition.x;
            if (aimPosition.x < 200) { aimPosition.x = 200; }
            else if (aimPosition.x > 1800) { aimPosition.x = 1800; }

            aimPosition.y = Input.mousePosition.y;
            if (aimPosition.y > 1000) { aimPosition.y = 1000; }
            else if (aimPosition.y < 100) { aimPosition.y = 100; }
           // Debug.Log("MOUSE:" + Input.mousePosition + "   AIM:" +aimPosition);
            Move();
            #endregion
        }
    }
}
