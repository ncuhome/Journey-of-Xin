
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
    public int[] staticEventList = new int[100];
    #endregion

    #region Unity Methods

    private void Awake() // 创建单例以及静态事件列表
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region  EventSystem
    public void changeStaticEvent(int index, bool active) //改变静态事件
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
            case 1:
                MiniGame3();
                break;
            case 2:
                IntoWorkTable();
                break;
            case 3:
                ShowLetterBox();
                break;
            case 4:
                GetWeapon();
                break;
            case 5:
                MiniGame2();
                break;
            case 6:
                DeleteRebornDevice();
                break;
            case 7:
                GetLetterInTrashBin();
                break;
            case 8:
                GetCoffee();
                break;
            case 9:
                GetLetterInBox();
                break;
            case 10:
                GetNumber1();
                break;
            case 11:
                GetNumber2();
                break;
            case 12:
                GetNumber3();
                break;
            case 13:
                GetNumber4();
                break;
            case 14:
                GetNumber5();
                break;
            case 15:
                MiniGame4();
                break;
            case 16:
                GetMailInShip();
                break;
            case 32:
                GetLastLetterAndMail();
                break;
            default:
                return false;
        }
        return true;
    }
    #endregion

    #region  ActiveEvents

    private void MiniGame3()
    {
        Debug.Log("进入小游戏");
        // 进入小游戏3

    }

    private void IntoWorkTable()
    {
        StartCoroutine("DisplayIntoWorkTable");
        StartCoroutine("DelayIntoWorkTable");
    }

    private IEnumerator DisplayIntoWorkTable()
    {
        yield return new WaitForSeconds(0.5f);
        SceneItemManager.Instance.intoWorkTablePanel.SetActive(true);
        SceneItemManager.Instance.intoWorkTablePanel.transform.SetSiblingIndex(SceneItemManager.Instance.intoWorkTablePanel.transform.parent.childCount);
    }

    private IEnumerator DelayIntoWorkTable()
    {
        yield return new WaitForSeconds(1.5f);
        SceneItemManager.Instance.intoWorkTablePanel.SetActive(false);
        SceneItemManager.Instance.interactive = true;
        //进入工作台界面
        WorkbenchSystem.Instance.ShowWorkbench();
    }

    private void ShowLetterBox()
    {
        SceneItemManager.Instance.letterBox.SetActive(true);
        SceneItemManager.Instance.letterBox.transform.SetSiblingIndex(SceneItemManager.Instance.letterBox.transform.parent.childCount);
    }

    // private IEnumerator DelayShowLetterBox()
    // {
    //     yield return new WaitForSeconds(0.5f);

    // }

    private void GetLastLetterAndMail()
    {
        SceneItemManager.Instance.lastLetter.SetActive(true);
        SceneItemManager.Instance.lastLetter.GetComponent<ItemDisplay>().Click();
        StoreSystem.Add(5);
        StoreSystem.Add(6);
        StartCoroutine("GetLastLetter");
        StartCoroutine("ShowLastMail");
        StartCoroutine("GetLastMail");
        // 获得最后的信和最后的邮的事件

    }
    private IEnumerator GetLastLetter()
    {
        yield return new WaitForSeconds(0.5f);
        SceneItemManager.Instance.lastLetter.GetComponent<ItemDisplay>().DisplayStart();
    }

    private IEnumerator ShowLastMail()
    {
        yield return new WaitForSeconds(2f);
        SceneItemManager.Instance.lastMail.SetActive(true);
        SceneItemManager.Instance.lastMail.GetComponent<ItemDisplay>().Click();
    }
    private IEnumerator GetLastMail()
    {
        yield return new WaitForSeconds(2.5f);
        SceneItemManager.Instance.lastMail.GetComponent<ItemDisplay>().DisplayStart();
    }

    private void GetWeapon()
    {
        StoreSystem.Add(13);
    }

    private void GetCoffee()
    {
        StoreSystem.Add(2);
    }

    private void MiniGame2()
    {
        // 小游戏2 华容道
    }

    private void DeleteRebornDevice()
    {
        StoreSystem.Remove(1);
    }

    private void GetLetterInTrashBin()
    {
        StoreSystem.Add(14);
    }

    private void GetLetterInBox()
    {
        StoreSystem.Add(14);
    }

    private void GetNumber1()
    {
        StoreSystem.Add(16);
    }

    private void GetNumber2()
    {
        StoreSystem.Add(12);
    }

    private void GetNumber3()
    {
        StoreSystem.Add(26);
    }

    private void GetNumber4()
    {
        StoreSystem.Add(25);
    }

    private void GetNumber5()
    {
        StoreSystem.Add(27);
    }

    private void MiniGame4()
    {

    }

    private void GetMailInShip()
    {
        StoreSystem.Add(15);
    }

    #endregion

}
