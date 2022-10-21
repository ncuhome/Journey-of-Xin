using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AStroke : MonoBehaviour
{
    private float timer = 0;//��ʱ��
    private int[] blockList = new int[45];//0:��ɫ��   1����ɫ    2������ǽ   3��Դɫ�ʷ���
    private GameObject[] gameObjectList = new GameObject[45];
    public GameObject ASCanvas;//����
    public GameObject endImage;//��ֹ��ʾ��
    public GameObject text0;//ʧ����ʾ
    public GameObject text1;//�ɹ���ʾ
    public GameObject progressBar;//������
    public GameObject textTime;//ʱ��
    public GameObject blockOrigin;//Դ�鶯����
    private int index = 5;//��ǰ��ɫ�ƶ����λ��
    private bool start = false;//�ж��Ƿ���Ϸ��ʼ
    private bool stop = false;
    #region ���봦��&�����߼�


    private bool pick = false;
    public void MouseDown(float mx,float my)//��갴���ж�
    {
        float ix = gameObjectList[index].transform.position.x;
        float iy = gameObjectList[index].transform.position.y;
        Debug.Log("���λ�ã�X:" +  + mx+"  Y:" + my +"�ʿ�λ�ã�X:" + ix + "  Y:" + iy);
        if (mx >= ix-100 && mx <= ix+100 && my >= iy-100 && my<=iy+100)
        {
            pick = true;
            Debug.Log("ץס��");
        }
        else
        {
            pick = false;
        }
    }
    public void MouseUp()//����ɿ�
    {
        pick = false;
        blockOrigin.transform.position = new Vector3(-1500, 0, -50);
    }
    public void MouseDrag(float mx, float my)//����϶�
    {
        if (pick)
        {
            blockOrigin.transform.position = new Vector3(mx,my,-20);
            float ix = 0;
            float iy = 0;
            #region �ж�����
            if(index % 9 >0)
            {
                ix = gameObjectList[index - 1].transform.position.x;
                iy = gameObjectList[index - 1].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Left();
                    return;
                }
            }
            #endregion
            #region �ж�����
            if (index % 9 < 8)
            {
                ix = gameObjectList[index + 1].transform.position.x;
                iy = gameObjectList[index + 1].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Right();
                    return;
                }
            }
            #endregion
            #region �ж�����
            if(index / 9>0)
            {
                ix = gameObjectList[index - 9].transform.position.x;
                iy = gameObjectList[index - 9].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Up();
                    return;
                }
            }
            #endregion
            #region �ж�����
            if (index / 9 < 4)
            {

                ix = gameObjectList[index + 9].transform.position.x;
                iy = gameObjectList[index + 9].transform.position.y;
                if (Input.mousePosition.x >= ix - 100 && Input.mousePosition.x <= ix + 100 && Input.mousePosition.y >= iy - 100 && Input.mousePosition.y <= iy + 100)
                {
                    Down();
                    return;
                }
            }
            #endregion
        }
    }
    private void Up()//�����ƶ�Դ
    {
        start = true;
        if(index/9 > 0)//���ڵ�һ����ȥ·Ϊ��ɫ��
        {
            if(blockList[index - 9] == 0)
            {
                blockList[index] = 1;//��ǰλ�ø�Ϊ��ɫ
                index -= 9;
                blockList[index] = 3;//�ƶ���ɫԴ
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Left()//�����ƶ�Դ
    {
        start = true;
        if (index % 9 > 0)//���ڵ�һ����ȥ·Ϊ��ɫ��
        {
            if(blockList[index - 1] == 0)
            {
                blockList[index] = 1;//��ǰλ�ø�Ϊ��ɫ
                index--;
                blockList[index] = 3;//�ƶ���ɫԴ
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Down()//�����ƶ�Դ
    {
        start = true;
        if (index / 9 < 4)//���ڵ�һ����ȥ·Ϊ��ɫ��
        {
           if(blockList[index + 9] == 0)
           {
                blockList[index] = 1;//��ǰλ�ø�Ϊ��ɫ
                index += 9;
                blockList[index] = 3;//�ƶ���ɫԴ
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    private void Right()//�����ƶ�Դ
    {
        start = true;
        if (index % 9 < 8)//���ڵ�һ����ȥ·Ϊ��ɫ��
        {
            if(blockList[index + 1] == 0)
            {
                blockList[index] = 1;//��ǰλ�ø�Ϊ��ɫ
                index++;
                blockList[index] = 3;//�ƶ���ɫԴ
                UpdateSprite();
                if (Success()) { EndSuccess(); }
                else if (!Next()) { End(); }
            }
        }
    }
    public void Cancel()//ȡ�����˳�
    {
        ASCanvas.SetActive(false);
        this.gameObject.SetActive(false);
    }
    private void End()//��Ϸ������ʧ�ܣ�
    {
        stop = true;
        endImage.SetActive(true);
        text0.SetActive(true);
    }
    private void EndSuccess()//��Ϸ�������ɹ���
    {
        //EventSystem.changeStaticEvent(83,true);
        stop = true;
        endImage.SetActive(true);
        text1.SetActive(true);
    }
    private bool Next()//����Ƿ���Ϸ�ɼ�����δʧ�ܣ�
    {
        if (timer > 20) { return false; }//ʧ��
        else
        {
            if(index / 9 > 0)//���ڵ�һ��ʱ
            {
                if (blockList[index - 9] == 0)
                {
                    return true;
                }
            }
            if(index % 9 > 0)//���������ʱ
            {
                if (blockList[index-1] == 0)
                {
                    return true;
                }
            }
            if(index / 9 < 4)//�������²�
            {
                if (blockList[index+9] == 0)
                {
                    return true;
                }
            }
            if(index % 9 < 8)//�������ұ�
            {
                if (blockList[index+1] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool Success()//�����Ϸ�Ƿ�ɹ�
    {
        for(int i = 0; i < blockList.Length;i++)
        {
            if (blockList[i] == 0)
            {
                return false;
            }
        }
        return true;
    }
    private void UpdateSprite()//������ͼ
    {
        for(int i=0;i< gameObjectList.Length;i++)
        {
            try { Animator animator = gameObjectList[i].GetComponent<Animator>(); animator.SetInteger("status", blockList[i]); }
            catch (Exception e) { Debug.Log("����λ�ñ�ţ�"+i); }
            
        }
    }
    private void ResetData()//��������
    {
        ASCanvas.SetActive(true);
        timer = 0;
        index = 5;
        start = false;
        stop = false;
        for (int i = 0; i < blockList.Length; i++)
        {
            blockList[i] = 0;
        }
        blockList[0] = 2;
        blockList[5] = 3;
        UpdateSprite();
        endImage.SetActive(false);
        text0.SetActive(false);
        text1.SetActive(false);
    }
    #endregion
    private void Awake()
    {
        for (int i = 0; i < gameObjectList.Length; i++)
        {
            gameObjectList[i] = GameObject.Find("ASCanvas/Block" + i);//������Ϸ����
            blockOrigin.GetComponent<Animator>().SetInteger("status", 3);
        }
    }
    private void OnEnable()
    {
        ResetData();
    }
    void Start()
    {
    }
    void Update()
    {
        if(!stop)
        {
            if (start)
            {
                timer += Time.deltaTime;
                //Debug.Log("Time:" + timer);
                float progress = (20.0f - timer) / 20;
                textTime.GetComponent<TMP_Text>().text = (int)(20.0f - timer) + "s";
                progressBar.transform.localScale = new Vector3(progress,1,1);
                progressBar.transform.localPosition = new Vector3(750*(progress-1),0,0);
            }
            if (Input.GetButtonDown("Up"))
            {
                Up();
            }
            else if (Input.GetButtonDown("Down"))
            {
                Down();
            }
            else if (Input.GetButtonDown("Left"))
            {
                Left();
            }
            else if (Input.GetButtonDown("Right"))
            {
                Right();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
            if(timer > 20) { End(); }
        }
    }
}
