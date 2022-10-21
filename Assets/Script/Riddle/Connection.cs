using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    private int stability = 2;//�ȶ���
    private float timer = 0;//��ʱ��
    public GameObject Canvas;
    public GameObject Collider;//������
    public GameObject BarF1, BarF2;//�ȶ��� ʱ����
    public GameObject EndL,EndS;//������ť
    private Block[] blockList;
    private Block cursor;//��ǰѡ�еķ���
    private int index;//������������ֵ

    private bool stop = false;
    private bool timerStart = false;
    // Start is called before the first frame update
  
    private void ResetData()//��������
    {
        //���õ�ͼ
        Canvas.SetActive(true);
        for (int i=0;i<blockList.Length;i++)
        {
            blockList[i].color = -1;
            blockList[i].status = 0;
            blockList[i].style = 1; 
        }
        blockList[15].style = 0;   blockList[15].color = 0;
        blockList[19].style = 0;   blockList[19].color = 0;
        blockList[18].style = 0;   blockList[18].color = 1;
        blockList[34].style = 0;   blockList[34].color = 1;
        blockList[24].style = 0;   blockList[24].color = 2;
        blockList[26].style = 0;   blockList[26].color = 2;
        blockList[33].style = 0;   blockList[33].color = 3;
        blockList[41].style = 0;   blockList[41].color = 3;
        blockList[36].style = 0;   blockList[36].color = 4;
        blockList[40].style = 0;   blockList[40].color = 4;
        //�������оƬ�ȶ���
        timer = 0;
        BarF2.GetComponent<Image>().transform.localPosition = new Vector3(0, -450 * (timer / 20), 0);
        BarF2.GetComponent<Image>().transform.localScale = new Vector3(1, (1 - timer / 20f), 0);
        EndL.SetActive(false);
        EndS.SetActive(false);
        stop = false;
    }

    #region ���ú���
    private void Awake()
    {
        blockList = new Block[49];

        for (int i = 0; i < blockList.Length; i++)
        {
            blockList[i] = new Block();
            blockList[i].blockObject = GameObject.Find("Canvas/Map/Block (" + i + ")");//��������
        }
        Canvas.SetActive(true);
    }
    private void OnEnable()//�ٴμ����
    {
        ResetData();
        UpdateSprite();
    }
    private void Update()
    {
        if(timerStart)
        {
            if (stability == 0)//���ȶ�ʱ����ʼ��ʱ
            {
                timer += Time.deltaTime;
                if (timer > 20) { End(); }
                BarF2.GetComponent<Image>().transform.localPosition = new Vector3(0, -450 * (timer / 20), 0);
                BarF2.GetComponent<Image>().transform.localScale = new Vector3(1, (1 - timer / 20f), 0);
            }
        }
        BarF1.GetComponent<Image>().transform.localPosition = new Vector3(0,-150*(3-stability),0);
        BarF1.GetComponent<Image>().transform.localScale = new Vector3(1,  ( stability / 3f), 0);
        if (Input.GetButtonDown("Cancel")){ Cancel(); }
    }
    #endregion

    #region �������
    public void MouseDown(float mx, float my)//��갴���ж�
    {
        if (stop) { return; }
        timerStart = true;
        // Debug.Log("���λ�ã�X" + mx + "  Y:" + my);
        index = InsideBlock(mx, my);//��ȡ������������
        if(index > -1 && index <49)
        {
            Debug.Log("��ǰλ�ã�"+index);
            if (blockList[index].color > -1)
            {
                if(index == 15 || index == 19 || index == 18 || index == 34 || index == 24
                    || index == 26 || index == 33 || index == 41 || index == 36 || index == 40)
                {

                    Debug.Log("׼���������");
                    if (Surrd(index))
                    {
                        Debug.Log("�������");
                        clearCp();//�����ǰ��·
                        UpdateSprite();
                        index = -1;
                        return;
                    }
                }
                cursor = blockList[index];//��ȡץȡ�ķ���
            }
            else
            {
                index = -1;
            }
        }
        else
        {
            index = -1;
        }
    }
    public void MouseUp()//����ɿ�
    {
        if (stop) { return; }
        cursor = null;
        index = -1;
        toEnd();
    }
    public void MouseDrag(float mx, float my)//����϶�
    {
        if (stop) { return; }
        if (index > -1)//���Ѿ�ѡ�з���ʱ
        {
            int will = InsideBlock(mx, my);//��ȡ��ǰ������ڷ�������
            Debug.Log("Ŀ��λ�ã�"+will+"   ��ǰλ�ã�"+index );
            if(index/7 > 0)//�������鲻������
            {
                if(will == index - 7)
                {
                    Debug.Log("��");
                    Up();
                }
            }
            if(index/7 < 6)//�������鲻������
            {
                if(will == index + 7)
                {
                    Down();
                }
            }
            if(index%7 > 0)//���뷽�鲻������
            {
                if(will == index -1)
                {
                    Left();
                }
            }
            if(index%7 < 6)//�������鲻������
            {
                if(will == index + 1)
                {
                    Right();
                }
            }
        }
    }
    #endregion
    #region ����
    private void clearCp()//�����·
    {
        int nowIndex = index;//��ǰ����ֵ
        int direction = -1;//0�� 1�� 2�� 3��
        Activation(index, 0);

        #region ��ʼ����
        bool clear = false;
        if (nowIndex / 7 > 0)//��
        {
            if (blockList[nowIndex - 7].status == 2)//��
            {
                nowIndex -= 7;
                direction = 3;
                clear = true;
            }
            else if (blockList[nowIndex - 7].status == 3)//��
            {
                nowIndex -= 7;
                direction = 2;
                clear = true;
            }
            else if (blockList[nowIndex - 7].status == 6)//��
            {
                nowIndex -= 7;
                direction = 0;
                clear = true;
            }

        }
        if (nowIndex / 7 < 6 && !clear)//��
        {
            if (blockList[nowIndex + 7].status == 1)//��
            {
                nowIndex += 7;
                direction = 3;
                clear = true;
            }
            else if (blockList[nowIndex + 7].status == 4)//��
            {
                nowIndex += 7;
                direction = 2;
                clear = true;
            }
            else if (blockList[nowIndex + 7].status == 6)//��
            {
                nowIndex += 7;
                direction = 1;
                clear = true;
            }

        }
        if (nowIndex % 7 > 0 && !clear)//��
        {
            if (blockList[nowIndex - 1].status == 1)//��
            {
                nowIndex--;
                direction = 0;
                clear = true;
            }
            else if (blockList[nowIndex - 1].status == 2)//��
            {
                nowIndex--;
                direction = 1;
                clear = true;
            }
            else if (blockList[nowIndex - 1].status == 5)//��
            {
                nowIndex--;
                direction = 2;
                clear = true;
            }

        }
        if (nowIndex % 7 < 6 && !clear)//��
        {
            if (blockList[nowIndex + 1].status == 3)//��
            {
                nowIndex++;
                direction = 1;
                clear = true;
            }
            else if (blockList[nowIndex + 1].status == 4)//��
            {
                nowIndex++;
                direction = 0;
                clear = true;
            }
            else if (blockList[nowIndex + 1].status == 5)//��
            {
                nowIndex++;
                direction = 3;
                clear = true;
            }

        }

        #endregion
        while (true)
        {
            switch (direction)
            {
                case 0://����ת
                    if (nowIndex / 7 == 0)
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex-7].color == blockList[nowIndex].color && blockList[nowIndex - 7].style == 1)//��ɫ��ͬ
                    {
                        //��ȡ��һ�εķ���
                        if (blockList[nowIndex - 7].status == 2)//��
                        {
                            direction = 3;
                        }
                        else if (blockList[nowIndex - 7].status == 3)//��
                        {
                            direction = 2;
                        }
                        else if (blockList[nowIndex - 7].status == 6)//��
                        {
                            direction = 0;
                        }
                        //�����ǰ��ɫ���ƶ�����λ��
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex -= 7;
                    }
                    else//��ɫ��ͬ����ֹ
                    {
                        //��������ǰ��ɫ
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 1://����ת
                    if (nowIndex / 7 == 6)
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex + 7].color == blockList[nowIndex].color && blockList[nowIndex + 7].style == 1)//��ɫ��ͬ
                    {
                        //��ȡ��һ�εķ���
                        if (blockList[nowIndex + 7].status == 1)//��
                        {
                            direction = 3;
                        }
                        else if (blockList[nowIndex + 7].status == 4)//��
                        {
                            direction = 2;
                        }
                        else if (blockList[nowIndex + 7].status == 6)//��
                        {
                            direction = 1;
                        }
                        //�����ǰ��ɫ���ƶ�����λ��
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex += 7;
                    }
                    else//��ɫ��ͬ����ֹ
                    {
                        //��������ǰ��ɫ
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 2://����ת
                    if (nowIndex % 7 == 0) 
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex - 1].color == blockList[nowIndex].color && blockList[nowIndex - 1].style == 1)//��ɫ��ͬ
                    {
                        //��ȡ��һ�εķ���
                        if (blockList[nowIndex - 1].status == 1)//��
                        {
                            direction = 0;
                        }
                        else if (blockList[nowIndex - 1].status == 2)//��
                        {
                            direction = 1;
                        }
                        else if (blockList[nowIndex - 1].status == 5)//��
                        {
                            direction = 2;
                        }
                        //�����ǰ��ɫ���ƶ�����λ��
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex --;
                    }
                    else//��ɫ��ͬ����ֹ
                    {
                        //��������ǰ��ɫ
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case 3://����ת
                    if (nowIndex % 7 == 6) 
                    {

                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        return; 
                    }
                    if (blockList[nowIndex + 1].color == blockList[nowIndex].color && blockList[nowIndex + 1].style == 1)//��ɫ��ͬ
                    {
                        //��ȡ��һ�εķ���
                        if (blockList[nowIndex + 1].status == 3)//��
                        {
                            direction = 1;
                        }
                        else if (blockList[nowIndex + 1].status == 4)//��
                        {
                            direction = 0;
                        }
                        else if (blockList[nowIndex + 1].status == 5)//��
                        {
                            direction = 3;
                        }
                        //�����ǰ��ɫ���ƶ�����λ��
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        nowIndex++;
                    }
                    else//��ɫ��ͬ����ֹ
                    {
                        //��������ǰ��ɫ
                        blockList[nowIndex].status = 0;
                        blockList[nowIndex].color = -1;
                        direction = -1;
                    }
                    break;
                case -1:
                    return;
            }
        }
    }
    private int InsideBlock(float mx, float my)//��ȡ��굱ǰ���ڷ��������ֵ
    {
        int dx = 0;//��
        int startX = 435;//x��ʼ��������
        while (true)
        {
            startX += 150;
            if (startX > mx) { break; }
            dx++;
        }
        int dy = 0;//��
        int startY = 1065;//y��ʼ��������
        while (true)
        {
            startY -= 150;
            if (startY < my) { break; }
            dy++;
        }
        int indexC = dy * 7 + dx;//��������ֵ

       // Debug.Log("ץȡ����:" + index + "  X:" + dx + "  Y:" + dy);
        if (indexC > -1 && indexC < 49) { return indexC; }
        return -1;
    }
    private void UpdateSprite()//������ͼ����
    {
        for (int i = 0; i < blockList.Length; i++)
        {
            GameObject obj = blockList[i].blockObject;

            switch (blockList[i].color)
            {
                case -1:
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(1,1,1,1);
                    break;
                case 0://��
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(230 / 255f, 25 / 255f, 15 / 255f, 255 / 255f);
                    break;
                case 1://��
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(245 / 255f, 230 / 255f, 30 / 255f, 255 / 255f);
                    break;
                case 2://��
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(55 / 255f, 250 / 255f, 50 / 255f, 255 / 255f);
                    break;
                case 3://��
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(50 / 255f, 250 / 255f, 250 / 255f, 255 / 255f);
                    break;
                case 4://��
                    blockList[i].blockObject.transform.Find("Flash").GetComponent<Image>().color = new Color(180 / 255f, 50 / 255f, 250 / 255f, 255 / 255f);
                    break;

            }
            if (blockList[i].style == 0)//ΪоƬ
            {
                blockList[i].blockObject.GetComponent<Animator>().SetInteger("class", 0);
            }
            else//Ϊ���ӵ�
            {
                blockList[i].blockObject.GetComponent<Animator>().SetInteger("class", 1);
            }
            blockList[i].blockObject.GetComponent<Animator>().SetInteger("status", blockList[i].status);
        }
    }
    private void Activation(int aim,int active)//����/�ر�һ��оƬ(activeΪ0�ǷǼ���)
    {
        switch(aim)
        {
            case 15:
            case 19:
                blockList[19].status = blockList[15].status = active;
                break;
            case 18:
            case 34:
                blockList[18].status = blockList[34].status = active;
                break;
            case 24:
            case 26:
                blockList[24].status = blockList[26].status = active;
                break;
            case 33:
            case 41:
                blockList[33].status = blockList[41].status = active;
                break;
            case 36:
            case 40:
                blockList[36].status = blockList[40].status = active;
                break;

        }
    }
    private bool Surrd(int indexP)//�ж���Χ�Ƿ���ͬɫ����
    {
        int number = 0;
        if(indexP/7 > 0)//��
        {
            if(indexP - 7 != index)
            {
                if (blockList[indexP - 7].status == 2
                    || blockList[indexP - 7].status == 3
                    || blockList[indexP - 7].status == 6)
                {
                    if (blockList[indexP - 7].color == blockList[indexP].color) { number++; }
                   // Debug.Log("���нӿ�");
                }
            }
        }
        if(indexP/7 < 6)//��
        {
            if (indexP + 7 != index)
            {
                if (blockList[indexP + 7].status == 1
                || blockList[indexP + 7].status == 4
                || blockList[indexP + 7].status == 6)
                {
                    if (blockList[indexP + 7].color == blockList[indexP].color) { number++; }
                    //  Debug.Log("���нӿ�");
                }
            }
        }
        if(indexP%7 > 0)//��
        {
            if (indexP - 1 != index)
            {
                if (blockList[indexP - 1].status == 1
                || blockList[indexP - 1].status == 2
                || blockList[indexP - 1].status == 5)
                {
                    if (blockList[indexP - 1].color == blockList[indexP].color) { number++; }
                    //  Debug.Log("���нӿ�");
                }
            }
        }
        if(indexP%7 < 6)//��
        {
            if (indexP + 1 != index)
            {
                if (blockList[indexP + 1].status == 3
                || blockList[indexP + 1].status == 4
                || blockList[indexP + 1].status == 5)
                {
                    if (blockList[indexP + 1].color == blockList[indexP].color) { number++; }
                    // Debug.Log("���нӿ�");
                }
            }
        }
       // Debug.Log("NUmber:" + number);
        if(number >= 1)
        {
            return true;
        }
        return false;
    }

    private void Up()//��������
    {
        int aimIndex = index - 7;
        if(blockList[aimIndex].style == 1)//Ϊ���ӵ�
        {
           // Debug.Log("���������ϣ��ҷ�������Ϊ���ӵ�");
           // Debug.Log("Ŀ����ɫ��"+ blockList[aimIndex].color);
            if (blockList[aimIndex].color == -1)//Ŀ��λ��Ϊ����ɫ
            {
               // Debug.Log("���������ϣ��ҷ�������Ϊ���ӵ㣬�ҷ���Ŀ������ɫ");
                #region ���Ŀ��λ�÷������Ϣ
                blockList[aimIndex].status = 6;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region �ı����ӵ��״̬
                if (blockList[index].status == 5)
                {
                    if (index % 7 > 0)//������
                    {
                        if (blockList[index - 1].color == cursor.color)//������ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index - 1].style == 1)//���ӵ������������
                            {
                                if (blockList[index - 1].status == 1
                                    || blockList[index - 1].status == 2
                                    || blockList[index - 1].status == 5)
                                {
                                    cursor.status = 4;//��������������
                                }
                            }
                            else//оƬ�޷���Ҫ��
                            {
                                cursor.status = 4;//��������������
                            }
                        }
                    }
                    if (index % 7 < 6)//����ұ�
                    {
                        if (blockList[index + 1].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index + 1].style == 1)//���ӵ������������
                            {
                                if (blockList[index + 1].status == 3
                                    || blockList[index + 1].status == 4
                                    || blockList[index + 1].status == 5)
                                {
                                    cursor.status = 1;//��������������
                                }

                            }
                            else
                            {
                                cursor.status = 1;//��������������
                            }

                        }
                    }
                }
                #endregion

                #region ����ѡ��ķ���
                index -= 7;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//ΪоƬ��ͬ��ɫ��
        {

            #region ���Ŀ��λ�÷������Ϣ
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//����оƬ
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region �ı����ӵ��״̬
            if (blockList[index].status == 5)
            {
                if (index % 7 > 0)//������
                {
                    if (blockList[index - 1].color == cursor.color)//������ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index - 1].style == 1)//���ӵ������������
                        {
                            if (blockList[index - 1].status == 1
                                || blockList[index - 1].status == 2
                                || blockList[index - 1].status == 5)
                            {
                                cursor.status = 4;//��������������
                            }
                        }
                        else//оƬ�޷���Ҫ��
                        {
                            cursor.status = 4;//��������������
                        }
                    }
                }
                if (index % 7 < 6)//����ұ�
                {
                    if (blockList[index + 1].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index + 1].style == 1)//���ӵ������������
                        {
                            if (blockList[index + 1].status == 3
                                || blockList[index + 1].status == 4
                                || blockList[index + 1].status == 5)
                            {
                                cursor.status = 1;//��������������
                            }

                        }
                        else
                        {
                            cursor.status = 1;//��������������
                        }

                    }
                }
            }
            #endregion

            #region ����ѡ��ķ���
            MouseUp();//�ͷŷ���
            #endregion
        }
        UpdateSprite();
    }
    private void Down()//��������
    {
        int aimIndex = index + 7;
        if (blockList[aimIndex].style == 1)//Ϊ���ӵ�
        {
            if (blockList[aimIndex].color == -1)//Ŀ��λ��Ϊ����ɫ
            {
                #region ���Ŀ��λ�÷������Ϣ
                blockList[aimIndex].status = 6;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region �ı����ӵ��״̬
                if (blockList[index].status == 5)
                {
                    if (index % 7 > 0)//������
                    {
                        if (blockList[index - 1].color == cursor.color)//������ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index - 1].style == 1)//���ӵ������������
                            {
                                if (blockList[index - 1].status == 1
                                    || blockList[index - 1].status == 2
                                    || blockList[index - 1].status == 5)
                                {
                                    cursor.status = 3;//��������������
                                }
                            }
                            else//оƬ�޷���Ҫ��
                            {
                                cursor.status = 3;//��������������
                            }

                        }
                    }
                    if (index % 7 < 6)//����ұ�
                    {
                        if (blockList[index + 1].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index + 1].style == 1)//���ӵ������������
                            {
                                if (blockList[index + 1].status == 3
                                    || blockList[index + 1].status == 4
                                    || blockList[index + 1].status == 5)
                                {
                                    cursor.status = 2;//��������������
                                }

                            }
                            else
                            {
                                cursor.status = 2;//��������������
                            }

                        }
                    }
                }
                
                #endregion

                #region ����ѡ��ķ���
                index += 7;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//ΪоƬ��ͬ��ɫ��
        {
            #region ���Ŀ��λ�÷������Ϣ
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//����оƬ
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region �ı����ӵ��״̬
            if (blockList[index].status == 5)
            {
                if (index % 7 > 0)//������
                {
                    if (blockList[index - 1].color == cursor.color)//������ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index - 1].style == 1)//���ӵ������������
                        {
                            if (blockList[index - 1].status == 1
                                || blockList[index - 1].status == 2
                                || blockList[index - 1].status == 5)
                            {
                                cursor.status = 3;//��������������
                            }
                        }
                        else//оƬ�޷���Ҫ��
                        {
                            cursor.status = 3;//��������������
                        }

                    }
                }
                if (index % 7 < 6)//����ұ�
                {
                    if (blockList[index + 1].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index + 1].style == 1)//���ӵ������������
                        {
                            if (blockList[index + 1].status == 3
                                || blockList[index + 1].status == 4
                                || blockList[index + 1].status == 5)
                            {
                                cursor.status = 2;//��������������
                            }

                        }
                        else
                        {
                            cursor.status = 2;//��������������
                        }

                    }
                }
            }

            #endregion

            #region ����ѡ��ķ���
            MouseUp();//�ͷŷ���
            #endregion
        }
        UpdateSprite();
    }
    private void Left()
    {
        int aimIndex = index - 1;
        if (blockList[aimIndex].style == 1)//Ϊ���ӵ�
        {
            if (blockList[aimIndex].color == -1)//Ŀ��λ��Ϊ����ɫ
            {
                #region ���Ŀ��λ�÷������Ϣ
                blockList[aimIndex].status = 5;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region �ı����ӵ��״̬
                if (blockList[index].status == 6)
                {

                    if (index / 7 > 0)//����ϱ�
                    {
                        if (blockList[index - 7].color == cursor.color)//�ϱ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index - 7].style == 1)//���ӵ������������
                            {
                                if (blockList[index - 7].status == 2
                                    || blockList[index - 7].status == 3
                                    || blockList[index - 7].status == 6)
                                {
                                    cursor.status = 4;//��������������
                                }
                            }
                            else
                            {
                                cursor.status = 4;//��������������
                            }

                        }
                    }
                    if (index / 7 < 6)//����±�
                    {
                        if (blockList[index + 7].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index + 7].style == 1)//���ӵ������������
                            {
                                if (blockList[index + 7].status == 1
                                    || blockList[index + 7].status == 4
                                    || blockList[index + 7].status == 6)
                                {
                                    cursor.status = 3;//��������������
                                }
                            }
                            else
                            {
                                cursor.status = 3;//��������������
                            }
                        }
                    }
                }
                #endregion

                #region ����ѡ��ķ���
                index --;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//ΪоƬ��ͬ��ɫ��
        {
            #region ���Ŀ��λ�÷������Ϣ
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//����оƬ
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region �ı����ӵ��״̬
            if (blockList[index].status == 6)
            {

                if (index / 7 > 0)//����ϱ�
                {
                    if (blockList[index - 7].color == cursor.color)//�ϱ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index - 7].style == 1)//���ӵ������������
                        {
                            if (blockList[index - 7].status == 2
                                || blockList[index - 7].status == 3
                                || blockList[index - 7].status == 6)
                            {
                                cursor.status = 4;//��������������
                            }
                        }
                        else
                        {
                            cursor.status = 4;//��������������
                        }

                    }
                }
                if (index / 7 < 6)//����±�
                {
                    if (blockList[index + 7].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index + 7].style == 1)//���ӵ������������
                        {
                            if (blockList[index + 7].status == 1
                                || blockList[index + 7].status == 4
                                || blockList[index + 7].status == 6)
                            {
                                cursor.status = 3;//��������������
                            }
                        }
                        else
                        {
                            cursor.status = 3;//��������������
                        }
                    }
                }
            }
            #endregion

            #region ����ѡ��ķ���
            MouseUp();//�ͷŷ���
            #endregion
        }
        UpdateSprite();
    }
    private void Right()
    {
        int aimIndex = index + 1;
        if (blockList[aimIndex].style == 1)//Ϊ���ӵ�
        {
            if (blockList[aimIndex].color == -1)//Ŀ��λ��Ϊ����ɫ
            {
                #region ���Ŀ��λ�÷������Ϣ
                blockList[aimIndex].status = 5;
                blockList[aimIndex].color = cursor.color;
                #endregion

                #region �ı����ӵ��״̬
                if (blockList[index].status == 6)
                {
                    if (index / 7 > 0)//����ϱ�
                    {
                        if (blockList[index - 7].color == cursor.color)//�ϱ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index - 7].style == 1)//���ӵ������������
                            {
                                if (blockList[index - 7].status == 2
                                    || blockList[index - 7].status == 3
                                    || blockList[index - 7].status == 6)
                                {
                                    cursor.status = 1;//��������������
                                }
                            }
                            else
                            {
                                cursor.status = 1;//��������������
                            }
                        }
                    }
                    if (index / 7 < 6)//����±�
                    {
                        if (blockList[index + 7].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                        {
                            if (blockList[index + 7].style == 1)//���ӵ������������
                            {
                                if (blockList[index + 7].status == 1
                                    || blockList[index + 7].status == 4
                                    || blockList[index + 7].status == 6)
                                {
                                    cursor.status = 2;//��������������
                                }
                            }
                            else
                            {
                                cursor.status = 2;//��������������
                            }
                        }
                    }
                }
                #endregion

                #region ����ѡ��ķ���
                index++;
                cursor = blockList[index];
                #endregion
            }
        }
        else if (blockList[aimIndex].color == cursor.color)//ΪоƬ��ͬ��ɫ��
        {
            #region ���Ŀ��λ�÷������Ϣ
            if (!Surrd(aimIndex))
            {
                Activation(aimIndex, 1);//����оƬ
            }
            else
            {
                MouseUp();
                return;
            }

            #endregion

            #region �ı����ӵ��״̬
            if (blockList[index].status == 6)
            {

                if (index / 7 > 0)//����ϱ�
                {
                    if (blockList[index - 7].color == cursor.color)//�ϱ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index - 7].style == 1)//���ӵ������������
                        {
                            if (blockList[index - 7].status == 2
                                || blockList[index - 7].status == 3
                                || blockList[index - 7].status == 6)
                            {
                                cursor.status = 1;//��������������
                            }
                        }
                        else
                        {
                            cursor.status = 1;//��������������
                        }
                    }
                }
                if (index / 7 < 6)//����±�
                {
                    if (blockList[index + 7].color == cursor.color)//�ұ����ӷ���Ϊ��ͬ��ɫ
                    {
                        if (blockList[index + 7].style == 1)//���ӵ������������
                        {
                            if (blockList[index + 7].status == 1
                                || blockList[index + 7].status == 4
                                || blockList[index + 7].status == 6)
                            {
                                cursor.status = 2;//��������������
                            }
                        }
                        else
                        {
                            cursor.status = 2;//��������������
                        }
                    }
                }
            }
            #endregion

            #region ����ѡ��ķ���
            MouseUp();//�ͷŷ���
            #endregion
        }
        UpdateSprite();
    }
    private void Cancel()//�˳��ж���Ϸ
    {
        End();
    }
    public void Close()//�˳���Ϸ
    {
        Canvas.SetActive(false);
        gameObject.SetActive(false);
    }
    private void toEnd()//�ж��Ƿ��������ת����Ӧ���
    {
        if (blockList[15].status == 1 && blockList[19].status == 1 && blockList[18].status == 1
              && blockList[34].status == 1 && blockList[24].status == 1 && blockList[26].status == 1
              && blockList[33].status == 1 && blockList[41].status == 1 && blockList[36].status == 1
              && blockList[40].status == 1)//����оƬ�������������ж�
        {
            for (int i = 0; i < blockList.Length; i++)
            {
                if (blockList[i].status == 0)//����û�м���Ľڵ�ʱʧ��
                {
                    End();
                    return;
                }
            }
            //ȫ�������ϳɹ�
            EndSuccess();
            return;
        }
    }
    private void End()//ʧ���˳�
    {
        stop = true;
        timerStart = false;
        if (stability > 0)
        {
            stability--;
        }//оƬ��
       // Thread.Sleep(1500);
        EndL.SetActive(true);
    }
    private void EndSuccess()//�ɹ��˳������¼�¼���
    {
        stop = true;
        timerStart = false;
      //  Thread.Sleep(1500);
        EndS.SetActive(true);
    }
    #endregion
    private class Block//��Ԫ����
    {
        public GameObject blockObject = null;//��Ӧ����Ϸ����
        public int style = 0;//������� 0��оƬ  1�����ӵ�
        public int status = 0;//��ǰ��״̬ 00��оƬδ���� 01��оƬ������ ��
        public int color = -1;//��ɫ 0���� 1���� 2���� 3���� 4����  -1:����ɫ
    }
}
