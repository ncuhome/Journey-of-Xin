using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuaRong : MonoBehaviour
{
    private int[] square;
    private bool[] squareBool = new bool[16];
    private GameObject[] blockList;//��Ϸ�������
    private int move = 0;//0:�ȴ����� 1�������ƶ��׿��� 2�������ƶ� 3�������ƶ� 4�������ƶ�
    public GameObject Canvas;
    int cursor = 0;//��ǰ��꣨�ո񣩵�����ֵ

    private Vector3 oldPosition;//������
    //���½�Ϊ��㣬���Ͻ�Ϊ�յ�

    private void Awake()
    {
        blockList = new GameObject[16];
        for (int i = 0; i < 16; i++)
        {
            blockList[i] = GameObject.Find("Canvas/Block (" + i + ")");//��ʼ����������Ϸ����
        }
    }
    private void OnEnable()
    {
        Canvas.SetActive(true);
        square = new int[] { 0, 2, 3, 2, 2, 4, 6, 6, 1, 3, 6, 6, 5, 4, 1, 4 };
        for(int i=0;i<squareBool.Length;i++)
        {
            squareBool[i] = false;
        }
        UpdateSprite();
    }

    

    void Update()
    {
        switch(move)
        {
            case 0:
                if (isSuccess()) { Debug.Log("���ճɹ�����"); }
                if (Input.GetButtonDown("Up"))
                {
                    Down();
                }
                else if (Input.GetButtonDown("Down"))
                {
                    Up();
                }
                else if (Input.GetButtonDown("Left"))
                {
                    Right();
                }
                else if (Input.GetButtonDown("Right"))
                {
                    Left();
                }
                else if (Input.GetButtonDown("Cancel"))
                {
                    Cancel();
                }
                
                break;
            case 1://�׿������ƶ�
                blockList[cursor].transform.position += new Vector3(0, -Time.deltaTime*20, 0);//�����ƶ���
                if (blockList[cursor].transform.position.y <= oldPosition.y-200)//�ƶ���ָ����λ�ú�
                {
                    move = 0;//�ָ��ȴ�״̬
                    blockList[cursor].transform.position = oldPosition + new Vector3(0, -200, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor + 4];
                    blockList[cursor + 4] = gobj;
                }
                break;
            case 2://�׿������ƶ�
                blockList[cursor].transform.position += new Vector3(Time.deltaTime*20, 0, 0);//�����ƶ���
                if (blockList[cursor].transform.position.x >= oldPosition.x+200)//�ƶ���ָ����λ�ú�
                {
                    move = 0;//�ָ��ȴ�״̬
                    blockList[cursor].transform.position = oldPosition + new Vector3(200, 0, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor + 1];
                    blockList[cursor + 1] = gobj;
                }
                break;
            case 3://�׿������ƶ�
                blockList[cursor].transform.position += new Vector3(0, Time.deltaTime*20, 0);//�����ƶ���
                if (blockList[cursor].transform.position.y >= oldPosition.y+200)//�ƶ���ָ����λ�ú�
                {
                    move = 0;//�ָ��ȴ�״̬
                    blockList[cursor].transform.position = oldPosition + new Vector3(0, 200, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor - 4];
                    blockList[cursor - 4] = gobj;
                }
                break;
            case 4://�����ƶ��׿�
                blockList[cursor].transform.position += new Vector3(-Time.deltaTime*20, 0, 0);//�����ƶ���
                if (blockList[cursor].transform.position.x <= oldPosition.x-200)//�ƶ���ָ����λ�ú�
                {
                    move = 0;//�ָ��ȴ�״̬
                    blockList[cursor].transform.position = oldPosition + new Vector3(-200, 0, 0);
                    GameObject gobj = blockList[cursor];
                    blockList[cursor] = blockList[cursor - 1];
                    blockList[cursor - 1] = gobj;
                }
                break;


        }
       
    }
    private void UpdateSprite()//������ͼ
    {
        for (int i = 0; i < square.Length; i++)
        {
            if (squareBool[i])
            {
                blockList[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-HuaRong/Block" + i + "1");
            }
            else
            {
                blockList[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Riddle-HuaRong/Block" + i + "0");
            }
        }
    }
    private void Up()//�ո����Ͻ���
    {
        if (cursor > 3)//��겻�����
        {
            square[cursor] = square[cursor - 4];
            square[cursor - 4] = 0;
            cursor -= 4;
            oldPosition = blockList[cursor].transform.position;
            move = 1;
            UpdateSprite();
        }
    }

    private void Down()//�ո����½���
    {
        if (cursor < 12)//��겻�����·�
        {
            square[cursor] = square[cursor + 4];
            square[cursor + 4] = 0;
            cursor += 4;
            oldPosition = blockList[cursor].transform.position;
            move = 3;
            UpdateSprite();
        }
    }

    private void Left()//�ո����󽻻�
    {
        if (cursor % 4 != 0)//��겻�������
        {
            square[cursor] = square[cursor - 1];
            square[cursor - 1] = 0;
            cursor--;
            oldPosition = blockList[cursor].transform.position;
            move = 2;
            UpdateSprite();
        }
    }

    private void Right()//�ո����ҽ���
    {
        if (cursor % 4 != 3)//��겻�����ұ�
        {
            square[cursor] = square[cursor + 1];
            square[cursor + 1] = 0;
            cursor++;
            oldPosition = blockList[cursor].transform.position;
            move = 4;
            UpdateSprite();
        }
    }

    private void Cancel()//ȡ���� �˳�
    {
        Canvas.SetActive(false);
    }
    private bool isSuccess()//�ж��Ƿ���ճɹ�
    {
        for(int i=0;i<16;i++)
        {
            squareBool[i] = false;
        }
        int aimIndex = 12;//��ǰ����ֵ
        int from = 1;//���Է��� 0���� 1:�� 2���� 3����
        bool switchBoll = true;//ѭ������
        while (switchBoll)//���½ǣ����棩Ϊ��㣬���Ͻǣ����棩Ϊ�յ�
        {
            switch(from)
            {
                case 0://������
                    if(square[aimIndex] == 1)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 1;
                        if(aimIndex%4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else if(square[aimIndex] == 4)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 3;
                        if(aimIndex%4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else if(square[aimIndex] == 6)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 0;
                        if(aimIndex/4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex+=4;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 1://������
                    if (square[aimIndex] == 3)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 0;
                        if (aimIndex / 4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex += 4;
                    }
                    else if (square[aimIndex] == 4)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 2;
                        if(aimIndex/4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -=4;
                    }
                    else if (square[aimIndex] == 5)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 1;
                        if (aimIndex % 4 == 3)
                        {
                            if (aimIndex == 4)
                            {
                                switchBoll = false;
                            }
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 2://������
                    if (square[aimIndex] == 2)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 1;
                        if (aimIndex % 4 == 3)
                        {
                            if (aimIndex == 4)
                            {
                                switchBoll = false;
                            }
                            switchBoll = false;
                        }
                        aimIndex++;
                    }
                    else if (square[aimIndex] == 3)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 3;
                        if (aimIndex % 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else if (square[aimIndex] == 6)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 2;
                        if (aimIndex / 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -= 4;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
                case 3://������
                    if (square[aimIndex] == 1)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 2;
                        if (aimIndex / 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex -= 4;
                    }
                    else if (square[aimIndex] == 2)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 0;
                        if (aimIndex / 4 == 3)
                        {
                            switchBoll = false;
                        }
                        aimIndex += 4;
                    }
                    else if (square[aimIndex] == 5)//ת����
                    {
                        squareBool[aimIndex] = true;
                        from = 3;
                        if (aimIndex % 4 == 0)
                        {
                            switchBoll = false;
                        }
                        aimIndex--;
                    }
                    else
                    {
                        switchBoll = false;
                    }
                    break;
            }
        }
        UpdateSprite();
        if (blockList[3].GetComponent<Animator>().GetBool("active"))
        {
            if (square[3] == 1 || square[3] == 2 || square[3] == 5)
            {
                return true;
            }
        }
        return false;
    }
}
