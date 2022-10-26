using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantA : MonoBehaviour//������A
{
    public GameObject player;//�Ի�
    public GameObject boss;//boss��
    public GameObject laser;//����ļ���

    private float attackCDTimer = 2;//����cd
    private int status = 0;//״̬ģʽ 0����ʼ׼��   1:�ȴ�׼������   2��ͨ��   3���ܸ���
    private float timer = 0.3f;//��ʱ��ʱ��
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
            case 0://׼��״̬
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//��ת
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
            case 2://ͨ��״̬
                transform.Rotate(new Vector3(0, 0, 1), 720 * Time.deltaTime);//��ת
                transform.RotateAround(boss.transform.position, new Vector3(0, 0, 1), 180 * Time.deltaTime);//��ת
                if (attackCDTimer <= 0)//��ȴ��ȫ ��������
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
