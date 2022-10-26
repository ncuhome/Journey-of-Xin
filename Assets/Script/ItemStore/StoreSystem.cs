using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包系统，一个静态系统
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
    /// 获取背包内物品的id组
    /// </summary>
    /// <returns>背包内全部物品栏的id数组</returns>
    public int[] IdAll()
    {
        return store;
    }
    /// <summary>
    /// 设置背包物品为一个新数组的副本
    /// </summary>
    /// <param name="idList">新数组</param>
    public void SetStore(int[] idList)
    {
        store = (int[])idList.Clone();
    }
    /// <summary>
    /// 向背包内添加新的物品，添加成功返回 true
    /// </summary>
    /// <param name="id">添加物品的 id 值</param>
    /// <returns>反映操作结果的 bool 值</returns>
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
    /// 移除背包内的指定 id 物品 删除成功返回 true
    /// </summary>
    /// <param name="id">欲将删除的指定物品id值</param>
    /// <returns>反映操作结果的 bool 值</returns>
    public bool Remove(int id)
    {
        bool change = false;//交换开关
        for (int i = 0; i < 24; i++)
        {
            if (store[i] == id)
            {
                store[i] = 0;
                change = true;
            }
            if (change && i < 23)//如果已经开启交换且下一项不为空
            {
                store[i] = store[i + 1];
                store[i + 1] = 0;
            }
        }
        return change;
    }
    /// <summary>
    /// 查找背包内是否有该物品
    /// </summary>
    /// <param name="id">查询目标的 id 值</param>
    /// <returns>反映查询结果 bool 值</returns>
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