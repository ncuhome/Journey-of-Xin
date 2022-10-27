using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretNumber : MonoBehaviour
{
    private DialogueTrigger correctDialog = null;
    private DialogueTrigger wrongDialog = null;

    public void Click()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (SceneItemManager.Instance.itemStates[GetComponent<ItemDisplay>().itemIndex] != ItemState.Interactive || !SceneItemManager.Instance.interactive) { return; }
        Debug.Log("SecretClick");
        if (StoreSystem.Instance.Find(20))
        {
            correctDialog.StartDialogue();
        }
        else
        {
            wrongDialog.StartDialogue();
        }
        SceneItemManager.Instance.interactive = true;
    }

    void Awake()
    {
        correctDialog = transform.Find("CorrectDialog").GetComponent<DialogueTrigger>();
        wrongDialog = transform.Find("WrongDialog").GetComponent<DialogueTrigger>();
    }

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0;
    }

    void Update()
    {
        if (StoreSystem.Instance.Find(20))
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}
