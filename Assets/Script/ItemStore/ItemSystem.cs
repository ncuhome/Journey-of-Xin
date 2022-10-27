using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//��ȡ��Ʒ������
    {
        switch (id)
        {
            case 0: return "ʲô��û��Ŷ~";
            case 1: return "ce�Ĵ��ұ�����������ʶ���渴��ce";
            case 2: return "�������Ʒ���������������";
            case 3: return "��˵�ǵ�������ʹ�õĽ������ߣ����ڴ�ָce�ļ�����Ϸ�лָ������ҩƷ�����ܣ������Ч";
            case 4: return "��˵�ǵ�������ʹ�õĽ������ߣ����ڴ�ָce�ļ�����Ϸ��ʧȥ�����ҩƷ�����ܣ������Ч";
            case 5: return "��ǿ�����ţ�Ч������ͨ�ŵ��ڱ�������ʹ��";
            case 6: return "��ǿ�����ʣ�Ч������ͨ�ʵ��ڱ�������ʹ��";
            case 7: return "����ɿڵĿ��ȣ���������";
            case 8: return "����ɿڵĿ��ȣ��������磬�������";
            case 9: return "����ɿڵĿ��ȣ��������磬�������";
            case 10: return "Σ�յĿ��ȣ������������";
            case 11: return "Σ�յĿ��ȣ������������";
            case 12: return "����ĳ����Ϣ�ķ��ţ�������ĳ�������������";
            case 13: return "ӵ�����ͻ�������������վ�ĸ�������";
            case 14: return "�������ô�ƺ��أ��������ң��Ǹ��������˵�������һ��������ģ���ʵ���Ǵ�����������ѡ������ҵļ�����Ϸ�ˣ�����Ͱɣ��������Ż��������ô����";
            case 15: return "��С�ݵ��ʣ�������ʧȥ�������䣬��ǿ������";
            case 16: return "����ĳ����Ϣ�ķ��ţ�������ĳ�������������";
            case 17: return "ce�Ĵ��ұ�����������ʶ���渴��ce���������ڱ���װ����kown��";
            case 18: return "ԭ�Ӳ��ȶ��ľ��壬�����ͷ����������׷����˱�ը";
            case 19: return "ԭ�����н��ܵľ��壬�˷�Ӧʱ�ɿء���Ч";
            case 20: return "�������߷���۲�ľ�ͷ�����Դ����ڸ���";
            case 21: return "����������ĺ˷�Ӧ��";
            case 22: return "ce������ʶװ��ʱ�õ��ļ���װ�ã����Ը�װ������������װ��";
            case 23: return "��������ĺ˷�Ӧ�ѣ�װ���˸�Ч�ķ�Ӧ����";
            case 24: return "���ȶ��ĺ˷�Ӧ�ѣ��þ��˻ᷢ����ը";
            case 25: return "����ĳ����Ϣ�ķ��ţ�������ĳ�������������";
            case 26: return "����ĳ����Ϣ�ķ��ţ�������ĳ�������������";
            case 27: return "����ĳ����Ϣ�ķ��ţ�������ĳ�������������";
            case 28: return "����ɿڵĿ��ȣ��������� \n�ɿ��Ⱥϳ�";
            case 29: return "����ɿڵĿ��ȣ��������磬������� \n�ɿ������źϳ�";
            case 30: return "����ɿڵĿ��ȣ��������磬������� \n�ɿ������ʺϳ�";
            case 31: return "����ɿڵĿ��ȣ��������磬������� \n�ɿ�������������źϳ�";
            case 32: return "����ɿڵĿ��ȣ��������磬������� \n�ɿ�����ɴ�����ʺϳ�";
            case 33: return "Σ�յĿ��ȣ������������ \n�ɿ����������źϳ�";
            case 34: return "Σ�յĿ��ȣ������������ \n�ɿ����������ʺϳ�";
            case 35: return "ce�Ĵ��ұ�����������ʶ���渴��ce���������ڱ���װ����kown�� \n�ɸ���װ�ã�Ce������΢�ӵ�����";
            case 36: return "����ĳ����Ϣ�ķ��ţ�������ĳ������������� \n�ɸ���װ�ã�Ce����͸�Ӿ�ͷ�ϳ�";
            case 37: return "����ĳ����Ϣ�ķ��ţ�������ĳ������������� \n�ɸ���װ�ã�Kown����͸�Ӿ�ͷ�ϳ�";
            case 38: return "���ȶ��ĺ˷�Ӧ�ѣ��þ��˻ᷢ����ը \n��ըҩԭ���Ϻϳ�";
            case 39: return "��������ĺ˷�Ӧ�ѣ�װ���˸�Ч�ķ�Ӧ���� \n��������ʯ���޾���Դ����װ�úϳ�";
        }
        return "û���������";
    }
    public static string ItemName(int id)//��ȡ��Ʒ������
    {
        switch (id)
        {
            case 0: return "";
            case 1: return "����װ�ã�Ce��";
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
            case 16: return "����������������";
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
            case 28: return "���ݺõĿ���";
            case 29: return "���俧��";
            case 30: return "ʧ�俧��";
            case 31: return "���俧��";
            case 32: return "ʧ�俧��";
            case 33: return "���Ļ��俧��";
            case 34: return "����ʧ�俧��";
            case 35: return "����װ��(Kown)";
            case 36: return "����װ������������";
            case 37: return "����װ������������";
            case 38: return "ըҩ";
            case 39: return "�޾���Դ";
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
            case 8: return Resources.Load<Sprite>("Image/Item/���俧��");
            case 9: return Resources.Load<Sprite>("Image/Item/ʧ�俧��");
            case 10: return Resources.Load<Sprite>("Image/Item/���Ļ��俧��");
            case 11: return Resources.Load<Sprite>("Image/Item/����ʧ�俧��");
            case 12: return Resources.Load<Sprite>("Image/Item/�ɴ���������");
            case 13: return Resources.Load<Sprite>("Image/Item/����");
            case 14: return Resources.Load<Sprite>("Image/Item/���������");
            case 15: return Resources.Load<Sprite>("Image/Item/�ɴ������");
            case 16: return Resources.Load<Sprite>("Image/Item/��������������");
            case 17: return Resources.Load<Sprite>("Image/Item/����װ�á�Know");
            case 18: return Resources.Load<Sprite>("Image/Item/ըҩԭ����");
            case 19: return Resources.Load<Sprite>("Image/Item/������ʯ");
            case 20: return Resources.Load<Sprite>("Image/Item/͸�Ӿ�ͷ");
            case 21: return Resources.Load<Sprite>("Image/Item/�޾���Դ����װ��");
            case 22: return Resources.Load<Sprite>("Image/Item/��΢�ӵ�����");
            case 23: return Resources.Load<Sprite>("Image/Item/�޾���Դ");
            case 24: return Resources.Load<Sprite>("Image/Item/ըҩ");
            case 25: return Resources.Load<Sprite>("Image/Item/��ʯ��������������");
            case 26: return Resources.Load<Sprite>("Image/Item/������������");
            case 27: return Resources.Load<Sprite>("Image/Item/����װ����������");
            case 28: return Resources.Load<Sprite>("Image/Item/���ݺõĿ���"); ;
            case 29: return Resources.Load<Sprite>("Image/Item/���俧��"); ;
            case 30: return Resources.Load<Sprite>("Image/Item/ʧ�俧��"); ;
            case 31: return Resources.Load<Sprite>("Image/Item/���俧��"); ;
            case 32: return Resources.Load<Sprite>("Image/Item/ʧ�俧��"); ;
            case 33: return Resources.Load<Sprite>("Image/Item/���Ļ��俧��"); ;
            case 34: return Resources.Load<Sprite>("Image/Item/����ʧ�俧��"); ;
            case 35: return Resources.Load<Sprite>("Image/Item/����װ�á�Know"); ;
            case 36: return Resources.Load<Sprite>("Image/Item/����װ����������"); ;
            case 37: return Resources.Load<Sprite>("Image/Item/����װ����������"); ;
            case 38: return Resources.Load<Sprite>("Image/Item/ըҩ"); ;
            case 39: return Resources.Load<Sprite>("Image/Item/�޾���Դ");
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
            case 28:
                return new int[] { 2 };
            case 29:
                return new int[] { 2, 3 };
            case 30:
                return new int[] { 2, 4 };
            case 31:
                return new int[] { 2, 14 };
            case 32:
                return new int[] { 2, 15 };
            case 33:
                return new int[] { 2, 5 };
            case 34:
                return new int[] { 2, 6 };
            case 35:
                return new int[] { 1, 22 };
            case 36:
                return new int[] { 1, 20 };
            case 37:
                return new int[] { 17, 20 };
            case 38:
                return new int[] { 18 };
            case 39:
                return new int[] { 19, 21 };
        }
        return new int[] { 0 };
    }

    public static int[] FormulaToItem(int id)
    {
        switch (id)
        {
            case 28:
                return new int[] { 7 };
            case 29:
                return new int[] { 8 };
            case 30:
                return new int[] { 9 };
            case 31:
                return new int[] { 8 };
            case 32:
                return new int[] { 9 };
            case 33:
                return new int[] { 10 };
            case 34:
                return new int[] { 11 };
            case 35:
                return new int[] { 17 };
            case 36:
                return new int[] { 1, 20, 27 };
            case 37:
                return new int[] { 17, 20, 27 };
            case 38:
                return new int[] { 24 };
            case 39:
                return new int[] { 23 };
        }
        return new int[] { 0 };
    }
}