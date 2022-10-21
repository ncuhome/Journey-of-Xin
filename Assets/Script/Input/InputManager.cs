using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState
{
    MainScene, Bag, Workbench, Dialog, Animation , Menu
}
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public SceneState sceneState = SceneState.MainScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("有另外的实例");
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((sceneState == SceneState.MainScene)||(sceneState == SceneState.Menu))
        {
            MenuOptions.Instance.InputDetect();
        }
        if (sceneState == SceneState.Dialog)
        {
            DialogueSystem.Instance.InputDetect();
        }
        if (sceneState == SceneState.Bag)
        {
            StoreManager.Instance.InputDetect();
        }
        if (sceneState == SceneState.Workbench)
        {
            WorkbenchSystem.Instance.InputDetect();
        }
    }
}
