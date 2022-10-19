using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagButton : MonoBehaviour
{
    public void ShowItemStore()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (!SceneItemManager.Instance.interactive) { return; }
        ManagerSystem.Instance.ShowItemStore();
    }
}
