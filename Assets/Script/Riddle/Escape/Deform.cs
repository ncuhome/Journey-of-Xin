using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deform : MonoBehaviour
{
    private float timer = 2.5f;//����ʱ���ʱ��
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()//����ʱ���¼�ʱ
    {
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            Debug.Log("���ܼ�����");
            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("������ʧ");
            gameObject.SetActive(false);
        }
    }
}
