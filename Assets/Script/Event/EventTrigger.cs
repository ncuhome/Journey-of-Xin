using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
    public int eventIndex; // 触发的事件的编号
    public int conditionIndex; // 触发的前置条件编号，默认为 0 即无条件
    public bool autoEvent; // 是否在进入场景/房间 时就自动触发事件
    // Start is called before the first frame update
    #region Unity Methods

    private void Awake()
    {
        
    }

    private void OnEnable() // 依靠 autoEvent 变量与触发器的激活来开启自动触发事件
    {
        if (autoEvent && EventSystem.Instance.isStaticEvent(conditionIndex)){
            StartEvent();
        }
    }

    private void Update()
    {
               
    }

    #endregion

    #region TriggerEvent

    public void StartEvent()
    {
        if (eventIndex > 0)
        {
            EventSystem.Instance.ActiveEvent(eventIndex);
        }
        if (eventIndex < 0)
        {
            EventSystem.Instance.changeStaticEvent(eventIndex, true);
        }
        if (eventIndex == 0)
        {
            GetComponent<ItemDisplay>().DisplayStart();
        }
        
    }

    public void OnClickEvent()
    {
        if (EventSystem.Instance.isStaticEvent(conditionIndex))
        {
            StartEvent();
        } 
    }

    #endregion
}
