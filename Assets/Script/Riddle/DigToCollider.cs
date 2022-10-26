using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigToCollider : MonoBehaviour
{
    public DigTo system;
    private void OnMouseDown()
    {
        system.MouseDown(Input.mousePosition.x,Input.mousePosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
