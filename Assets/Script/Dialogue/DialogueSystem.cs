using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    #region Properties

    public static DialogueSystem Instance { get; private set; } // ����ģʽ

    public bool[] canEnterDialog = new bool[200];
    public DialogueTrigger dialogueTrigger = null;
    private Image background = null; // �Ի��򱳾�
    public Sprite[] backgroundSprite = null; // ���ű�������
    private Image avatar = null; // ͷ�����
    private TMP_Text nameText = null; // �����ı�
    private TMP_Text dialogText = null; // �Ի��ı�
    private GameObject dialogueNode = null; // �Ի�ϵͳ������ڵ�
    public List<Sprite> avatars = new List<Sprite>(); // ͷ���б�
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();  // ������ͷ��Ķ�Ӧ��ϵ

    private int dialogIndex = 0; // ��ǰ���ı� ID
    private bool isChoosing = false; // ����ѡ��ѡ��
    public static bool inDialogue = false; // ���ڽ��жԻ�
    private string[] dialogRows = null; // ����ÿһ�жԻ��ı�������
    private string[] cells = null; // ÿһ�еĸ�������
    public GameObject optionButton = null; // ѡ�ťԤ�Ƽ�
    private Transform buttonGroup = null; // ѡ�ť������

    private bool textIsFinished = false; // �ı��Ƿ���ʾ���
    public float textSpeed = 0.05f; // ÿ���ֵ���ʾ�ٶ�

    #endregion


    #region Unity Methods

    private void Awake() // ��������ͷ���Ӧ��ͷ��ûͼ
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        canEnterDialog = new bool[200];
        // ���ֵ佫������ͷ���Ӧ
        imageDic["ce"] = avatars[0];
        imageDic["kown"] = avatars[1];

        //��ȡ���
        dialogueNode = GameObject.Find("DialogCanvas/Dialogue");
        avatar = dialogueNode.transform.Find("Avatar").GetComponent<Image>();
        nameText = dialogueNode.transform.Find("NameText").GetComponent<TMP_Text>();
        dialogText = dialogueNode.transform.Find("DialogueText").GetComponent<TMP_Text>();
        buttonGroup = dialogueNode.transform.Find("OptionsGroup");
        background = dialogueNode.transform.Find("Background").GetComponent<Image>();
    }

    private void Start()
    {
        for (int i = 0;i < 200;i++)
        {
            canEnterDialog[i] = true;
        }
        dialogueNode.SetActive(false);
        // StartDialogue(dialogDataFile);
    }

    private void Update() //��ȷ�ϼ���z�����ߵ�����ͽ�����һ�仰�������Ľ�
    {

    }

    #endregion


    #region Dialogue

    public void UpdateText(string name, string text) // ��ʾ�ı���ͷ��
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

    public IEnumerator StartDialogue(DialogueTrigger dialogueTrigger) //��ʼ�Ի�
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

    public void ReadText(TextAsset textAsset) // �ѶԻ��ı��ָ�ɸ���
    {
        dialogRows = textAsset.text.Split('\n');
    }

    public void ShowDialogRow() // ��ʾ�Ի���
    {
        // Debug.Log(dialogIndex);
        for (int i = 1; i < dialogRows.Length; i++)
        {
            cells = dialogRows[i].Split(','); // �ѶԻ��зָ�ɸ�������

            if (int.Parse(cells[1]) != dialogIndex)
            {
                continue;
            }

            if (cells[0] == "End") // ����ǽ����ڵ�������Ի�
            {
                ExitDialogue();

                if (cells[6] != "") //���Ч����Ϊ�գ��򴥷���̬�¼�
                {
                    string[] effects = cells[6].Split('/'); // ��Ч����Ž��зָ�
                    foreach (string effect in effects)
                    {
                        DialogEffect(int.Parse(effect));
                    }
                }

                break;
            }

            if (cells[6] != "") //���Ч����Ϊ�գ��򴥷���̬�¼�
            {
                string[] effects = cells[6].Split('/'); // ��Ч����Ž��зָ�
                foreach (string effect in effects)
                {
                    DialogEffect(int.Parse(effect));
                }
            }

            if (cells[0] == "#") //�������ͨ�Ի��� ID �����ڽ��еĶԻ� ID ����ʾ
            {
                UpdateText(cells[2], cells[3]);
                Debug.Log("��ǰId"+dialogIndex);
                dialogIndex = int.Parse(cells[4]); // ��ת��һ���Ի�
                Debug.Log("��תId"+dialogIndex);
                break;
            }
            else if (cells[0] == "&") // �����ѡ��Ի�����ʾ��ť
            {
                UpdateText(cells[2], "");

                GenerateOption(i);
                break;
            }
        }
    }

    public void ContinueDialog() //������һ�жԻ�
    {
        ShowDialogRow();
    }

    public void GenerateOption(int index) // ���ɰ�ť����Ӱ�ť�¼�
    {
        isChoosing = true;
        string[] cells = dialogRows[index].Split(',');
        string[] conditions = cells[5].Split('/'); // ��������Ž��зָ�
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[3];

            // ����ť����¼�
            button.GetComponent<Button>().onClick.AddListener
            (
                delegate
                {
                    OnOptionClick(int.Parse(cells[4]));
                }
            );

            if (cells[5] != "") //���������������ж��Ƿ����ð�ť
            {
                foreach (string condition in conditions) //�����һ������������ͽ��ð�ť
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


    public void OnOptionClick(int id) // ��Ӱ�ť�¼�
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

    public void DialogEffect(int index) // ������̬�¼�
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

    public IEnumerator DisplayDialogue(string text)//һ����һ������ʾ�ı�
    {
        // Debug.Log("��ʼ����");
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
