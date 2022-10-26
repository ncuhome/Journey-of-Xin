using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//��ȡ��Ʒ������
    {
        switch (id)
        {
            case 1: return "û���������";
            case 2: return "���صĿ��ȣ������ر�֮��";
            case 3: return "��˵ĳ�������ϵ��������ô�����ϵĹ��ߴ�����Ϣ";
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
            case 14: return "�������ô�ƺ��أ��������ң��Ǹ��������˵�������һ��������ģ���ʵ���Ǵ�����������ѡ������ҵļ�����Ϸ�ˣ�����Ͱɣ��������Ż��������ô����";
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
        return "û���������";
    }
    public static string ItemName(int id)//��ȡ��Ʒ������
    {
        switch (id)
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
        switch (id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 1: return Resources.Load<Sprite>("Image/Item/����װ�á���ʿ");
            case 2: return Resources.Load<Sprite>("Image/Item/����");
            case 3: return Resources.Load<Sprite>("Image/Item/��");
            case 4: return Resources.Load<Sprite>("Image/Item/��");
            case 5: return Resources.Load<Sprite>("Image/Item/������");
            case 6: return Resources.Load<Sprite>("Image/Item/������");
            case 7: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���");
            case 8: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���");
            case 9: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���");
            case 10: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���");
            case 11: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���");
            case 12: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 13: return Resources.Load<Sprite>("Image/Item/����");
            case 14: return Resources.Load<Sprite>("Image/Item/���������");
            case 15: return Resources.Load<Sprite>("Image/Item/�ɴ������");
            case 16: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 17: return Resources.Load<Sprite>("Image/Item/����װ�á�Know");
            case 18: return Resources.Load<Sprite>("Image/Item/ըҩԭ����");
            case 19: return Resources.Load<Sprite>("Image/Item/������ʯ");
            case 20: return Resources.Load<Sprite>("Image/Item/͸�Ӿ�ͷ");
            case 21: return Resources.Load<Sprite>("Image/Item/�޾���Դ����װ��");
            case 22: return Resources.Load<Sprite>("Image/Item/��΢�ӵ�����");
            case 23: return Resources.Load<Sprite>("Image/Item/�޾���Դ");
            case 24: return Resources.Load<Sprite>("Image/Item/ըҩ");
            case 25: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 26: return Resources.Load<Sprite>("Image/Item/����Ʒ");
            case 27: return Resources.Load<Sprite>("Image/Item/����Ʒ");
        }
        return Resources.Load<Sprite>("Image/Item/������ͼ");
    }
    public static bool MakeFormula(int id, int[] store)//�ж��Ƿ�ɺϳɸ���Ʒ ��Ʒ�䷽id ������Ʒ��
    {
        foreach (int item in FormulaItem(id))
        {
            if (!StoreSystem.Instance.Find(item)) { return false; }
        }
        return true;
    }
    public static int[] FormulaItem(int id)//��ȡ�ϳɸ���Ʒ�� �䷽����id��
    {
        switch (id)
        {
            case 7:
                return new int[] { 2 };
            case 8:
                return new int[] { 2, 3 };
            case 9:
                return new int[] { 2, 4 };
            case 10:
                return new int[] { 2, 5 };
            case 11:
                return new int[] { 2, 6 };
            case 17:
                return new int[] { 1, 22 };
            case 24:
                return new int[] { 18 };
            case 23:
                return new int[] { 19, 21 };
        }
        return new int[] { 0 };
    }
}