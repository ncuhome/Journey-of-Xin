using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IntList
{
    #nullable enable
    private string name = "SimpleList";//链表名称
    private bool log = false;//是否打印日志
    private int count = 0;//链表长度
    private ChainLink headLink;//头链结（头链结的索引值为-1）\
    private ChainLink? lastLink;//末链结\


    public int Count { get { return count; } }
    public string Name { get { return name; } set { name = value; } }
    public bool Log { get { return log; } set { log = value; } }

    public IntList()//初始化一个空链表
    {
        headLink = lastLink = new ChainLink();
    }
    public IntList(string name)
    {
        headLink = lastLink = new ChainLink();
        this.name = name;
    }
    private void Update()//更新链表数据(长度,末链结)
    {
        count = -1;
        ChainLink? nowLink = headLink;
        while (true)
        {
            if (nowLink != null)
            {
                count++;
                if (nowLink.Next != null)
                {
                    nowLink = nowLink.Next;
                }
                else
                {
                    lastLink = nowLink;
                    return;
                }
            }
        }
    }
    private ChainLink? FindLink(int index, bool supplement)
    {//获取指定索引值的链结，supplement 为true时：当索引的链结不存在时返回尾链结，而不是null
        if (count <= index || index < 0)//索引超出范围
        {
            return lastLink;
        }
        else//索引值在长度范围内
        {
            int nowIndex = -1;//当前遍历的索引值
            ChainLink? nowLink = headLink;
            while (index < nowIndex)
            {
                nowIndex++;
                if (nowLink != null)//向下遍历
                {
                    nowLink = nowLink.Next;
                }
                else//为空时说明出现异常，打印日志，中断循环
                {
                    if (log) { Console.WriteLine("[" + name + "]LinkErro:链表中存在空链结！"); }
                    break;
                }
            }
            if (nowLink != null) { return nowLink; }
            else
            {
                if (log) { Console.WriteLine("[" + name + "]LinkErro:链表中存在空链结！"); }
                return null;
            }
        }
    }
    public int FindIndex(int item)//查询指定对象在链表内的索引值（返回第一个符合的索引值）（返回-1则说明无符合对象）
    {
        int nowIndex = -1;
        ChainLink? nowLink = headLink;
        while (true)
        {
            if (nowLink != null)//向下遍历
            {
                if (nowLink.Item == item) { return nowIndex; }
                nowLink = nowLink.Next;
            }
            else
            {
                return -1;
            }
        }
    }

    public void Add(int item)//从尾部添加一个新的链结（包含一个新元素不为null）
    {
        ChainLink newLink = new ChainLink(item, lastLink);//在尾链结后添加一个新的链结
        lastLink = newLink;
        Update();
    }
    public bool AddInside(int item, int index)//将一个元素插入到指定索引值后面（超出长度时则添加至末尾，并返回是否执行成功
    {
        if (index < 0)
        {
            if (log) { Console.WriteLine("[" + name + "]LinkErro:存在非法插入！"); }
            return false;
        }//如果索引值<0则不执行插入
        ChainLink? chainLink = FindLink(index, true);//获取指定链结
        if (chainLink != null)
        {
            new ChainLink(item, chainLink);
            Update();
            return true;
        }
        else
        {
            if (log) { Console.WriteLine("[" + name + "]LinkErro:存在非法插入！链结不存在！"); }
            return false;
        }
    }


    public int Get(int index, bool supplement)//获取指定索引值的元素，supplement 为true时：当索引的链结不存在时返回最后一个元素，而不是null
    {
        ChainLink? nowLink = FindLink(index, supplement);
        if (nowLink != null) { return nowLink.Item; }
        return 0;
    }

    public bool RemoveIndex(int index)//移除指定元素
    {
        ChainLink? chainLink = FindLink(index, false);
        if (chainLink != null)
        {
            chainLink.BreakOff(true);
            Update();
            return true;
        }
        return false;
    }
    public bool RemoveF(int item)//移除指定元素
    {
        ChainLink? nowLink = headLink;
        while (true)
        {
            if (nowLink != null)//向下遍历
            {
                if (nowLink.Item == item)
                {
                    nowLink.BreakOff(true);
                    Update();
                    return true;
                }
                nowLink = nowLink.Next;
            }
            else
            {
                return false;
            }
        }
    }

    public int this[int index]//使用索引器对链结内的元素进行获取和变更
    {
        get { return Get(index, false); }
        set
        {
            ChainLink? chainLink = FindLink(index, false);
            if (chainLink != null)
            {
                chainLink.Item = value;
            }
        }
    }

    public int this[int index, bool supplement]//使用索引器对链结内的元素进行获取和变更,supplement 为true时：当索引的链结不存在时返回最后一个元素，而不是null
    {
        get { return Get(index, supplement); }
        set
        {
            ChainLink? chainLink = FindLink(index, false);
            if (chainLink != null)
            {
                chainLink.Item = value;
            }
        }
    }


    //链结类：用于存储对象，并与其他链结相连
    private class ChainLink
    {
        private ChainLink? last = null;//上一个链结
        private ChainLink? next = null;//下一个链接
        private int item;//所储存的对象

        public ChainLink? Last { get { return last; } }
        public ChainLink? Next { get { return next; } }
        public int Item { get { return item; } set { item = value; } }

        public ChainLink() { }//声明头链结（不储存元素）
        public ChainLink(int item)//声明一个链结并与对象连接
        {
            this.item = item;
        }
        public ChainLink(int item, ChainLink? lastLink)//声明一个链结之后的链结（断口修复）
        {
            this.item = item;
            ConnectLast(lastLink);
        }

        public void ConnectLast(ChainLink? lastLink)//插入到指定链结的尾部（覆盖连接）（断口修复）
        {
            if (lastLink != null)
            {
                ChainLink? nextLink = lastLink.next;
                this.last = lastLink;//链结上一个链结
                lastLink.next = this;

                if (nextLink != null)//连接下一个链结
                {
                    this.next = nextLink;
                    nextLink.last = this;
                }
                return;
            }
            this.last = null;
        }
        public void ConnectNext(ChainLink? nextLink)//插入到指定链结的头部（前面）（覆盖连接）（断口修复）
        {
            if (nextLink != null)
            {
                ChainLink? lastLink = nextLink.last;
                this.next = nextLink;//连接下一个链结
                nextLink.last = this;

                if (lastLink != null)//连接上一个链结
                {
                    this.last = lastLink;
                    lastLink.next = this;
                }
            }
            this.next = null;
        }
        public void BreakOff(bool linkable)//断开此链结与其他链结的连接（且断开内部元素），linkable为true时，则会自动连接断口
        {
            if (last != null)
            {
                last.next = null;
            }
            if (next != null)
            {
                next.last = null;
            }
            if (linkable)
            {
                if (last != null && next != null)
                {
                    last.next = next;
                    next.last = last;
                }
            }
            last = null;
            next = null;
            item = 0;
        }
    }
}
