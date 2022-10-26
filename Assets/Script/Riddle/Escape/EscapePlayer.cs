using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapePlayer : MonoBehaviour
{
    private int speedMax = 10;//�����ٶ�����
    private float energy = 1;//����(����200)
    private bool stop = false;

    public bool invincible = false;//�Ƿ��޵�
    public bool deform = false;//�Ƿ��ڻ���ģʽ
    public bool invincibleTF = false;//����ʹ�õ��޵�

    public GameObject form;//����
    public GameObject bomb;//����
    public GameObject FormTimeBar;//����CD��
    public GameObject BobmTimeBar;//����CD��
    public GameObject EnergyBar;//������
    public Animator ImageAnimator;//��ͼ�Ķ�����

    public Escape systemRiddle;//��������ϵͳ

    private float timer = 0;//�޵м�ʱ��
    private float formCDTimer = 0;//����cd��ʱ��
    private float bombCDTimer = 0;//����cd��ʱ��

    private Vector3 aimPosition = new Vector3(900, 0, -20);//Ĭ��λ��
    // Start is called before the first frame update

    public float Energy { get { return energy; } }

    public void GetHit()//�ܵ�ײ��
    {
        ImageAnimator.SetTrigger("hit");//�����ܻ�����
        if (invincibleTF) { return; }
        if (energy > 0) { energy -= 10; }//����-10
        UpdateBar();//����״̬��
        invincible = true;//�޵�
        timer = 1.5f;
    }
    private void Move()//������Ŀ��λ���ƶ�
    {
        // Debug.Log("AIM:" + aimPosition + "   NOW:" + gameObject.transform.localPosition);
        Vector3 aimVector = (aimPosition - gameObject.transform.localPosition);
        gameObject.transform.localPosition += aimVector * speedMax / 100;
    }
    private void UpdateBar()//����������
    {
        EnergyBar.transform.localScale = new Vector3(energy/200f,1,1);
        EnergyBar.transform.localPosition = new Vector3(-500+500*(energy/200f),0,0);

        FormTimeBar.transform.localScale = new Vector3(1-(formCDTimer / 7f), 1, 1);
        FormTimeBar.transform.localPosition = new Vector3(-150 * (formCDTimer / 7f), 0, 0);

        BobmTimeBar.transform.localScale = new Vector3(1-(bombCDTimer / 15f), 1, 1);
        BobmTimeBar.transform.localPosition = new Vector3(-150 * (bombCDTimer / 15f), 0, 0);
    }
    public void Defom()//����������
    {
       // Debug.Log("��������");
        form.SetActive(true);
        invincible = true;//�޵�
        timer = 2.5f;
    }
    public void Bomb()//��������
    {
        bomb.SetActive(true);
        invincible = true;//�޵�
        timer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("STOP:"+stop);
        if (!stop)
        {
            #region ״̬
            if (formCDTimer>0)//����cd��ʱ
            {
                formCDTimer -= Time.deltaTime;
            }
            if(bombCDTimer>0)//����cd��ʱ
            {
                bombCDTimer -= Time.deltaTime;
            }
            if (timer > 0)//�����޵�
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
                invincible = false;
            }
            if(deform)//����ģʽ
            {
                if(formCDTimer > 2)
                {
                    deform = false;
                }
            }
            if (energy <= 0)//ʧ����Ϸ�ж�
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
            else//ͨ����Ϸ�ж�
            {
                energy = 200;
                stop = true;
                systemRiddle.EndSuccess();
            }
            UpdateBar();
            #endregion 

            #region ����
            if (Input.GetMouseButtonDown(0))//�������
            {
                if (energy > 18 && formCDTimer <= 0)//��ȴ����
                {
                    energy -= 16;
                    formCDTimer = 6;//���¼�ʱcd
                    Defom();//�����
                }
            }
            else if(Input.GetMouseButtonDown(1))//�����Ҽ�
            {
                if(energy > 20 && bombCDTimer <= 0)//��ȴ����
                {
                    energy -= 24;
                    bombCDTimer = 8;
                    Bomb();//���������
                }
            }
            #endregion

            #region  �ƶ����
            
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
