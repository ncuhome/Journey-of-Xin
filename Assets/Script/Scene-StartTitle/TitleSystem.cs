using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/***δʵ�ֲ��Ź����������Զ����ٶ�������**/
//���ڹ����������UI����
public class TitleSystem : MonoBehaviour
{
    public Animator animator;//�����������
    public Animator animatorLoading;//���Ź���������
    private AsyncOperation operation;//�첽�������
    private int cursor = 0;//��ǰ���λ��
    private bool nextScene = false;//�Ƿ������һ������
    private float timer = 0;//��ʱ��
    //0��Xin��������Ա������     1����ʼ�µ���Ϸ    2����Ϸ����      3��������Ϸ      4���˳�������Ϸ

    void Start()
    {
    }

    void Update()
    {
        if(!nextScene)//�ڵ�ǰ���� �¶���Ҳ����ļ��
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
            else if (Input.GetButtonDown("Confirm"))//ȷ��
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
        else
        {
            Debug.Log("�������ؽ��ȣ�" + operation.progress);
            timer += Time.deltaTime;//��ʱ��
            if (operation.progress == 0.9f && timer >= 3.0)//������Ϻ� �� ����������� ����ת
            {
                Debug.Log("���س������");
                DontDestroyOnLoad(animatorLoading.gameObject);//�����³���ʱ�����ٹ�����������
                animatorLoading.SetTrigger("nextScene");//���Ź�����ʧ����
                operation.allowSceneActivation = true;//��ת���³���
            }
        }

    }

    private void toProducer()//ǰ����������������
    {

    }
    private void toNewGame()//��ʼ�µ���Ϸ
    {
        nextScene = true;
        Debug.Log("��ʼ���س���");
        animatorLoading.SetTrigger("nextScene");//���Ź�����ʼ����
        StartCoroutine(LoadScene());//ʹ���첽���س���
    }
    private IEnumerator LoadScene()//�첽���أ�ʹ��Э�̣�
    {
        operation = SceneManager.LoadSceneAsync("SceneState1");
        operation.allowSceneActivation = false;
        yield return operation;
    }
    private void toSettings()//ǰ����Ϸ���û���
    {

    }
    private void toMemory()//ǰ�����������ͻ���
    {

    }


}
