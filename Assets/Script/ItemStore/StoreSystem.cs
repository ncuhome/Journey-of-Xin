using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ϵͳ��һ����̬ϵͳ
/// </summary>
public class StoreSystem : MonoBehaviour
{
    public static StoreSystem Instance { get; private  set; }
    private int[] store = new int[24];

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    /// <summary>
    /// ��ȡ��������Ʒ��id��
    /// </summary>
    /// <returns>������ȫ����Ʒ����id����</returns>
    public int[] IdAll()
    {
        return store;
    }
    /// <summary>
    /// ���ñ�����ƷΪһ��������ĸ���
    /// </summary>
    /// <param name="idList">������</param>
    public void SetStore(int[] idList)
    {
        store = (int[])idList.Clone();
    }
    /// <summary>
    /// �򱳰�������µ���Ʒ����ӳɹ����� true
    /// </summary>
    /// <param name="id">�����Ʒ�� id ֵ</param>
    /// <returns>��ӳ��������� bool ֵ</returns>
    public bool Add(int id)
    {
        for (int i = 0; i < 24; i++)
        {
            if (store[i] == 0)
            {
                store[i] = id;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// �Ƴ������ڵ�ָ�� id ��Ʒ ɾ���ɹ����� true
    /// </summary>
    /// <param name="id">����ɾ����ָ����Ʒidֵ</param>
    /// <returns>��ӳ��������� bool ֵ</returns>
    public bool Remove(int id)
    {
        bool change = false;//��������
        for (int i = 0; i < 24; i++)
        {
            if (store[i] == id)
            {
                store[i] = 0;
                change = true;
            }
            if (change && i < 23)//����Ѿ�������������һ�Ϊ��
            {
                store[i] = store[i + 1];
                store[i + 1] = 0;
            }
        }
        return change;
    }
    /// <summary>
    /// ���ұ������Ƿ��и���Ʒ
    /// </summary>
    /// <param name="id">��ѯĿ��� id ֵ</param>
    /// <returns>��ӳ��ѯ��� bool ֵ</returns>
    public bool Find(int id)
    {
        for (int i = 0; i < 24; i++)
        {
            if (store[i] == id)
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        store = new int[24];
    }
}