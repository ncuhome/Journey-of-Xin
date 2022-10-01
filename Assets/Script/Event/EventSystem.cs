using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region  Interface
public interface IEventList//接口，用于管理全局事件：静态事件：是发生或未发生的事件状态；动态事件：一个会对游戏产生影响的功能函数
{
    public bool isStaticEvent(int index);//通过索引值检索该静态事件是否被触发
    public bool ActiveEvent(int index);//通过索引值触发该动态事件的功能函数,成功调用返回true
}
#endregion

public class EventSystem : MonoBehaviour, IEventList
{
    #region  Properties
    public static EventSystem Instance { get; private set; } // 单例模式 
    public IntList staticEventList = new IntList("staticEventList");
    #endregion

    #region Unity Methods

    private void Awake() {
        
        Instance = this;

        staticEventList[0] = 0;
        for (int i = 1; i < 100; i++)
        {
            staticEventList.Add(0);  
        }  
    }

    #endregion

    #region  EventSystem
    public void changeStaticEvent(int index , bool active)
    {
        if (active)
        {
            staticEventList[index] = 1;
        } 
        else 
        {
            staticEventList[index] = 0;
        }
        
    }

    public bool isStaticEvent(int index)
    {
        if (staticEventList[index] == 0)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }

    public bool ActiveEvent(int index)
    {
        switch (index)
        {
            case 1:
                ActiveEvent1();
                break;
            case 2:
                ActiveEvent2();
                break;
            case 3:
                ActiveEvent3();
                break;
            case 4:
                ActiveEvent4();
                break;
            case 5:
                ActiveEvent5();
                break;
            case 6:
                ActiveEvent6();
                break;
            
            default :
                return false;
        }
        return true;
    }
    #endregion

    #region  ActiveEvents

    public void ActiveEvent1()
    {

    }

    public void ActiveEvent2()
    {

    }

    public void ActiveEvent3()
    {
        
    }

    public void ActiveEvent4()
    {
        
    }

    public void ActiveEvent5()
    {
        
    }

    public void ActiveEvent6()
    {
        
    }

    #endregion
    
}
