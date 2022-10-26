using System.Drawing;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.Instance.staticEventList[2] == 1)
        {
            GetComponent<Image>().color = UnityEngine.Color.green;
        }
        else
        {
            GetComponent<Image>().color = UnityEngine.Color.red;
        }
    }
}
