using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public delegate void CallBack();

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public float time = 0f;
    public bool timeRecordStart = false;
    public float targetTime = 0f;
    public int roomIndex = 0;
    public int planetIndex = 0;
    public int callbackIndex = 0;
    public bool needStay = true;
    public CallBack callBack = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (((RoomManager.Instance.roomIndex != roomIndex) || (RoomManager.Instance.planetIndex != planetIndex)) && (needStay))
        {
            time = 0;
            return;
        }

        if (timeRecordStart)
        {
            time += Time.deltaTime;
            if (time >= targetTime)
            {
                timeRecordStart = false;
                time = 0;
                callBack();
            }
        }
    }

    public void StartTimeRecord(float target, int planet, int room, int call, bool stay)
    {
        planetIndex = planet;
        needStay = stay;
        roomIndex = room;
        callbackIndex = call;
        callBack = GetCallback(call);
        time = 0;
        targetTime = target;
        timeRecordStart = true;
    }

    public void StopTimeRecord()
    {
        timeRecordStart = false;
        time = 0;
    }

    public static CallBack GetCallback(int callbackIndex)
    {
        switch (callbackIndex)
        {
            case 1: return CeController.Instance.Click1;
            case 2: return CeController.Instance.CEMoveToMainRoom;
            case 3: return EventSystem.Instance.IncomingLetter;
            case 4: return CeController.Instance.CEMoveToMainRoom;
            case 5: return EventSystem.Instance.StartDialogNode12;
            case 6: return EventSystem.Instance.StartDialogRemind1;
            case 7: return EventSystem.Instance.EnterBlackMarket;
            case 8: return EventSystem.Instance.ReturnToMainControlRoom;
        }
        return null;
    }
}
