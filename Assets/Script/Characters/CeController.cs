using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CeController : MonoBehaviour
{
    public static CeController Instance { get; private set; }
    public GameObject[] CEs = new GameObject[10];
    public int state = 0;

    public Vector3 centerOfCanvas = new Vector3(960, 540, 0);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CEs[0] = RoomManager.Instance.rooms[0][0].transform.Find("CE1").gameObject;
        CEs[1] = RoomManager.Instance.rooms[0][1].transform.Find("CE2").gameObject;
        CEs[2] = RoomManager.Instance.rooms[0][0].transform.Find("CE3").gameObject;

    }
    // Start is called before the first frame update
    void Start()
    {
        centerOfCanvas = new Vector3(960, 540, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //ControlCEs();
    }

    public void Click1()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (state == 1)
        {
            TimeManager.Instance.StopTimeRecord();
            if (EventSystem.Instance.staticEventList[2] == 1)
            {
                CEs[0].transform.Find("CorrectDialog").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                CEs[0].transform.Find("WrongDialog").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
    }

    public void CeWalkToSpaceShip()
    {

        // 开始播放动画
        InputManager.Instance.sceneState = SceneState.Animation;
        CEs[0].GetComponent<Button>().interactable = false;

        // 待添加动画
        CEs[0].GetComponent<Animator>().SetTrigger("StartWalking");
        StartCoroutine("FinishWalkToSpaceShip");
    }

    public IEnumerator FinishWalkToSpaceShip()
    {
        yield return new WaitForSeconds(2f);
        state = 2;
        InputManager.Instance.sceneState = SceneState.MainScene;
        EventSystem.Instance.ActiveEvent(37);
        CEs[1].transform.SetSiblingIndex(CEs[1].transform.parent.childCount);
    }

    public void Click2()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (StoreSystem.Find(7))
        {
            StoreSystem.Remove(7);
            EventSystem.Instance.staticEventList[5] = 1;
            CEs[1].transform.Find("CoffeeDialog").GetComponent<DialogueTrigger>().StartDialogue();
            return;
        }
        if (StoreSystem.Find(8))
        {
            StoreSystem.Remove(8);
            EventSystem.Instance.staticEventList[6] = 1;
            CEs[1].transform.Find("RememberDialog").GetComponent<DialogueTrigger>().StartDialogue();
            return;
        }
        if (StoreSystem.Find(9))
        {
            StoreSystem.Remove(9);
            EventSystem.Instance.staticEventList[7] = 1;
            CEs[1].transform.Find("LostDialog").GetComponent<DialogueTrigger>().StartDialogue();
            return;
        }
        if (StoreSystem.Find(10))
        {
            StoreSystem.Remove(10);
            EventSystem.Instance.staticEventList[8] = 1;
            CEs[1].transform.Find("LastRememberDialog").GetComponent<DialogueTrigger>().StartDialogue();
            return;
        }
        if (StoreSystem.Find(11))
        {
            StoreSystem.Remove(11);
            EventSystem.Instance.staticEventList[9] = 1;
            CEs[1].transform.Find("LastLostDialog").GetComponent<DialogueTrigger>().StartDialogue();
            return;
        }
        CEs[1].transform.Find("NoItemDialog").GetComponent<DialogueTrigger>().StartDialogue();
    }

    public void ControlCEs()
    {
        switch (state)
        {
            case 1:
                CEs[0].transform.position = centerOfCanvas;
                CEs[1].transform.position = centerOfCanvas;
                CEs[2].transform.position = centerOfCanvas + new Vector3(1016, 0, 0);
                break;
            case 2:
                // CEs[0].GetComponent<Animator>().enabled = false;
                // CEs[0].transform.position = centerOfCanvas + new Vector3(1016, 0, 0);
                CEs[0].SetActive(false);
                CEs[1].transform.position = centerOfCanvas;
                CEs[1].transform.SetSiblingIndex(CEs[1].transform.parent.childCount);
                CEs[2].transform.position = centerOfCanvas + new Vector3(1016, 0, 0);
                break;
            case 3:
                CEs[0].SetActive(false);
                CEs[1].SetActive(false);
                CEs[2].transform.position = centerOfCanvas + new Vector3(-607, 0, 0);
                break;
            case 4:
                CEs[0].SetActive(false);
                CEs[1].SetActive(true);
                CEs[1].GetComponent<Button>().enabled = false;
                CEs[1].transform.position = centerOfCanvas;
                CEs[1].transform.SetSiblingIndex(CEs[1].transform.parent.childCount);
                CEs[2].transform.position = centerOfCanvas + new Vector3(1040, 0, 0);
                break;
        }
    }



    public void CEMoveToMainRoom()
    {
        InputManager.Instance.sceneState = SceneState.Animation;
        CEs[2].GetComponent<Animator>().SetTrigger("StartWalking");
        StartCoroutine("FinishWalkToMainControlRoom");
    }

    public IEnumerator FinishWalkToMainControlRoom()
    {
        yield return new WaitForSeconds(2f);
        state = 3;
        InputManager.Instance.sceneState = SceneState.MainScene;
        if (DialogueSystem.Instance.canEnterDialog[33])
        {
            CEs[2].transform.Find("Dialog33").GetComponent<DialogueTrigger>().StartDialogue();
        }
        else
        {
            if (EventSystem.Instance.staticEventList[10] == 1)
            {
                CEs[2].transform.Find("Dialog35").GetComponent<DialogueTrigger>().StartDialogue();
            }
            else
            {
                CEs[2].transform.Find("Dialog34").GetComponent<DialogueTrigger>().StartDialogue();
            }
        }
        
    }

    public void CELeaveMainRoom()
    {
        InputManager.Instance.sceneState = SceneState.Animation;
        CEs[2].GetComponent<Animator>().SetTrigger("StartWalking"); 
        StartCoroutine("FinishLeaveMainRoom");
    }

    public IEnumerator FinishLeaveMainRoom()
    {
        yield return new WaitForSeconds(2f);
        state = 4;
        InputManager.Instance.sceneState = SceneState.MainScene;
        SceneItemManager.Instance.itemStates[5] = ItemState.Interactive;
    }

}
