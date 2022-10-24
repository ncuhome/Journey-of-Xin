using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    #region Properties

    public static DialogueSystem Instance { get; private set; } // 单例模式

    public bool[] canEnterDialog = new bool[100];
    public DialogueTrigger dialogueTrigger = null;
    private Image background = null; // 对话框背景
    public Sprite[] backgroundSprite = null; // 两张背景画面
    private Image avatar = null; // 头像组件
    private TMP_Text nameText = null; // 名字文本
    private TMP_Text dialogText = null; // 对话文本
    private GameObject dialogueNode = null; // 对话系统的整体节点
    public List<Sprite> avatars = new List<Sprite>(); // 头像列表
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();  // 名字与头像的对应关系

    private int dialogIndex = 0; // 当前的文本 ID
    private bool isChoosing = false; // 正在选择选项
    public static bool inDialogue = false; // 正在进行对话
    private string[] dialogRows = null; // 储存每一行对话文本的数组
    private string[] cells = null; // 每一行的各个部分
    public GameObject optionButton = null; // 选项按钮预制件
    private Transform buttonGroup = null; // 选项按钮父物体

    private bool textIsFinished = false; // 文本是否显示完毕
    public float textSpeed = 0.05f; // 每个字的显示速度

    #endregion


    #region Unity Methods

    private void Awake() // 把名字与头像对应（头像还没图
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        canEnterDialog = new bool[100];
        // 用字典将名字与头像对应
        imageDic["ce"] = avatars[0];
        imageDic["kown"] = avatars[1];

        //获取组件
        dialogueNode = GameObject.Find("DialogCanvas/Dialogue");
        avatar = dialogueNode.transform.Find("Avatar").GetComponent<Image>();
        nameText = dialogueNode.transform.Find("NameText").GetComponent<TMP_Text>();
        dialogText = dialogueNode.transform.Find("DialogueText").GetComponent<TMP_Text>();
        buttonGroup = dialogueNode.transform.Find("OptionsGroup");
        background = dialogueNode.transform.Find("Background").GetComponent<Image>();
    }

    private void Start()
    {
        for (int i = 0;i < 100;i++)
        {
            canEnterDialog[i] = true;
        }
        dialogueNode.SetActive(false);
        // StartDialogue(dialogDataFile);
    }

    private void Update() //按确认键（z）或者点击鼠标就进入下一句话（后续改进
    {

    }

    #endregion


    #region Dialogue

    public void UpdateText(string name, string text) // 显示文本与头像
    {
        // Debug.Log("UpdateText");
        dialogText.text = "";
        nameText.text = name;
        if (name == "")
        {
            background.sprite = backgroundSprite[0];
        }
        else
        {
            background.sprite = backgroundSprite[1];
        }
        avatar.gameObject.SetActive(false);
        StartCoroutine("DisplayDialogue", text);

    }

    public IEnumerator StartDialogue(DialogueTrigger dialogueTrigger) //开始对话
    {
        InputManager.Instance.sceneState = SceneState.Dialog;
        this.dialogueTrigger = dialogueTrigger;
        dialogIndex = 0;
        dialogText.text = "";
        textIsFinished = false;
        dialogueNode.SetActive(true);
        ReadText(dialogueTrigger.dialogDataFile);
        ShowDialogRow();
        yield return new WaitForSeconds(0.1f);
        inDialogue = true;
    }

    public void ExitDialogue()
    {
        dialogueNode.SetActive(false);
        inDialogue = false;
        InputManager.Instance.sceneState = SceneState.MainScene;
    }

    public void ReadText(TextAsset textAsset) // 把对话文本分割成各行
    {
        dialogRows = textAsset.text.Split('\n');
    }

    public void ShowDialogRow() // 显示对话行
    {
        // Debug.Log(dialogIndex);
        for (int i = 1; i < dialogRows.Length; i++)
        {
            cells = dialogRows[i].Split(','); // 把对话行分割成各个数据

            if (int.Parse(cells[1]) != dialogIndex)
            {
                continue;
            }

            if (cells[0] == "End") // 如果是结束节点则结束对话
            {
                ExitDialogue();

                if (cells[6] != "") //如果效果不为空，则触发动态事件
                {
                    string[] effects = cells[6].Split('/'); // 把效果编号进行分割
                    foreach (string effect in effects)
                    {
                        DialogEffect(int.Parse(effect));
                    }
                }

                break;
            }

            if (cells[6] != "") //如果效果不为空，则触发动态事件
            {
                string[] effects = cells[6].Split('/'); // 把效果编号进行分割
                foreach (string effect in effects)
                {
                    DialogEffect(int.Parse(effect));
                }
            }

            if (cells[0] == "#") //如果是普通对话且 ID 是正在进行的对话 ID 就显示
            {
                UpdateText(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[4]); // 跳转下一条对话
                break;
            }
            else if (cells[0] == "&") // 如果是选择对话则显示按钮
            {
                UpdateText(cells[2], "");

                GenerateOption(i);
                break;
            }
        }
    }

    public void ContinueDialog() //进行下一行对话
    {
        ShowDialogRow();
    }

    public void GenerateOption(int index) // 生成按钮并添加按钮事件
    {
        isChoosing = true;
        string[] cells = dialogRows[index].Split(',');
        string[] conditions = cells[5].Split('/'); // 把条件编号进行分割
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[3];

            // 给按钮添加事件
            button.GetComponent<Button>().onClick.AddListener
            (
                delegate
                {
                    OnOptionClick(int.Parse(cells[4]));
                }
            );

            if (cells[5] != "") //如果有条件则进入判断是否启用按钮
            {
                foreach (string condition in conditions) //如果有一个条件不满足就禁用按钮
                {
                    if (!EventSystem.Instance.isStaticEvent(int.Parse(condition)))
                    {
                        button.GetComponent<Button>().interactable = false;
                    }
                }
            }
            GenerateOption(index + 1);
        }
    }


    public void OnOptionClick(int id) // 添加按钮事件
    {
        dialogIndex = id;
        // Debug.Log(dialogIndex);

        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
        ShowDialogRow();
        StartCoroutine("Chosen");
    }

    public IEnumerator Chosen()
    {
        yield return new WaitForSeconds(0.1f);
        isChoosing = false;
    }

    public void DialogEffect(int index) // 触发动态事件
    {
        if (index > 0)
        {
            EventSystem.Instance.ActiveEvent(index);
        }
        if (index < 0)
        {
            EventSystem.Instance.changeStaticEvent(index, true);
        }
        if (index == 0)
        {
            dialogueTrigger.GetComponent<ItemDisplay>().DisplayStart();
        }
    }

    public IEnumerator DisplayDialogue(string text)//一个字一个字显示文本
    {
        // Debug.Log("开始打字");
        textIsFinished = false;
        for (int i = 0; i < text.Length; i++)
        {
            dialogText.text += text[i];

            yield return new WaitForSeconds(textSpeed);
        }
        textIsFinished = true;
    }

    public IEnumerator FinishText()
    {
        yield return new WaitForSeconds(0.1f);
        textIsFinished = true;
    }

    public void InputDetect()
    {
        if (!inDialogue)
        {
            return;
        }

        if ((Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) && !isChoosing)
        {
            if (textIsFinished)
            {
                ContinueDialog();
            }
            else
            {
                StopCoroutine("DisplayDialogue");
                StartCoroutine("FinishText");
                dialogText.text = cells[3];
            }
        }
    }

    #endregion
}
