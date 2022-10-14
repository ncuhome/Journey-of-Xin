using System.Security.AccessControl;
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
    public List<int> staticEventList = new List<int>();
    #endregion

    #region Unity Methods

    private void Awake() // 创建单例以及静态事件列表
    {

        Instance = this;

        for (int i = 0; i < 100; i++)
        {
            staticEventList.Add(0);  
        }  
    }

    #endregion

    #region  EventSystem
    public void changeStaticEvent(int index , bool active) //改变静态事件
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

    public bool isStaticEvent(int index) //查询静态事件
    {
        if (index == 0)
        {
            return true;
        }
        // Debug.Log(staticEventList[index]);
        if (staticEventList[index] == 0)
        {
            return false;
        }
        return true;
    }

    public bool ActiveEvent(int index) //进行动态事件
    {
        switch (index)
        {
            case 0:
                Debug.Log("TestEvent");
                break;
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
            case 9:
                ActiveEvent9();
                break;
            
            default :
                return false;
        }
        return true;
    }
    #endregion

    #region  ActiveEvents

    private void ActiveEvent1()
    {

    }

    private void ActiveEvent2()
    {

    }

    private void ActiveEvent3()
    {
        
    }

    private void ActiveEvent4()
    {
        
    }

    private void ActiveEvent5()
    {
        
    }

    private void ActiveEvent6()
    {
        
    }

    private void ActiveEvent9()
    {
        GameObject coffee = GameObject.Find("Canvas/Coffee");
        coffee.GetComponent<Animator>().SetTrigger("Click");
        StartCoroutine("ClickDialogStart", coffee.GetComponent<DialogueTrigger>());
    }

    private IEnumerator ClickDialogStart(DialogueTrigger dialogueTrigger)
    {
        yield return new WaitForSeconds(0.5f);
        dialogueTrigger.StartDialogue();
    }

    #endregion
    
}
