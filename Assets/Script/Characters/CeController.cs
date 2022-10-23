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
        CEs[3] = RoomManager.Instance.rooms[0][1].transform.Find("CE4").gameObject;
        CEs[4] = RoomManager.Instance.rooms[0][1].transform.Find("CE5").gameObject;
        CEs[5] = RoomManager.Instance.rooms[1][0].transform.Find("CE6").gameObject;
        // ����ͼƬ��ֻ�в�͸���ĵط��ܴ�����Ӧ
        // CEs[0].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        // CEs[1].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        // CEs[2].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        // CEs[3].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        centerOfCanvas = new Vector3(960, 340, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // CEs[0].GetComponent<Image>().rectTransform.sizeDelta = new Vector2 (CEs[0].GetComponent<Image>().sprite.rect.width * imageSizeScale, CEs[0].GetComponent<Image>().sprite.rect.height * imageSizeScale);
        // CEs[1].GetComponent<Image>().rectTransform.sizeDelta = new Vector2 (CEs[1].GetComponent<Image>().sprite.rect.width * imageSizeScale, CEs[1].GetComponent<Image>().sprite.rect.height * imageSizeScale);
        // CEs[2].GetComponent<Image>().rectTransform.sizeDelta = new Vector2 (CEs[2].GetComponent<Image>().sprite.rect.width * imageSizeScale, CEs[2].GetComponent<Image>().sprite.rect.height * imageSizeScale);
        // CEs[3].GetComponent<Image>().rectTransform.sizeDelta = new Vector2 (CEs[3].GetComponent<Image>().sprite.rect.width * imageSizeScale, CEs[3].GetComponent<Image>().sprite.rect.height * imageSizeScale);
        ControlCEs();
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

        // 开始播放动�?
        InputManager.Instance.sceneState = SceneState.Animation;
        CEs[0].GetComponent<Button>().interactable = false;

        // 待添加动�?
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
            state = 5;
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
        if ((EventSystem.Instance.staticEventList[5] != 1)
        && (EventSystem.Instance.staticEventList[6] != 1)
        && (EventSystem.Instance.staticEventList[7] != 1)
        && (EventSystem.Instance.staticEventList[8] != 1)
        && (EventSystem.Instance.staticEventList[9] != 1))
        {
            CEs[1].transform.Find("NoItemDialog").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    public void ControlCEs()
    {
        switch (state)
        {
            case 1:
                CEs[0].transform.position = centerOfCanvas;
                CEs[1].transform.position = centerOfCanvas;
                CEs[2].transform.position = centerOfCanvas + new Vector3(1150, 0, 0);
                CEs[3].transform.position = centerOfCanvas;
                break;
            case 2:
                // CEs[0].GetComponent<Animator>().enabled = false;
                // CEs[0].transform.position = centerOfCanvas + new Vector3(1016, 0, 0);
                CEs[0].SetActive(false);
                CEs[1].transform.position = centerOfCanvas;
                CEs[1].transform.SetSiblingIndex(CEs[1].transform.parent.childCount - 2);
                CEs[2].transform.position = centerOfCanvas + new Vector3(1150, 0, 0);
                CEs[3].transform.position = centerOfCanvas;
                break;
            case 3:
                CEs[0].SetActive(false);
                CEs[1].SetActive(false);
                CEs[2].transform.position = centerOfCanvas + new Vector3(-607, 0, 0);
                if (EventSystem.Instance.staticEventList[13] != 1)
                {
                    CEs[2].GetComponent<Animator>().Play("Stand 0");
                }
                CEs[3].transform.position = centerOfCanvas;
                break;
            case 4:
                CEs[0].SetActive(false);
                CEs[1].SetActive(true);
                CEs[1].GetComponent<Button>().enabled = false;
                CEs[1].transform.position = centerOfCanvas;
                CEs[1].transform.SetSiblingIndex(CEs[1].transform.parent.childCount - 2);
                CEs[2].transform.position = centerOfCanvas + new Vector3(1150, 0, 0);
                CEs[3].transform.position = centerOfCanvas;
                break;
            case 5:
                CEs[0].SetActive(false);
                CEs[1].SetActive(false);
                CEs[2].SetActive(false);
                CEs[3].transform.SetSiblingIndex(CEs[3].transform.parent.childCount - 2);
                if (EventSystem.Instance.staticEventList[14] == 1)
                {
                    CEs[3].GetComponent<Animator>().SetBool("Sleep", true);
                }
                CEs[3].transform.position = centerOfCanvas;
                break;
            case 6:
                CEs[1].transform.SetSiblingIndex(0);
                CEs[4].transform.SetSiblingIndex(CEs[4].transform.parent.childCount - 2);
                break;
        }
    }



    public void CEMoveToMainRoom()
    {
        InputManager.Instance.sceneState = SceneState.Animation;
        CEs[2].GetComponent<Animator>().SetTrigger("StartWalking");
        CEs[2].transform.SetSiblingIndex(CEs[2].transform.parent.childCount - 2);
        StartCoroutine("FinishWalkToMainControlRoom");
    }

    public IEnumerator FinishWalkToMainControlRoom()
    {
        yield return new WaitForSeconds(2f);
        EventSystem.Instance.staticEventList[13] = 1;
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
        state = 4;
        CEs[2].GetComponent<Animator>().SetTrigger("StartWalking");
        StartCoroutine("FinishLeaveMainRoom");
    }

    public IEnumerator FinishLeaveMainRoom()
    {
        yield return new WaitForSeconds(2f);
        InputManager.Instance.sceneState = SceneState.MainScene;
        SceneItemManager.Instance.itemStates[5] = ItemState.Interactive;
    }


    public void ClickFaintCe()
    {
        if (EventSystem.Instance.staticEventList[12] != 1)
        {
            CEs[3].GetComponent<DialogueTrigger>().StartDialogue();
        }
        else
        {
            AwakeCe();
        }
    }

    public void AwakeCe()
    {
        EventSystem.Instance.staticEventList[14] = 1;
        CEs[3].GetComponent<Animator>().SetBool("Sleep", false);
        CEs[3].transform.Find("DialogNode9").GetComponent<DialogueTrigger>().StartDialogue();
    }

}
