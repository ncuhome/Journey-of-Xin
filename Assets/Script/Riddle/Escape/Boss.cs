using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour//boss��Ϊ�ű�
{
    private Vector3 aimPosition;//Ŀ��λ��
    private int status = 1;//��Ϊ״̬
    //0:��ʼ״̬
    private bool stop = false;//�Ƿ�ֹͣ

    public GameObject servantA;//ʹħ��A
    public GameObject servantB;//ʹħ��B
    public GameObject servantC;//ʹħ��C
    public GameObject Laser;//����
    public GameObject player;//�Ի�

    private GameObject sc1, sc2;//ʹħC
    private float servantCCDtimer = 4;

    private float usualTimer = 5;//ͨ�ü�ʱ��
    private float moveCDTimer = 0;//�ı�λ�õ�cd��ʱ��
    private int maxCountA = 3,countA = 0;//ʹħ��A��������
    private int maxCountB = 0,countB = 0;//ʹħ��B��������
    private int maxCountC = 0,countC = 0;//ʹħ��C��������

    private int hitCount = 0;//��ҳɹ��е�������ÿ�е�����Boss��x�ƶ�200

    private float speed = 100;//boss�ƶ��ٶ�

    public void PlayerGetHit()//����е�
    {
        hitCount++;
    }
    private void UpdateStatus()//״̬����
    {
        // Debug.Log("ʹħA������" + countA + "    MAX:" + maxCountA);
        EscapePlayer playerSI = player.GetComponent<EscapePlayer>();
        if (status == 0)
        {
            if (countA < maxCountA)//���ʹħ����δ�ﵽ���ޣ����ٻ�ʹħ
            {
                status = 1;
            }
            if (countB < maxCountB)//���ʹħ����δ�ﵽ���ޣ����ٻ�ʹħ
            {
                status = 2;
            }
            if(countC < maxCountC)//���ʹħ����δ�ﵽ���ޣ����ٻ�ʹħ
            {
                status = 3;
            }
        }
        if(playerSI.Energy > 105)//����ʹħ��C��������2
        {
            maxCountC = 2;
        }
        if(playerSI.Energy > 90)//����ʹħ��B��������4
        {
            maxCountB = 4;
        }
        else if (playerSI.Energy > 80)//����ʹħ��A��������4
        {
            maxCountA = 4;
        }
        else if (playerSI.Energy > 60)//����ʹħ��B��������2
        {
            maxCountB = 2;
        }
    }

    private void ServantCAttack()
    {
        if(sc1 != null && sc2 != null)
        {
            if (servantCCDtimer <= 0)//��ȴ���
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
    private void Behaviour()//��Ϊ
    {
        switch (status)
        {
            case 0://ͨ��״̬
                Behaviour0();
                break;
            case 1://׼��A״̬
                Behaviour1();
                break;
            case 2://׼��B״̬
                Behaviour2();
                break;
            case 3://׼��C״̬
                Behaviour3();
                break;
            case 4://�ƶ�
                Behaviour4();
                break;
            case 5://���ں��
                Behaviour5();
                break;
        }
        ServantCAttack();
    }
    private void Behaviour0()//��Ϊ0��Ѱ���ƶ�λ��
    {
      //  Debug.Log("��ȴʱ�䣺"+moveCDTimer);
        if (moveCDTimer <= 0)//�ƶ���ȴ���
        {
            
            //  Debug.Log("ǰ���ƶ�״̬");
            status = 4;//ǰ���ƶ�״̬
            //Debug.Log("״̬��"+status);
            Vector3 nowPosition = transform.localPosition;//��ǰλ��
            
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
            // Debug.Log("Ŀ��λ�ã�"+aimPosition);
            return;
        }
        else
        {
            moveCDTimer -= Time.deltaTime;
        }
    }
    private void Behaviour1()//��Ϊ1��׼��ʹħ��A
    {
        //Debug.Log("ʹħA������" + countA + "MAX:" + maxCountA);
        //��Ϊ1
        if (usualTimer < 0)//��ȴ���
        {
            if (countA < maxCountA)
            {
                //Debug.Log("����ʹħA");
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
    private void Behaviour2()//��Ϊ2��׼��ʹħ��B
    {
        if (usualTimer < 0)//��ȴ���
        {
            if (countB < maxCountB)
            {
                //Debug.Log("����ʹħA");
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
    private void Behaviour3()//��Ϊ3��׼��ʹħ��C
    {
        if (usualTimer < 0)//��ȴ���
        {
            if (countC < maxCountC)
            {
                //Debug.Log("����ʹħA");
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
    private void Behaviour4()//��Ϊ4���ƶ�
    {
        Vector3 move = new Vector3(aimPosition.x - transform.localPosition.x, aimPosition.y - transform.localPosition.y,0);
        //Debug.Log("�ƶ��У�"+move);
        if (move.magnitude <= 1)
        {
            transform.localPosition = aimPosition;
            moveCDTimer = 6 + Random.value * 10;
            if (Random.value > 0.9) { status = 0; }
            else { status = 5; }
        }
        transform.localPosition += move.normalized * speed * Time.deltaTime;
    }
    private void Behaviour5()//��Ϊ5�����ں��
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
