using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 1:return "û���������";
            case 2:return "���صĿ��ȣ������ر�֮��";
            case 3: return "��˵ĳ�������ϵ��������ô�����ϵĹ��ߴ�����Ϣ";
            case 4: return "��";
            case 5: return "������";
            case 6: return "������";
            case 7: return "���ݺõĿ���";
            case 8: return "���俧��";
            case 9: return "ʧ�俧��";
            case 10: return "���Ļ��俧��";
            case 11: return "����ʧ�俧��";
            case 12: return "û���������";
            case 13: return "û���������";
            case 14: return "û���������";
            case 15: return "û���������";
            case 16: return "û���������";
            case 17: return "û���������";
            case 18: return "û���������";
            case 19: return "û���������";
            case 20: return "û���������";
            case 21: return "û���������";
            case 22: return "û���������";
            case 23: return "û���������";
            case 24: return "û���������";
            case 25: return "û���������";
            case 26: return "û���������";
            case 27: return "û���������";

        }
        return "û���������";
    }
    public static string ItemName(int id)//��ȡ��Ʒ������
    {
        switch(id)
        {
            case 0: return "";
            case 1: return "����װ�ã���ʿ��";
            case 2: return "����";
            case 3: return "��";
            case 4: return "��";
            case 5: return "������";
            case 6: return "������";
            case 7: return "���ݺõĿ���";
            case 8: return "���俧��";
            case 9: return "ʧ�俧��";
            case 10: return "���Ļ��俧��";
            case 11: return "����ʧ�俧��";
            case 12: return "�ɴ�����������������";
            case 13: return "����";
            case 14: return "���������";
            case 15: return "�ɴ������";
            case 16: return "��������������";
            case 17: return "����װ�ã�Kown��";
            case 18: return "ըҩԭ����";
            case 19: return "������ʯ";
            case 20: return "͸�Ӿ�ͷ";
            case 21: return "�޾���Դ����װ��";
            case 22: return "��΢�ӵ�����";
            case 23: return "�޾���Դ";
            case 24: return "ըҩ";
            case 25: return "��ʯ��������������";
            case 26: return "��������������";
            case 27: return "����װ������������";
        }
        return "δ֪��Ʒ";
    }
    public static Sprite ItemSprite(int id)//��ȡ��Ʒ����ͼ
    {
        switch(id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 1: return Resources.Load<Sprite>("Image/Item/����װ�ã���ʿ��");
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
            case 7:
                return new int[] { 2 };
            case 8:
                return new int[] { 2,3 };
            case 9:
                return new int[] { 2,4 };
            case 10:
                return new int[] { 2,5 };
            case 11:
                return new int[] { 2,6 }; 
        }
        return new int[] { 0 };
    }
}
