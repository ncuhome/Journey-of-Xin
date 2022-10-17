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
        Canvas.SetActive(false);
    }

    public void ShowItemStore()
    {
        Canvas.SetActive(true);
    }

    public void HideItemStore()
    {
        Canvas.SetActive(false);
    }
}
