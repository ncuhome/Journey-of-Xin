using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemManager : MonoBehaviour
{
    public static SceneItemManager Instance = null;
    public bool interactive = true;
    public GameObject IntoWorkTablePanel = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        IntoWorkTablePanel = GameObject.Find("Canvas/IntoWorkTable");
    }
    // Start is called before the first frame update
    void Start()
    {
        IntoWorkTablePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
