using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStrokeCollider : MonoBehaviour
{
    public AStroke RiddleSystem;
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
}
