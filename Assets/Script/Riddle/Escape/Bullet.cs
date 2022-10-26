using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dirction;//����
    public Boss boss;//boss��
    public float speed = 400;//�ٶ�
    private void OnTriggerEnter2D(Collider2D collision)//��ײ����
    {
        GameObject gobj = collision.gameObject;
        if (gobj.tag == "Player")//�����ײ�Ķ����� ��� �Ļ����򴥷�����ܻ�����
        {
            EscapePlayer ep = collision.GetComponent<EscapePlayer>();
            if (!ep.invincible && !ep.deform)//���޵�,�ж�Ϊ�е�
            {
                ep.GetHit();
                boss.PlayerGetHit();
            }
            Destroy(gameObject);
        }
        else if (gobj.tag == "Deform")//�����ײ����Ϊ ���ܵĻ����������ӵ�
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += dirction.normalized * speed * Time.deltaTime;
        float fx = transform.localPosition.x;
        float fy = transform.localPosition.y;
        if (fx <= -3000 || fx >= 3000 || fy <= -2000 || fy >= 2000)//�������������
        {
            Destroy(gameObject);
        }
    }
}
