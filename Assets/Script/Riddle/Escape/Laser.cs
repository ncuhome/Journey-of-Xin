using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int status = 0;//״̬ 0����ʼ״̬ 1����ȫ״̬
    public Vector3 dirction;//����
    public GameObject player;//���
    public Boss boss;//boss��
    public int lengthMax = 100;//��󼤹ⳤ��
    public float speed = 1000;//�ٶ�

    private void OnTriggerEnter2D(Collider2D collision)//��ײ����
    {
        GameObject gobj = collision.gameObject;
        if (gobj.tag == "Player")//�����ײ�Ķ����� ��� �Ļ����򴥷�����ܻ�����
        {
            EscapePlayer ep = player.GetComponent<EscapePlayer>();
            if (!ep.invincible && !ep.deform)//���޵�,�ж�Ϊ�е�
            {
                ep.GetHit();
                boss.PlayerGetHit();
            }
            Destroy(gameObject);
        }
        else if(gobj.tag == "Deform")//�����ײ����Ϊ ���ܵĻ����������ӵ�
        {
            Destroy(gameObject);
        }
    }
    public void UpdateDirction()//�����Ի���ǰ�������ķ���
    {
        float px = player.transform.localPosition.x;
        float py = player.transform.localPosition.y;
        float lx = transform.localPosition.x;
        float ly = transform.localPosition.y;
        dirction = new Vector3(px - lx, py - ly, 0);
        #region ʹ���⳯�����
        float angle = Mathf.Atan2(dirction.y, dirction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        #endregion
    }


    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0:
                if (transform.localScale.x < lengthMax)//���켤�ⳤ��
                {
                    transform.localScale += new Vector3(speed / 2,0,0)*Time.deltaTime;
                    transform.localPosition += dirction.normalized * (speed / 2f) * Time.deltaTime;
                }
                else
                {
                    status = 1;
                }
                break;
            case 1://��������
                transform.localPosition += dirction.normalized * speed * Time.deltaTime;
                float fx = transform.localPosition.x;
                float fy = transform.localPosition.y;
                if(fx <= -5000 || fx >= 5000 || fy <= -2000 || fy >= 2000)//�������������
                {
                    Destroy(gameObject);
                }
                break;
        }

    }
}
