using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterstellarPassagePlayer : MonoBehaviour
{
    private int speedMax = 20;//机体速度上限
    private float energy = 24;//能量
    private int HP = 3;
    private bool stop = false;

    public bool invincible = false;//是否无敌
    public bool deform = false;//是否开启护盾

    public GameObject Form;//护盾
    public GameObject HPBar;//血条
    public GameObject EnergyBar;//能量条
    public InterstellarPassage systemRiddle;

    private float timer = 0;//无敌计时器
    private Vector3 aimPosition = new Vector3(900,0,-20);//默认位置
    // Start is called before the first frame update

    public void GetHit()//受到撞击
    {
       // Debug.Log("受到撞击");
        HP--;
        systemRiddle.CanmeraRock();
        UpdateBar();
        invincible = true;
        GetComponent<Animator>().SetTrigger("hit");//播放受击动画
        if(HP <= 0) 
        {
            HP = 0; 
            stop = true;
            systemRiddle.End();
        }
    }
    private void Move()//机体向目标位置移动
    {
       // Debug.Log("AIM:" + aimPosition + "   NOW:" + gameObject.transform.localPosition);
        Vector3 aimVector = (aimPosition - gameObject.transform.localPosition);
        gameObject.transform.localPosition += aimVector * speedMax / 100;
    }
    private void UpdateBar()//更新条长度
    {
        EnergyBar.transform.localScale = new Vector3(energy / 24f,1,1);
        EnergyBar.transform.localPosition = new Vector3((energy / 24f)*300-250, -10,0);
        if(energy/24f < 0.25)
        {
            EnergyBar.GetComponent<Image>().color = new Color(255/255f,0,0, 150/255f);
        }
        else if(energy / 24f < 0.5)
        {
            EnergyBar.GetComponent<Image>().color = new Color(255 / 255f, 180/255f, 0, 150 / 255f);
        }
        else
        {
            EnergyBar.GetComponent<Image>().color = new Color(50 / 255f, 250 / 255f, 255 / 255f, 150/255f);
        }
        HPBar.transform.localScale = new Vector3(HP / 3f, 1, 1);
        HPBar.transform.localPosition = new Vector3((HP / 3f) * 300 - 280, 0, 0);
        if (HP / 3f < 0.35)
        {
            HPBar.GetComponent<Image>().color = new Color(255 / 255f, 0, 0, 150 / 255f);
        }
        else if(HP/3f<0.7)
        {
            HPBar.GetComponent<Image>().color = new Color(255/255f,180/255f,150/255f);
        }
        else
        {
            HPBar.GetComponent<Image>().color = new Color(50 / 255f, 250 / 255f, 255 / 255f, 150 / 255f);
        }
    }
    public void Defom()//开启防御盾
    {
        deform = true;
        Form.SetActive(true);
    }
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            if (Input.GetMouseButtonDown(0))//按下左键
            {
                if (energy >= 24)
                {
                    Defom();
                }
            }

            #region 状态
            UpdateBar();
            if (invincible)//短暂无敌
            {
                timer += Time.deltaTime;
                if (timer > 1.5)//无敌结束
                {
                    invincible = false;
                }
            }
            if (deform)
            {
                energy -= 8 * Time.deltaTime;//减少能量
                if (energy <= 0) { deform = false; Form.SetActive(false); }
            }
            else if (energy < 24)
            {
                energy += 3 * Time.deltaTime;//积累能量
            }
            #endregion
            #region  移动相关
            /* Debug.Log("AIM:" + aimPosition + "   NOW:" + gameObject.transform.localPosition);
             gameObject.transform.localPosition = Input.mousePosition;*/

            aimPosition.x = Input.mousePosition.x;
            if (aimPosition.x < 200) { aimPosition.x = 200; }
            else if (aimPosition.x > 1800) { aimPosition.x = 1800; }

            aimPosition.y = Input.mousePosition.y;
            if (aimPosition.y > 1000) { aimPosition.y = 1000; }
            else if (aimPosition.y < 100) { aimPosition.y = 100; }

            Move();
            #endregion
        }
    }
}
