using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterstellarPassagePlayer : MonoBehaviour
{
    private int speedMax = 20;//�����ٶ�����
    private float energy = 24;//����
    private int HP = 3;
    private bool stop = false;

    public bool invincible = false;//�Ƿ��޵�
    public bool deform = false;//�Ƿ�������

    public GameObject Form;//����
    public GameObject HPBar;//Ѫ��
    public GameObject EnergyBar;//������
    public InterstellarPassage systemRiddle;

    private float timer = 0;//�޵м�ʱ��
    private Vector3 aimPosition = new Vector3(900,0,-20);//Ĭ��λ��
    // Start is called before the first frame update

    public void GetHit()//�ܵ�ײ��
    {
       // Debug.Log("�ܵ�ײ��");
        HP--;
        systemRiddle.CanmeraRock();
        UpdateBar();
        invincible = true;
        GetComponent<Animator>().SetTrigger("hit");//�����ܻ�����
        if(HP <= 0) 
        {
            HP = 0; 
            stop = true;
            systemRiddle.End();
        }
    }
    private void Move()//������Ŀ��λ���ƶ�
    {
       // Debug.Log("AIM:" + aimPosition + "   NOW:" + gameObject.transform.localPosition);
        Vector3 aimVector = (aimPosition - gameObject.transform.localPosition);
        gameObject.transform.localPosition += aimVector * speedMax / 100;
    }
    private void UpdateBar()//����������
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
    public void Defom()//����������
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
            if (Input.GetMouseButtonDown(0))//�������
            {
                if (energy >= 24)
                {
                    Defom();
                }
            }

            #region ״̬
            UpdateBar();
            if (invincible)//�����޵�
            {
                timer += Time.deltaTime;
                if (timer > 1.5)//�޵н���
                {
                    invincible = false;
                }
            }
            if (deform)
            {
                energy -= 8 * Time.deltaTime;//��������
                if (energy <= 0) { deform = false; Form.SetActive(false); }
            }
            else if (energy < 24)
            {
                energy += 3 * Time.deltaTime;//��������
            }
            #endregion
            #region  �ƶ����
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
