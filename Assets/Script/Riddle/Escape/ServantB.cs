using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantB : MonoBehaviour
{
    private int status = 0;//״̬

    private float timer = 0.3f;//������ʱ��
    private float attackCDTimer = 0;//����cd��ʱ��
    private int countB = 5;//�ӵ���

    public GameObject boss;//boss��
    public GameObject bullet;//�ӵ�
    public float speed = 100;//�ƶ��ٶ�

    //0��׼��״̬  1��ͨ��״̬ 2���ܸ���״̬

    private void Attack()//����
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
            case 0://׼��״̬
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//��ת
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
            case 1://ͨ��״̬�������ƶ���
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//��ת
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
                if (attackCDTimer <= 0)//��ȴ��ȫ ��������
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
            case 2://ͨ��״̬�������ƶ���
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//��ת
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
                if (attackCDTimer <= 0)//��ȴ��ȫ ��������
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
            case 3://�ܸ���״̬
                break;

        }
    }
}
