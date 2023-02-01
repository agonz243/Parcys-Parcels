using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNdrop2D : MonoBehaviour
{
    public GameObject selectedObject;
    Vector3 offset;
    Vector3 angle;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
            var rotate = 90;
            if (Input.GetMouseButtonDown(1))
            {
                angle = Vector3.forward * rotate;
                selectedObject.transform.eulerAngles = angle;
                rotate = rotate * 2;
            }
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log(rotate);
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }

}
