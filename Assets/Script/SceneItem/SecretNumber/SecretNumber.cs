using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretNumber : MonoBehaviour
{
    private DialogueTrigger correctDialog = null;
    private DialogueTrigger wrongDialog = null;
    
    public void Click()
    {
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
