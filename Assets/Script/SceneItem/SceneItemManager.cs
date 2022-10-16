using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemManager : MonoBehaviour
{
    public static SceneItemManager Instance {get; private set;}
    public bool interactive = true;
    public GameObject intoWorkTablePanel = null;
    public GameObject letterBox = null;
    public GameObject lastLetter = null;
    public GameObject lastMail = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        intoWorkTablePanel = GameObject.Find("IntoWorkTable");
        letterBox = GameObject.Find("LetterBoxPanel");
        lastLetter = GameObject.Find("LastLetter");
        lastMail = GameObject.Find("LastMail");
    }
    // Start is called before the first frame update
    void Start()
    {
        intoWorkTablePanel.SetActive(false);
        letterBox.SetActive(false);
        lastLetter.SetActive(false);
        lastMail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
