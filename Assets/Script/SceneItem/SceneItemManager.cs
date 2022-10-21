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
    public ItemDisplay[] items = new ItemDisplay[100];
    public ItemState[] itemStates = new ItemState[100];
    public DialogueTrigger[] dialogueTriggers = new DialogueTrigger[50];

    public GameObject panel = null;

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

        intoWorkTablePanel = GameObject.Find("IntoWorkTable");
        letterBox = GameObject.Find("LetterBoxPanel");
        lastLetter = GameObject.Find("LastLetter");
        lastMail = GameObject.Find("LastMail");
        panel = GameObject.Find("Canvas/Panel");
        coffeeInMarket = GameObject.Find("Planet2/Room2/Coffee");
        neutrinoDebugger = GameObject.Find("NeutrinoDebugger");
        blackMineral = GameObject.Find("BlackMineral");
        perspectiveGlass = GameObject.Find("PerspectiveGlass");
        endlessEnergyMaker = GameObject.Find("EndlessEnergyMaker");
        letterInTrashBin = GameObject.Find("LetterInTrashBin");
    }
    // Start is called before the first frame update
    void Start()
    {
        intoWorkTablePanel.SetActive(false);
        letterBox.SetActive(false);
        lastLetter.SetActive(false);
        lastMail.SetActive(false);
        panel.SetActive(false);

        for (int i = 0; i < itemStates.Length; i++)
        {
            itemStates[i] = ItemState.NotInteractive;
        }
        itemStates[7] = ItemState.Invisible;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
