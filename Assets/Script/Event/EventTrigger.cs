using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public int eventIndex; // 触发的事件的编号
    public int conditionIndex; // 触发的前置条件编号，默认为 0 即无条件
    public bool autoEvent; // 是否在进入场景/房间 时就自动触发事件
    public bool mouseEnter = false; // 鼠标是否在触发器中
    // Start is called before the first frame update
    #region Unity Methods

    private void OnEnable() // 依靠 autoEvent 变量与触发器的激活来开启自动触发事件
    {
        if (autoEvent && EventSystem.Instance.isStaticEvent(conditionIndex)){
            StartEvent();
        }
    }

    private void Update() // 如果鼠标在触发器内且点击了，就触发事件
    {
        if (Input.GetMouseButtonDown(0) && mouseEnter && EventSystem.Instance.isStaticEvent(conditionIndex))
        {
            StartEvent();
        }        
    }

    // 判断鼠标是否在触发器内
    private void OnMouseEnter() 
    {
        mouseEnter = true;
    }
    private void OnMouseExit() 
    {
        mouseEnter = false;
    }

    #endregion

    #region TriggerEvent

    public void StartEvent()
    {
        EventSystem.Instance.ActiveEvent(eventIndex);
    }

    #endregion
}
