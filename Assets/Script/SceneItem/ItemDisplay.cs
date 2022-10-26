using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���ڲ��ų����п��ռ���Ʒ����ʧ����
/// </summary>

public enum ItemState
{
    Invisible, NotInteractive, Interactive
}

public class ItemDisplay : MonoBehaviour
{
    private float a = 1.0f;
    private float timer = 0;
    private int status = 0;
    public int itemIndex = 0;
    private Vector3 target = Vector3.zero;
    public GameObject panelPrefab = null;
    public bool moveToCenter = true;
    public bool showInHighestLevel = true;
    public bool highLight = true;

    private GameObject panel = null;

    private Image itemImage = null;
    private Button itemButton = null;
    /// <summary>
    /// ???????????????????????????????????
    /// </summary>
    public void DisplayStart()
    {
        status = 1;
        GetComponent<Animator>().SetBool("Display", true);
        //InstantiatePanel();
    }

    public void Click()
    {
        //Debug.Log("Click");
        //if (DialogueSystem.Instance.inDialogue) { return; }
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (SceneItemManager.Instance.itemStates[itemIndex] != ItemState.Interactive || !SceneItemManager.Instance.interactive) { return; }

        //Debug.Log("EnableClick");
        SceneItemManager.Instance.interactive = false;
        target = new Vector3(-850, -440, 0);
        if (moveToCenter)
        {
            gameObject.transform.localPosition = Vector3.zero;
            GetComponent<Animator>().SetBool("Scale", true);
        }
        if (highLight)
        {
            GetComponent<Animator>().SetBool("Click", true);
        }
        if (showInHighestLevel)
        {
            transform.SetSiblingIndex(transform.parent.childCount);
        }
        if (GetComponent<DialogueTrigger>() != null)
        {
            GetComponent<DialogueTrigger>().StartDialogue();
        }
    }

    public void CancelClick()
    {
        GetComponent<Animator>().SetBool("Click", false);
        SceneItemManager.Instance.interactive = true;
    }

    void Awake()
    {
        itemImage = GetComponent<Image>();
        panelPrefab = Resources.Load<GameObject>("Pref/UI/Panel");

        // 设置图片中只有不透明的地方能触发响应
        itemImage.alphaHitTestMinimumThreshold = 0.1f;
        if (GetComponent<Button>() != null)
        {
            itemButton = GetComponent<Button>();
        }

    }

    void Start()
    {
        SceneItemManager.Instance.items[itemIndex] = this;

        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemState();

        switch (status)
        {
            case 1://???????????????
                gameObject.transform.localPosition += (target - gameObject.transform.localPosition) * Time.deltaTime * 3;
                if ((target - gameObject.transform.localPosition).magnitude <= 10)
                {
                    gameObject.transform.localPosition = target;
                }
                timer += Time.deltaTime;
                a -= Time.deltaTime * (1 / (1 + timer));
                gameObject.GetComponent<Image>().color = new Color(255, 255, 255, a);
                if (a <= 0.2f)
                {
                    status = 2;
                    SceneItemManager.Instance.interactive = true;
                }
                break;
            case 2:
                SceneItemManager.Instance.itemStates[itemIndex] = ItemState.Invisible;
                break;
        }
    }

    private void UpdateItemState()
    {
        if (SceneItemManager.Instance.itemStates[itemIndex] == ItemState.Invisible)
        {
            itemImage.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
        }

        if (itemButton != null)
        {
            if (SceneItemManager.Instance.itemStates[itemIndex] == ItemState.Interactive)
            {
                itemButton.interactable = true;
            }
            else
            {
                itemButton.interactable = false;
            }
        }
    }

    public void InstantiatePanel()
    {
        panel = Instantiate(panelPrefab);
        panel.transform.SetParent(this.transform.parent);
        panel.transform.position = Vector2.zero;
        panel.transform.SetSiblingIndex(panel.transform.parent.childCount);
    }

    public void UpdateItem()
    {
        switch (itemIndex)
        {
            case 0: SceneItemManager.Instance.lastLetter = this.gameObject; break;
            case 1: SceneItemManager.Instance.lastMail = this.gameObject; break;
            case 7: SceneItemManager.Instance.letterInTrashBin = this.gameObject; break;
            case 13: SceneItemManager.Instance.trashBin = this.gameObject; break;
            case 15: SceneItemManager.Instance.coffeeInMarket = this.gameObject; break;

            case 16: SceneItemManager.Instance.neutrinoDebugger = this.gameObject; break;
            case 17: SceneItemManager.Instance.blackMineral = this.gameObject; break;
            case 18: SceneItemManager.Instance.perspectiveGlass = this.gameObject; break;
            case 19: SceneItemManager.Instance.endlessEnergyMaker = this.gameObject; break;
            case 23: SceneItemManager.Instance.rebornMachineCE = this.gameObject; break;

        }


    }
}
