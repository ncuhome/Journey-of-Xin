using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStatic : MonoBehaviour
{
    public GameObject servant;//����ʹħ
    public float activeTimer = 1.5f;//���ʱ��
    public float waitTimer = 1.5f;//Ԥ��ʱ��
    private int status = 0;
    private void OnTriggerExit2D(Collider2D collision)//��ײ���
    {
        if(status == 1)
        {
            GameObject gobj = collision.gameObject;
            if (gobj.tag == "Player")//�����ײ�Ķ����� ��� �Ļ����򴥷�����ܻ�����
            {
                EscapePlayer ep = collision.GetComponent<EscapePlayer>();
                if (!ep.invincible && !ep.deform)//���޵�,�ж�Ϊ�е�
                {
                    ep.GetHit();
                    servant.GetComponent<ServantC>().PlayerGetHit();
                }
               // Destroy(gameObject);
            }
            else if (gobj.tag == "Deform")//�����ײ����Ϊ ���ܵĻ����������ӵ�
            {
              //  Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("STATUS:"+status);
        switch(status)
        {
            case 0://Ԥ��
                transform.position = servant.transform.position;
                if (waitTimer <= 0)
                {
                    status = 1;
                    gameObject.GetComponent<Animator>().SetTrigger("attack");//ֹͣ��˸
                }
                else
                {
                    waitTimer -= Time.deltaTime;
                }
                break;
            case 1:
                transform.position = servant.transform.position;
                if (activeTimer <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    activeTimer -= Time.deltaTime;
                }
                break;
        }
    }
}
