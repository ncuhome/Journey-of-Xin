using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//������Ϸ1��Բ�̹��� 
public class PlateOnThePost : MonoBehaviour
{
    private int[] rightKey = new int[] { 3, 0, 1, 2 };
    private int status = -1;//����״̬ -1������    0��ȡ��    1���ƶ�    2������
    private int aimX;//Ŀ������
    public GameObject Canvas;
    private Plate[] plateList = new Plate[4];
    private Plate getPlate;//��ǰץס������

    #region �����߼������봦��
    public void setPillar0()//�������0
    {
        if (status == -1)
        {
            if(getPlate == null)
            {
                getTopPlate(0);
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(0);
                aimX = 0;
                status = 1;
            }
        }
    }
    public void setPillar1()//�������1
    {
        if (status == -1)
        {
            if (getPlate == null)
            {
                getTopPlate(1);
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(1);
                aimX = 1;
                status = 1;
            }
        }
    }
    public void setPillar2()//�������2
    {
        if (status == -1)
        {
            if (getPlate == null)
            {
                getTopPlate(2);                                   
                if (getPlate != null)
                {
                    status = 0;
                }
            }
            else
            {
                getPlate.index = getMaxIndex(2);
                aimX = 2;
                status = 1;
            }
        }
    }
    private void getTopPlate(int x)//��ȡһ�������ϵĶ���Բ��
    {
        getPlate = null;
        for(int i=0;i<4;i++)
        {
            if (plateList[i].x == x)
            {
                if (getPlate == null)
                {
                    getPlate = plateList[i];
                }
                else if(getPlate.index < plateList[i].index)
                {
                    getPlate = plateList[i];
                }
            }
        }
    }
    private int getMaxIndex(int x)//��ȡһ�������ϵ���ߵ�index+1
    {
        int index = 0;
        for(int i=0;i<4;i++)
        {
           // Debug.Log("[" + i + "]" + plateList[i].x + ";");
            if (plateList[i] != getPlate && plateList[i].x == x )
            {
                index++;
            }
        }
        return index;
    }
    public void Cancel()//�˳�������Ƿ�ɹ�����
    {
        InputManager.Instance.sceneState = SceneState.MainScene;
        EventSystem.Instance.ActiveEvent(30);//���С��Ϸ1
        EventSystem.Instance.ActiveEvent(27);//�����Ի��ڵ�2
        SceneManager.UnloadSceneAsync(14);//ж�ر�����
    }
    private bool isCompleteve()//�ж��Ƿ������Ϸ
    {
        for(int i=0;i<4;i++)
        {
            if (plateList[i].x != 1 || plateList[i].index != rightKey[i] )
            {
                return false;
            }
        }
        return true;
    }

    #endregion �����߼������봦��

    #region ������ʾ

    private void Moving()//�����ƶ����ӵĶ���
    {
        switch(status)
        {
            case 0://ȡ������
                Debug.Log(getPlate == null);
                Vector3 vector = new Vector3(0, 400 - getPlate.plateObject.transform.localPosition.y, 0);
                if (vector.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector;
                    status = -1;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector / 5;
                }
                break;
            case 1:
                Vector3 vector1 = new Vector3((aimX*600-600)- getPlate.plateObject.transform.localPosition.x, 0,0);
                if (vector1.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector1;
                    status = 2;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector1 / 5;
                }
                break;
            case 2:
                Vector3 vector2 = new Vector3(0,(-400+getPlate.index*100) - getPlate.plateObject.transform.localPosition.y, 0);
                if (vector2.magnitude < 10)
                {
                    getPlate.plateObject.transform.localPosition += vector2;
                    getPlate.x = aimX;
                 //   Debug.Log(getPlate.index);
                    getPlate = null;
                    status = -1;
                }
                else
                {
                    getPlate.plateObject.transform.localPosition += vector2 / 5;
                }
                break;
        }
    }
    #endregion ������ʾ

    void Start()
    {
        for(int i=0;i<4;i++)
        {
            plateList[i] = new Plate(0,i,GameObject.Find("Canvas/Backboard/"+i),"Plate00"+i);//����Ϸ�����װ���б���
        }
    }
    private void OnEnable()
    {
        Canvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        Moving();
        if(Input.GetButtonDown("Cancel"))
        {
            Cancel();
        }
    }

    private class Plate//������λ������Ϸ�����װ��һ�����
    {
        public int x, index;//�������ڵ����ӣ������ϵ�λ��
        public string name;
        public GameObject plateObject;//��Ϸ����
        public Plate(int x, int index, GameObject plateObject,string name)
        {
            this.x = x;
            this.index = index;
            this.plateObject = plateObject;
            this.name = name;
        }
    }
}
