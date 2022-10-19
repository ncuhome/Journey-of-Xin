using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ???????????��????????????????
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
    private GameObject panel = null;
    public bool moveToCenter = true;

    private Image itemImage = null;
    private Button itemButton = null;
    /// <summary>
    /// ???????????????????????????????????
    /// </summary>
    public void DisplayStart()
    {
        status = 1;
        GetComponent<Animator>().SetBool("Display", true);
        panel.SetActive(false);
    }

    public void Click()
    {
        //if (DialogueSystem.Instance.inDialogue) { return; }
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (SceneItemManager.Instance.itemStates[itemIndex] != ItemState.Interactive || !SceneItemManager.Instance.interactive) { return; }

        SceneItemManager.Instance.interactive = false;
        target = new Vector3(-850, -440, 0);
        if (moveToCenter)
        {
            gameObject.transform.localPosition = Vector3.zero;
            GetComponent<Animator>().SetBool("Scale", true);
        }
        GetComponent<Animator>().SetBool("Click", true);
        if (GetComponent<DialogueTrigger>() != null)
        {
            GetComponent<DialogueTrigger>().StartDialogue();
            panel.SetActive(true);
        }
        transform.SetSiblingIndex(transform.parent.childCount);
    }

    void Awake()
    {
        itemImage = GetComponent<Image>();

        // 设置图片中只有不透明的地方能触发响应
        itemImage.alphaHitTestMinimumThreshold = 0.1f;
        if (GetComponent<Button>() != null)
        {
            itemButton = GetComponent<Button>();
        }
        panel = GameObject.Find("Canvas/Panel");
    }

    void Start()
    {
        panel.SetActive(false);
        SceneItemManager.Instance.itemStates[itemIndex] = ItemState.Interactive;
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
}
