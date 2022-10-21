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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
