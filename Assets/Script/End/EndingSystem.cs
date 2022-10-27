using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSystem : MonoBehaviour
{
    public Animator animator;//��������
    public TMP_Text text;//��������
    public GameObject Loading;
    private float timer = 0;

    private void Start()
    {
        if(EventSystem.Instance)
        {
            switch (EventSystem.Instance.END)
            {
                case 1:
                    text.text = "���һ������"; break;
                case 2:
                    text.text = "��ֶ���̽�ռ���"; break;
                case 3:
                    text.text = "�����������"; break;
                case 4:
                    text.text = "����ģ���Ϸ����"; break;
                case 5:
                    text.text = "����壺����Ϊ��"; break;
                case 6:
                    text.text = "����������ȵ���"; break;
            }
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>4)
        {
            if(Input.anyKeyDown)//���������
            {
                LoadingScript.Scene = 0;
                Instantiate(Loading, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
