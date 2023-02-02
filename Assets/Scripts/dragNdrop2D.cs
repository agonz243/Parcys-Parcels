using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNdrop2D : MonoBehaviour
{
    public GameObject selectedObject;
    Vector3 offset;
    Vector3 angle;
    //Vector3 gridSnap;
    void Update()
    {
        var rotate = 90;
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
        if (selectedObject) //if the mouse overlaps woth a draggable object (a collider) we move it 
        {
            selectedObject.transform.position = mousePosition + offset; //doing the moving
            if (Input.GetMouseButtonDown(1)) //right click controls for rotation
            {
              
                angle = Vector3.forward * rotate; //z axis rotation with (0, 0, 90) as vector, for 90 degree rotation
                selectedObject.transform.eulerAngles = angle; //doing the actual rotating

                
            }
            if (Input.GetMouseButtonUp(1))
            {

                Debug.Log(rotate);
            }

        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //LMB raised while over movable object, aka dropping it
        {
            var currentPos = selectedObject.transform.position; //fetch the current objects position
            //gridSnap = (Mathf.Round(currentPos.x),
            //                             Mathf.Round(currentPos.y),
            //                             Mathf.Round(currentPos.z));
            selectedObject.transform.position = new Vector3(Mathf.Round(currentPos.x),
                                         Mathf.Round(currentPos.y),
                                         Mathf.Round(currentPos.z));
            selectedObject = null;
        }
    }

}
