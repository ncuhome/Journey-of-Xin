using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 1:return "����ͨ�Ŀ��ȣ���һ�������ڡ����򡯵�����ֲ������Ӽӹ��Ƴɣ�ò�ƶԲ��������Ce��ʿͬ�����������ã�";
            case 2:return "��Ѫ��ɫ����ķ�ƣ������ͨ����ķ�ƾ�������ʱ��ķ��ͣ���Kens�����ĳ��ʱ���ǹ�����ר�������";
        }
        return "û���������";
    }
    public static string ItemName(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 1:return "����";
            case 2:return "Ѫɫ��ķ��";
        }
        return "";
    }
    public static Sprite ItemSprite(int id)//��ȡ��Ʒ����ͼ
    {
        switch(id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 1: return Resources.Load<Sprite>("Image/Item/����");
            case 2: return Resources.Load<Sprite>("Image/Item/��ķ��");
        }
        return Resources.Load<Sprite>("Image/Item/������ͼ");
    }
    public static bool MakeFormula(int id, int[] store)//�ж��Ƿ�ɺϳɸ���Ʒ ��Ʒ�䷽id ������Ʒ��
    {
        switch(id)
        {
            case 2:
                if (StoreSystem.Find(1)) { return true; }
                return false;
        }
        return false;
    }
    public static int[] FormulaItem(int id)//��ȡ�ϳɸ���Ʒ�� �䷽����id��
    {
        switch(id)
        {
            case 2:
                return new int[] { 1 };
        }
        return new int[] { 0 };
    }
}
