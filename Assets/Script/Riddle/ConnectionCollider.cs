using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCollider : MonoBehaviour
{
    public Connection RiddleSystem;
    private void OnMouseDown()
    {
        RiddleSystem.MouseDown(Input.mousePosition.x, Input.mousePosition.y);
    }
    private void OnMouseDrag()
    {
        RiddleSystem.MouseDrag(Input.mousePosition.x, Input.mousePosition.y);
    }
    private void OnMouseUp()
    {
        RiddleSystem.MouseUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
