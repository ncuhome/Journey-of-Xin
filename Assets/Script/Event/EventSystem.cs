
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region  Interface
public interface IEventList//接口，用于�?�理全局事件：静态事件：�??发生或未发生的事件状态；动态事件：一�??会�?�游戏产生影响的功能函数
{
    public bool isStaticEvent(int index);//通过索引值�?�索�??�静态事件是否�??触发
    public bool ActiveEvent(int index);//通过索引值触发�?�动态事件的功能函数,成功调用返回true
}
#endregion

public class EventSystem : MonoBehaviour, IEventList
{
    #region  Properties
    public static EventSystem Instance { get; private set; } // 单例模式 
    public int[] staticEventList = new int[100];
    #endregion

    #region Unity Methods

    private void Awake() // 创建单例以及静态事件列�??
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
        staticEventList[16] = 1;
        staticEventList[17] = 1;
        staticEventList[18] = 1;
        staticEventList[19] = 1;
        staticEventList[20] = 1;
    }

    #endregion

    #region  EventSystem
    public void changeStaticEvent(int index, bool active) //改变静态事�??
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

    public bool isStaticEvent(int index) //查�?�静态事�??
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

    public bool ActiveEvent(int index) //进�?�动态事�??
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
            case 44: GetRebornMachineCE(); break;
            case 45: DisplayGetRebornMachineCE(); break;
            case 46: CanClickTrashBin(); break;
            case 47: LetterIntoTrashBin(); break;
            case 48: CeFaint(); break;
            case 49: StealCe(); break;
            case 50: ToPlanet1(); break;
            case 51: ToPlanet2(); break;
            case 52: ToPlanet3(); break;
            case 53: ToPlanet4(); break;
            case 54: AwakeCe(); break;
            case 55: AfterDialogNode9(); break;
            case 56: StartTimeRecordToBlackMarket(); break;
            case 57: LeaveSpaceShipToMarket(); break;
            case 58: ChooseGoods(); break;
            case 59: AfterChooseGoods(); break;
            case 60: AfterDialogNode14(); break;
            case 61: LeaveSpaceShipToMainRoom(); break;
            default: return false;
        }
        return true;
    }
    #endregion

    #region  ActiveEvents

    private void MiniGame3()
    {
        Debug.Log("进入小游�??");
        // 进入小游�??3

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
        //进入工作台界�??
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
        // 获得最后的信和最后的�??的事�??

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
        // 小游�??2 华�?�道
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
        SceneItemManager.Instance.itemStates[7] = ItemState.Interactive;
        SceneItemManager.Instance.letterInTrashBin.transform.SetSiblingIndex(SceneItemManager.Instance.letterInTrashBin.transform.parent.childCount);
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
        staticEventList[15] = 1;
        SceneItemManager.Instance.itemStates[15] = ItemState.Interactive;
        SceneItemManager.Instance.coffeeInMarket.GetComponent<ItemDisplay>().Click();
    }
    private void ClickNeutrinoDebugger()
    {
        staticEventList[15] = 1;
        SceneItemManager.Instance.itemStates[16] = ItemState.Interactive;
        SceneItemManager.Instance.neutrinoDebugger.GetComponent<ItemDisplay>().Click();
    }
    private void ClickBlackMineral()
    {
        staticEventList[15] = 1;
        SceneItemManager.Instance.itemStates[17] = ItemState.Interactive;
        SceneItemManager.Instance.blackMineral.GetComponent<ItemDisplay>().Click();
    }
    private void ClickPerspectiveGlass()
    {
        staticEventList[15] = 1;
        SceneItemManager.Instance.itemStates[18] = ItemState.Interactive;
        SceneItemManager.Instance.perspectiveGlass.GetComponent<ItemDisplay>().Click();
    }
    private void ClickEndlessEnergyMaker()
    {
        staticEventList[15] = 1;
        SceneItemManager.Instance.itemStates[19] = ItemState.Interactive;
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
        // 挖矿小游�??
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
        TimeManager.Instance.StartTimeRecord(5, 0, 0, 1, true);
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
        TimeManager.Instance.StartTimeRecord(5, 0, 0, 2, true);
    }

    private void CELeaveMainRoom()
    {
        CeController.Instance.CELeaveMainRoom();
    }

    public void IncomingLetter()
    {
        SceneItemManager.Instance.itemStates[7] = ItemState.Interactive;
        TimeManager.Instance.StartTimeRecord(15, 0, 0, 4, true);
    }

    private void startTimeRecord3()
    {

        TimeManager.Instance.StartTimeRecord(32, 0, 0, 3, true);
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
        staticEventList[17] = 0;
        staticEventList[19] = 1;
        TimeManager.Instance.StopTimeRecord();
        TimeManager.Instance.StartTimeRecord(5, 0, 0, 4, true);
    }

    private void GetRebornMachineCE()
    {
        StoreSystem.Add(17);
        if (staticEventList[6] == 1)
        {
            StartCoroutine("DelayStartDialogNode11");
        }
        else
        {
            staticEventList[12] = 1;
            CeController.Instance.CEs[3].transform.Find("DialogRemind2").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    private void DisplayGetRebornMachineCE()
    {
        RoomManager.Instance.canChangeRoom = false;
        SceneItemManager.Instance.itemStates[23] = ItemState.Interactive;
        SceneItemManager.Instance.rebornMachineCE.transform.SetSiblingIndex(SceneItemManager.Instance.rebornMachineCE.transform.parent.childCount - 1);
        SceneItemManager.Instance.rebornMachineCE.GetComponent<ItemDisplay>().Click();
    }

    private IEnumerator DelayStartDialogNode11()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("DialogNode11").GetComponent<DialogueTrigger>().StartDialogue();
    }

    private void CanClickTrashBin()
    {
        SceneItemManager.Instance.itemStates[13] = ItemState.Interactive;
        TimeManager.Instance.StartTimeRecord(15, 0, 1, 5, true);
    }

    public void StartDialogNode12()
    {
        CeController.Instance.CEs[1].GetComponent<Animator>().SetTrigger("LeaveSpaceShip");
        InputManager.Instance.sceneState = SceneState.Animation;
        StartCoroutine("EndAnimation");
        StartCoroutine("DelayStartDialogNode12");
    }

    private IEnumerator DelayStartDialogNode12()
    {
        yield return new WaitForSeconds(2.1f);
        GameObject.Find("DialogNode12").GetComponent<DialogueTrigger>().StartDialogue();
    }

    private void LetterIntoTrashBin()
    {
        CeController.Instance.CEs[1].GetComponent<Animator>().SetTrigger("WalkToTrashbin");

        InputManager.Instance.sceneState = SceneState.Animation;
        StartCoroutine("EndAnimation");

        staticEventList[11] = 1;
        SceneItemManager.Instance.itemStates[12] = ItemState.Interactive;
    }

    private void CeFaint()
    {
        CeController.Instance.state = 5;
        CeController.Instance.CEs[3].GetComponent<Animator>().SetBool("Sleep", true);
        StartCoroutine("AfterCeFaint");
    }

    private IEnumerator AfterCeFaint()
    {
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.LastRoom();
        SceneItemManager.Instance.itemStates[5] = ItemState.Interactive;
        TimeManager.Instance.StartTimeRecord(60, 0, 0, 6, false);
    }

    public void StartDialogRemind1()
    {
        GameObject.Find("DialogRemind1").GetComponent<DialogueTrigger>().StartDialogue();
    }

    private void StealCe()
    {
        SceneItemManager.Instance.itemStates[23] = ItemState.Interactive;
        SceneItemManager.Instance.rebornMachineCE.GetComponent<ItemDisplay>().Click();
    }

    private void AwakeCe()
    {
        CeController.Instance.AwakeCe();
    }

    private void AfterDialogNode9()
    {
        SceneItemManager.Instance.itemStates[12] = ItemState.Interactive;
    }

    public IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(2f);
        InputManager.Instance.sceneState = SceneState.MainScene;
    }

    private void StartTimeRecordToBlackMarket()
    {
        TimeManager.Instance.StartTimeRecord(30, 0, 1, 7, false);
    }

    public void EnterBlackMarket()
    {
        RoomManager.Instance.ChangePlanet(1);
        CeController.Instance.state = 6;
        StartCoroutine("EnterBlackMarketDialog");
    }

    public IEnumerator EnterBlackMarketDialog()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("DialogSpaceShipToMarket");
        if (staticEventList[5] == 1)
        {
            if (staticEventList[10] != 1)
            {
                GameObject.Find("Dialog3-1-1").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                GameObject.Find("Dialog3-1-1").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
        if (staticEventList[6] == 1)
        {
            GameObject.Find("Dialog3-1-2").GetComponent<DialogueTrigger>().StartDialogue();
        }
        if (staticEventList[7] == 1)
        {
            GameObject.Find("Dialog3-1-4").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    private void LeaveSpaceShipToMarket()
    {
        CeController.Instance.CEs[4].transform.SetSiblingIndex(CeController.Instance.CEs[4].transform.parent.childCount - 2);
        CeController.Instance.CEs[4].GetComponent<Animator>().SetTrigger("Leave");
        StartCoroutine("IntoBlackMarket");
    }

    private IEnumerator IntoBlackMarket()
    {
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.LastRoom();
        StartCoroutine("StartBlackMarketDialog");
    }

    private IEnumerator StartBlackMarketDialog()
    {
        yield return new WaitForSeconds(3f);
        if (staticEventList[5] == 1)
        {
            if (staticEventList[10] != 1)
            {
                GameObject.Find("DialogNode13").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                GameObject.Find("DialogNode16").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
        if (staticEventList[6] == 1)
        {
            GameObject.Find("DialogNode15").GetComponent<DialogueTrigger>().StartDialogue();
        }
        if (staticEventList[7] == 1)
        {
            GameObject.Find("DialogNode13").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    private void ChooseGoods()
    {
        GameObject.Find("ChooseGoods").GetComponent<DialogueTrigger>().StartDialogue();
    }

    private void AfterChooseGoods()
    {
        StartCoroutine("StartDialogNode14");
    }

    private IEnumerator StartDialogNode14()
    {
        yield return new WaitForSeconds(1.5f);
        if (staticEventList[15] == 1)
        {
            GameObject.Find("DialogNode14").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    private void AfterDialogNode14()
    {
        CeController.Instance.CEs[5].GetComponent<Animator>().SetTrigger("Leave");
        StartCoroutine("DelayLeaveMarket");
    }

    private IEnumerator DelayLeaveMarket()
    {
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.NextRoom();
        CeController.Instance.state = 7;
        TimeManager.Instance.StartTimeRecord(15, 1, 1, 8, false);
    }

    public void ReturnToMainControlRoom()
    {
        RoomManager.Instance.ChangePlanet(0);
        StartCoroutine("ReturnMainControlRoomDialog");
    }

    public IEnumerator ReturnMainControlRoomDialog()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("DialogSpaceShipToMarket");
        if (staticEventList[5] == 1)
        {
            if (staticEventList[10] != 1)
            {
                GameObject.Find("Dialog4-1-1").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                GameObject.Find("Dialog4-1-2").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
        if (staticEventList[6] == 1)
        {
            GameObject.Find("Dialog4-2").GetComponent<DialogueTrigger>().StartDialogue();
        }
        if (staticEventList[7] == 1)
        {
            GameObject.Find("Dialog4-1-4").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    private void LeaveSpaceShipToMainRoom()
    {
        CeController.Instance.CEs[4].transform.SetSiblingIndex(CeController.Instance.CEs[4].transform.parent.childCount - 2);
        CeController.Instance.CEs[4].GetComponent<Animator>().SetTrigger("Leave");
        StartCoroutine("IntoMainControlRoom");
    }

    private IEnumerator IntoMainControlRoom()
    {
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.LastRoom();
        StartCoroutine("StartDialogChapter4");
    }

    private IEnumerator StartDialogChapter4()
    {
        yield return new WaitForSeconds(2.5f);
        if (staticEventList[5] == 1)
        {
            if (staticEventList[10] != 1)
            {
                GameObject.Find("DialogNode17").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                GameObject.Find("DialogNode17").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
        if (staticEventList[6] == 1)
        {
            GameObject.Find("DialogNode18").GetComponent<DialogueTrigger>().StartDialogue();
        }
        if (staticEventList[7] == 1)
        {
            GameObject.Find("DialogNode17").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    #endregion

}
