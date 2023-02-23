using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    public delegate void DragEndedDelegate(Draggable draggableObject);
    public DragEndedDelegate dragEndedCallback;

    private bool isDragged = false;
    private Vector3 mouseDragStartPos;
    private Vector3 spriteDragStartPos;

    private void OnMouseDown()
    {   
        isDragged = true;
        mouseDragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPos = transform.localPosition;
        
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = spriteDragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPos);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        dragEndedCallback(this);
    }

}
