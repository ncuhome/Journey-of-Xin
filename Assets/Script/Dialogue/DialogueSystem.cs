using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    #region Properties
    
    public static DialogueSystem Instance { get; private set; } // 单例模式

    public TextAsset dialogDataFile = null; // 对话文件，以 csv 形式保存
    public Image avatar = null; // 头像组件
    public TMP_Text nameText = null; // 名字文本
    public TMP_Text dialogText = null; // 对话文本
    public GameObject dialogueNode = null; // 对话系统的整体节点
    public List<Sprite> avatars = new List<Sprite>(); // 头像列表
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();  // 名字与头像的对应关系

    private int dialogIndex = 0; // 当前的文本 ID
    private bool isChoosing = false; // 正在选择选项
    public bool inDialogue = false; // 正在进行对话
    private string[] dialogRows = null; // 储存每一行对话文本的数组

    public GameObject optionButton = null; // 选项按钮预制件
    public Transform buttonGroup = null; // 选项按钮父物体

    #endregion


    #region Unity Methods

    private void Awake() // 把名字与头像对应（头像还没图
    {
        imageDic["Ce"] = avatars[0];
        imageDic["Know"] = avatars[1];
        Instance = this;
    }

    private void Start() //直接播放对话（调试用
    {
        // StartDialogue(dialogDataFile);
    }

    private void Update() //按确认键（z）或者点击鼠标就进入下一句话（后续改进
    {
        if (!inDialogue)
        {
            return;
        }

        if ((Input.GetButtonDown("Confirm") || Input.GetMouseButtonDown(0)) && !isChoosing)
        {
            ContinueDialog();
        }
    }

    #endregion


    #region Dialogue

    public void UpdateText(string name, string text) // 显示文本与头像
    {
        nameText.text = name;
        dialogText.text = text;
        avatar.sprite = imageDic[name];
    }

    public void StartDialogue(TextAsset dialogData) //开始对话
    {
        inDialogue = true;
        dialogIndex = 0;
        dialogueNode.SetActive(true);
        ReadText(dialogData);
        ShowDialogRow();
    }

    public void ExitDialogue()
    {
        dialogueNode.SetActive(false);
        inDialogue = false;
    }

    public void ReadText(TextAsset textAsset) // 把对话文本分割成各行
    {
        dialogRows = textAsset.text.Split('\n');
    }

    public void ShowDialogRow() // 显示对话行
    {
        for (int i = 1; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(','); // 把对话行分割成各个数据

            if (cells[6] != "" && int.Parse(cells[1]) == dialogIndex) //如果效果不为空，则触发动态事件
            {
                string[] effects = cells[6].Split('/'); // 把效果编号进行分割
                foreach (string effect in effects)
                {
                    DialogEffect(int.Parse(effect));
                }
            }

            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex) //如果是普通对话且 ID 是正在进行的对话 ID 就显示
            {
                UpdateText(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[4]); // 跳转下一条对话
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex) // 如果是选择对话则显示按钮
            {
                GenerateOption(i);
                break;
            }
            else if (cells[0] == "End" && int.Parse(cells[1]) == dialogIndex) // 如果是结束节点则结束对话
            {
                ExitDialogue();
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
        isChoosing = false;
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
        ShowDialogRow();
    }

    public void DialogEffect(int index) // 触发动态事件
    {
        EventSystem.Instance.ActiveEvent(index);
    }

    #endregion


    // public void TestButton()
    // {
    //     EventSystem.Instance.ActiveEvent(1);
    //     EventSystem.Instance.ActiveEvent(2);
    //     PlayerData.Instance.Save();
    //     EventSystem.Instance.staticEventList[1] = 0;
    //     Debug.Log(EventSystem.Instance.staticEventList[1]);
    //     PlayerData.Instance.Load();
    //     Debug.Log(EventSystem.Instance.staticEventList[1]);
    // }
}
