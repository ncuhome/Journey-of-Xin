using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject Star;
    private float starCDTimer = 0;//�������ǵļ�ʱ��
    public void End()//ʧ��
    {

    }
    public void EndSuccess()//�ɹ�
    {

    }

    private void CreatStar()//��������
    {
        GameObject star = Instantiate(Star, new Vector3(2000, 10 + Random.value * 1000, 0), Quaternion.identity);
        StarMove starMove = star.GetComponent<StarMove>();
        starMove.dirction = new Vector3(-100, -30 + Random.value * 30, 0) * 10 * (1+Random.value*2);//�������
        float size = 40 + 100 * Random.value;
        star.transform.localScale = new Vector3(size,size,0);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(starCDTimer <= 0)//��ȴ���
        {
            CreatStar();
            CreatStar();
            CreatStar();
            if (Random.value > 0.5)
            {
                CreatStar();
                CreatStar();
            }
            starCDTimer = 0.5f+Random.value;
        }
        else
        {
            starCDTimer -= Time.deltaTime;
        }
    }
}
