using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**<summary>
 * ʹ�ù�������ǰ�������ö�����Ҫת��ĳ�������ֵ.
 * ���ڵ��ú����У�����LoadingCanvasԤ�Ƽ�.
 * ��Ҫ���Ŷ���ʱ��ֻ������LoadingCanvasԤ�Ƽ�.
 * LoadingCanvasԤ�Ƽ��ᶨʱ����.
 * </summary>
 */
public class LoadingScript : MonoBehaviour
{
    private static int scene;//����Ҫ���صĳ�������ֵ
    private AsyncOperation operation;//�첽�������
    public Animator animatorLoading;//���Ź���������
    private float timer = 0;//��ʱ��
    private int status = 0;//��������״̬ 0��׼�����Ž��뻭��  1��׼�����Ž�������
    public static int Scene { get; set; }
    void Start()
    {
        toNewScene();
    }


    void Update()
    {
        Debug.Log("�������ؽ��ȣ�" + operation.progress);
        timer += Time.deltaTime;//��ʱ��

        if (InputManager.Instance != null)
        {
            InputManager.Instance.sceneState = SceneState.Animation;
        }

        if (operation.progress == 0.9f && timer >= 3.0 && status == 0)//������Ϻ� �� ����������� ����ת
        {

            Debug.Log("���س������");
            DontDestroyOnLoad(animatorLoading.gameObject);//�����³���ʱ�����ٹ�����������
            status = 1;
            timer = 0;//�����ʱ��
            animatorLoading.SetTrigger("nextScene");//���Ź�����ʧ����
            operation.allowSceneActivation = true;//��ת���³���
        }
        else if (status == 1 && timer >= 5.0)//��ʱ���ټ��ض���
        {
            InputManager.Instance.sceneState = SceneState.MainScene;
            Destroy(this.gameObject);
        }
    }

    private void toNewScene()//�����µĳ������첽��
    {
        //animatorLoading.SetTrigger("nextScene");//���Ź�����������
        Debug.Log("��ʼ���س���");
        animatorLoading.SetTrigger("nextScene");//���Ź�����ʼ����
        StartCoroutine(LoadScene());//ʹ���첽���س���
    }

    private IEnumerator LoadScene()//�첽���أ�ʹ��Э�̣�
    {
        operation = SceneManager.LoadSceneAsync(Scene);
        operation.allowSceneActivation = false;
        yield return operation;
    }
}
