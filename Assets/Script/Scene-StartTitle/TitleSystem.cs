using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ڹ����������UI����
/// </summary>
public class TitleSystem : MonoBehaviour
{
    public Animator animator;//�����������
    public GameObject animatorLoading;//��������Ԥ�Ƽ�
    private int cursor = 0;//��ǰ���λ��
    private bool nextScene = false;//�Ƿ������һ������
    //0��Xin��������Ա������     1����ʼ�µ���Ϸ    2����Ϸ����      3��������Ϸ      4���˳�������Ϸ

    void Start()
    {
    }

    void Update()
    {
        if (!nextScene)//�ڵ�ǰ���� �¶���Ҳ����ļ��
        {
            if (Input.GetButtonDown("Up"))//ѡ������
            {
                animator.SetTrigger("up");
                cursor--;
                if (cursor < 0) { cursor = 4; }

            }
            else if (Input.GetButtonDown("Down"))//ѡ������
            {
                animator.SetTrigger("down");
                cursor++;
                if (cursor > 4) { cursor = 0; }
            }
            else if (Input.GetButtonDown("Submit"))//ȷ��
            {
                switch (cursor)
                {
                    case 0://��ʾ����������
                        toProducer();
                        break;
                    case 1://��ʼ�µ���Ϸ
                        toNewGame();
                        break;
                    case 2://��Ϸ����
                        toSettings();
                        break;
                    case 3://�ع˾������������
                        toMemory();
                        break;
                    case 4://�˳���Ϸ
                        Application.Quit();
                        break;
                }
            }

        }
    }

    /// <summary>
    /// ǰ����������������
    /// </summary>
    private void toProducer()
    {

    }
    /// <summary>
    /// ��ʼ�µ���Ϸ
    /// </summary>
    private void toNewGame()
    {
        nextScene = true;
        Debug.Log("��ʼ���س���");
        LoadingScript.Scene = 7;//����ת�볡��������ֵ
        Instantiate(animatorLoading, Vector3.zero, Quaternion.identity);
    }
    /// <summary>
    /// ǰ����Ϸ���û���
    /// </summary>
    private void toSettings()
    {

    }
    /// <summary>
    /// ǰ�����������ͻ���
    /// </summary>
    private void toMemory()
    {

    }


}
