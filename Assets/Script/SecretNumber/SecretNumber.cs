using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretNumber : MonoBehaviour
{
    private DialogueTrigger correctDialog = null;
    private DialogueTrigger wrongDialog = null;
    public int number = 0;
    
    public void Click()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (SceneItemManager.Instance.itemStates[GetComponent<ItemDisplay>().itemIndex] != ItemState.Interactive || !SceneItemManager.Instance.interactive) { return; }
        //if (StoreSystem.Find(20))
        if (true)
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
}
