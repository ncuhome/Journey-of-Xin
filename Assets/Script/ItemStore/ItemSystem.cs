using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public string ItemDescription(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 1:return "����ͨ�Ŀ��ȣ���һ�������ڡ����򡯵�����ֲ������Ӽӹ��Ƴɣ�ò�ƶԲ��������Ce��ʿͬ�����������ã�";
            case 2:return "��Ѫ��ɫ����ķ�ƣ������ͨ����ķ�ƾ�������ʱ��ķ��ͣ���Kens�����ĳ��ʱ���ǹ�����ר�������";
        }
        return "û���������";
    }
    public string ItemName(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 1:return "����";
            case 2:return "Ѫɫ��ķ��";
        }
        return "";
    }
    public Sprite ItemSprite(int id)//��ȡ��Ʒ����ͼ
    {
        switch(id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 1: return Resources.Load<Sprite>("Image/Item/����");
            case 2: return Resources.Load<Sprite>("Image/Item/��ķ��");
        }
        return Resources.Load<Sprite>("Image/Item/������ͼ");
    }
}
