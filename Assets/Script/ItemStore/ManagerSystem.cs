using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSystem : MonoBehaviour
{
    public static ManagerSystem Instance {get; private set;}
    public GameObject Canvas;
    // Start is called before the first frame update
    // public void OnEnable()
    // {
    //     Canvas.SetActive(true);
    // }
    // public void OnDisable()
    // {
    //     Canvas.SetActive(false);
    // }
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Canvas.SetActive(false);
    }

    public void ShowItemStore()
    {
        InputManager.Instance.sceneState = SceneState.Bag;
        Canvas.SetActive(true);
    }

    public void HideItemStore()
    {
        InputManager.Instance.sceneState = SceneState.MainScene;
        Canvas.SetActive(false);
    }
}
