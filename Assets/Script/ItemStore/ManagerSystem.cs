using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSystem : MonoBehaviour
{
    public GameObject Canvas;
    // Start is called before the first frame update
    public void OnEnable()
    {
        Canvas.SetActive(true);
    }
    public void OnDisable()
    {
        Canvas.SetActive(false);
    }
}
