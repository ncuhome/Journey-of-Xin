using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    #region Properties

    public TextAsset dialogDataFile = null; // 对话文件，以 csv 形式保存]

    public int dialogIndex = 0;

    public bool autoEnterDialogue = false; // 是否在进入场景/房间 时就自动进行对话

    public bool DialogOnce = true; // 是否只能进行一次对话

    #endregion

    #region Unity Methods

    private void Start()
    {
        SceneItemManager.Instance.dialogueTriggers[dialogIndex] = this;
    }

    private void OnEnable() // 依靠 autoEnterDialogue 变量与触发器的激活来开启自动进入对话
    {

    }

    private void Update() // 如果鼠标在触发器内且点击了，就进入对话
    {
        if ((DialogueSystem.Instance) && (InputManager.Instance.sceneState == SceneState.MainScene))
        {
            AutoDialog();
        }
    }


    #endregion

    #region Trigger

    public void StartDialogue() //判断是否正在对话，如果没有正在对话则开始新的对话
    {
        if (DialogueSystem.inDialogue) { return; }
        if (!DialogueSystem.Instance.canEnterDialog[dialogIndex]) { return; }
        if (DialogOnce)
        {
            DialogueSystem.Instance.canEnterDialog[dialogIndex] = false;
        }
        DialogueSystem.Instance.StartCoroutine("StartDialogue", this);
    }

    public void AutoDialog()
    {
        if (autoEnterDialogue && DialogueSystem.Instance.canEnterDialog[dialogIndex])
        {
            {
                StartDialogue();
                this.gameObject.SetActive(false);
            }
        }

    }
    #endregion
}