
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
        staticEventList = new int[100];
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
            case 1: MiniGame3(); break;
            case 2: IntoWorkTable(); break;
            case 3: ShowLetterBox(); break;
            case 4: GetWeapon(); break;
            case 5: MiniGame2(); break;
            case 6: DeleteRebornDevice(); break;
            case 7: GetLetterInTrashBin(); break;
            case 8: GetCoffee(); break;
            case 9: GetLetterInBox(); break;
            case 10: GetNumber1(); break;
            case 11: GetNumber2(); break;
            case 12: GetNumber3(); break;
            case 13: GetNumber4(); break;
            case 14: GetNumber5(); break;
            case 15: MiniGame4(); break;
            case 16: GetMailInShip(); break;
            case 17: ClickCoffeeInMarket(); break;
            case 18: ClickNeutrinoDebugger(); break;
            case 19: ClickPerspectiveGlass(); break;
            case 20: ClickBlackMineral(); break;
            case 21: ClickEndlessEnergyMaker(); break;
            case 22: GetBlackMineral(); break;
            case 23: GetNeutrinoDebugger(); break;
            case 24: GetEndlessEnergyMaker(); break;
            case 25: GetPerspectiveGlass(); break;
            case 26: MiniGame1(); break;
            case 27: StartDialog23(); break;
            case 28: CanOperate(); break;
            case 29: CanNotOperate(); break;
            case 30: FinishMiniGame1(); break;
            case 31: FinishMiniGame2(); break;
            case 32: GetLastLetterAndMail(); break;
            case 33: FinishMiniGame3(); break;
            case 34: FinishMiniGame4(); break;
            case 35: CanChooseRedBox(); break;
            case 36: CeWalkToSpaceShip(); break;
            case 37: OpenMainRoomItem(); break;
            case 38: GetEndlessEnergy(); break;
            case 39: ChangeToMainRoom(); break;
            case 40: CELeaveMainRoom(); break;
            case 41: startTimeRecord3(); break;
            case 42: ChangeToSpaceShip(); break;
            case 43: AfterGetLetterInBox(); break;
            case 50: ToPlanet1(); break;
            case 51: ToPlanet2(); break;
            case 52: ToPlanet3(); break;
            case 53: ToPlanet4(); break;
            default: return false;
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
        SceneItemManager.Instance.intoWorkTablePanel.SetActive(true);
        SceneItemManager.Instance.intoWorkTablePanel.transform.SetSiblingIndex(SceneItemManager.Instance.intoWorkTablePanel.transform.parent.childCount);
        InputManager.Instance.sceneState = SceneState.Animation;
        StartCoroutine("DelayIntoWorkTable");
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
        SceneItemManager.Instance.interactive = true;
        SceneItemManager.Instance.panel.SetActive(false);
    }

    private void GetLetterInTrashBin()
    {
        StoreSystem.Add(14);
        SceneItemManager.Instance.interactive = true;
        SceneItemManager.Instance.letterInTrashBin.GetComponent<ItemDisplay>().Click();
    }

    private void GetLetterInBox()
    {
        StoreSystem.Add(14);
    }

    private void GetNumber1()
    {
        SceneItemManager.Instance.itemStates[8] = ItemState.NotInteractive;
        SecretNumberManager.Instance.numbersState[0] = 1;
        SecretNumberManager.Instance.SaveSecretNumbers();
        SecretNumberManager.Instance.Refresh();
    }

    private void GetNumber2()
    {
        SceneItemManager.Instance.itemStates[9] = ItemState.NotInteractive;
        SecretNumberManager.Instance.numbersState[1] = 1;
        SecretNumberManager.Instance.SaveSecretNumbers();
        SecretNumberManager.Instance.Refresh();
    }

    private void GetNumber3()
    {
        SceneItemManager.Instance.itemStates[14] = ItemState.NotInteractive;
        SecretNumberManager.Instance.numbersState[2] = 1;
        SecretNumberManager.Instance.SaveSecretNumbers();
        SecretNumberManager.Instance.Refresh();
    }

    private void GetNumber4()
    {
        SceneItemManager.Instance.itemStates[20] = ItemState.NotInteractive;
        SecretNumberManager.Instance.numbersState[3] = 1;
        SecretNumberManager.Instance.SaveSecretNumbers();
        SecretNumberManager.Instance.Refresh();
    }

    private void GetNumber5()
    {
        SecretNumberManager.Instance.numbersState[4] = 1;
        SecretNumberManager.Instance.SaveSecretNumbers();
        SecretNumberManager.Instance.Refresh();
    }

    private void MiniGame4()
    {

    }

    private void GetMailInShip()
    {
        StoreSystem.Add(15);
    }

    private void ToPlanet1()
    {
        RoomManager.Instance.ChangePlanet(0);
    }
    private void ToPlanet2()
    {
        RoomManager.Instance.ChangePlanet(1);
    }
    private void ToPlanet3()
    {
        RoomManager.Instance.ChangePlanet(2);
    }
    private void ToPlanet4()
    {
        RoomManager.Instance.ChangePlanet(3);
    }

    private void ClickCoffeeInMarket()
    {
        SceneItemManager.Instance.coffeeInMarket.GetComponent<ItemDisplay>().Click();
    }
    private void ClickNeutrinoDebugger()
    {
        SceneItemManager.Instance.neutrinoDebugger.GetComponent<ItemDisplay>().Click();
    }
    private void ClickBlackMineral()
    {
        SceneItemManager.Instance.blackMineral.GetComponent<ItemDisplay>().Click();
    }
    private void ClickPerspectiveGlass()
    {
        SceneItemManager.Instance.perspectiveGlass.GetComponent<ItemDisplay>().Click();
    }
    private void ClickEndlessEnergyMaker()
    {
        SceneItemManager.Instance.endlessEnergyMaker.GetComponent<ItemDisplay>().Click();
    }

    private void GetNeutrinoDebugger()
    {
        StoreSystem.Add(22);
    }

    private void GetBlackMineral()
    {
        StoreSystem.Add(18);
    }
    private void GetPerspectiveGlass()
    {
        StoreSystem.Add(20);
    }
    private void GetEndlessEnergyMaker()
    {
        StoreSystem.Add(21);
    }

    private void MiniGame1()
    {
        // 挖矿小游戏
    }

    private void StartDialog23()
    {
        SceneItemManager.Instance.dialogueTriggers[23].StartDialogue();
    }

    private void CanOperate()
    {
        SceneItemManager.Instance.interactive = true;
    }

    private void CanNotOperate()
    {
        SceneItemManager.Instance.interactive = false;
    }

    private void FinishMiniGame1()
    {
        staticEventList[1] = 1;
    }
    private void FinishMiniGame2()
    {
        staticEventList[2] = 1;
        SceneItemManager.Instance.interactive = true;
    }
    private void FinishMiniGame3()
    {
        staticEventList[3] = 1;
    }
    private void FinishMiniGame4()
    {
        staticEventList[4] = 1;
    }

    private void CanChooseRedBox()
    {
        SceneItemManager.Instance.itemStates[10] = ItemState.Interactive;
        CeController.Instance.state = 1;
        TimeManager.Instance.StartTimeRecord(5f, 0, 0, 1);
    }
    private void CeWalkToSpaceShip()
    {
        CeController.Instance.CeWalkToSpaceShip();
    }
    private void OpenMainRoomItem()
    {
        SceneItemManager.Instance.itemStates[2] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[3] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[4] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[6] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[8] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[9] = ItemState.Interactive;
        SceneItemManager.Instance.itemStates[11] = ItemState.Interactive;
    }

    private void GetEndlessEnergy()
    {
        StoreSystem.Add(23);
    }

    private void ChangeToMainRoom()
    {
        RoomManager.Instance.LastRoom();
        RoomManager.Instance.canChangeRoom = false;
        TimeManager.Instance.StartTimeRecord(5, 0, 0, 2);
    }

    private void CELeaveMainRoom()
    {
        CeController.Instance.CELeaveMainRoom();
    }

    public void IncomingLetter()
    {
        SceneItemManager.Instance.itemStates[7] = ItemState.Interactive;
        TimeManager.Instance.StartTimeRecord(15, 0, 0, 4);
    }

    private void startTimeRecord3()
    {

        TimeManager.Instance.StartTimeRecord(32, 0, 0, 3);
    }

    private void ChangeToSpaceShip()
    {
        SceneItemManager.Instance.itemStates[12] = ItemState.Interactive;
        StartCoroutine("DelayToSpaceShip");
    }

    private IEnumerator DelayToSpaceShip()
    {
        yield return new WaitForSeconds(2.5f);
        RoomManager.Instance.NextRoom();
    }

    private void AfterGetLetterInBox()
    {
        staticEventList[10] = 1;
        TimeManager.Instance.StopTimeRecord();
        TimeManager.Instance.StartTimeRecord(5, 0, 0, 4);
    }


    #endregion

}
