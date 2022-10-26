using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public Vector3 dirction = Vector3.left;//ʯ����
    public int status = 1;//�˺�ǿ��
    private float timer = 0;//���ٵ���ʱ��
    private bool destroy = false;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy)
        {
            timer += Time.deltaTime;
            if (timer > 1) { Destroy(gameObject); }
        }
        else
        {
            gameObject.transform.position += dirction;
            if (gameObject.transform.position.x < -500) { Destroy(gameObject); }
        }
            
    }
    public void Disappear()
    {
        destroy = true;
        gameObject.GetComponent<Animator>().SetTrigger("disappear");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("������ײ");
        if (destroy) { return; }
        GameObject gobj = collision.gameObject;
        if(gobj.tag == "Player")//�����ײ�Ķ����� ��� �Ļ����򴥷�����ܻ�����
        {
            if(!gobj.GetComponent<InterstellarPassagePlayer>().invincible)//���޵�״̬
            {
               // Debug.Log("���޵�");
                if(status == 1 && gobj.GetComponent<InterstellarPassagePlayer>().deform)//���ڻ���״̬
                {
                    Disappear();
                }
                else
                {
                    gobj.GetComponent<InterstellarPassagePlayer>().GetHit();
                    Disappear();
                }
            }
        }
    }
}
