using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemManager : MonoBehaviour
{
    public static SceneItemManager Instance { get; private set; }
    public bool interactive = true;
    public GameObject intoWorkTablePanel = null;
    public GameObject letterBox = null;
    public GameObject lastLetter = null;
    public GameObject lastMail = null;
    public GameObject coffeeInMarket = null;
    public GameObject neutrinoDebugger = null;
    public GameObject blackMineral = null;
    public GameObject perspectiveGlass = null;
    public GameObject endlessEnergyMaker = null;
    public GameObject letterInTrashBin = null;
    public GameObject trashBin = null;
    public GameObject rebornMachineCE = null;
    public ItemDisplay[] items = new ItemDisplay[100];
    public ItemState[] itemStates = new ItemState[100];
    public DialogueTrigger[] dialogueTriggers = new DialogueTrigger[200];

    public GameObject panel = null;

    public Canvas mainCanvas = null;

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

        items = new ItemDisplay[100];
        itemStates = new ItemState[100];
        dialogueTriggers = new DialogueTrigger[200];
    }
    // Start is called before the first frame update
    void Start()
    {
        
        FindItems();

        intoWorkTablePanel.SetActive(false);
        letterBox.SetActive(false);
        panel.SetActive(false);

        for (int i = 0; i < itemStates.Length; i++)
        {
            itemStates[i] = ItemState.NotInteractive;
        }
        itemStates[7] = ItemState.Invisible;
        itemStates[22] = ItemState.Invisible;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindItems()
    {
        intoWorkTablePanel = GameObject.Find("Canvas").transform.Find("IntoWorkTable").gameObject;
        letterBox = GameObject.Find("Canvas").transform.Find("LetterBoxPanel").gameObject;
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
    }

    

    public void ClickTrashBin1()
    {
        if (EventSystem.Instance.staticEventList[11] != 1)
        {
            trashBin.transform.Find("Dialog1").GetComponent<DialogueTrigger>().StartDialogue();
        }
        
    }

    public void ClickTrashBin2()
    {
        if (EventSystem.Instance.staticEventList[11] == 1)
        {
            trashBin.transform.Find("Dialog2").GetComponent<DialogueTrigger>().StartDialogue();
        }
    }
}
