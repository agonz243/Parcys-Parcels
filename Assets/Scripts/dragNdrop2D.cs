using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNdrop2D : MonoBehaviour
{
    public GameObject selectedObject;
    Vector3 offset;

    void Update()
    {
        //rotating function======================================
        void RotateByDegrees(GameObject wespin)
        {
            Vector3 rotationToAdd = new Vector3(0, 0, 45);
            wespin.transform.Rotate(rotationToAdd);
        }
        //========================================================

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //fetching mouse position relative to camera
        if (Input.GetMouseButtonDown(0)) //if LMB pressed
        {
            //mouse position finding bounds
            Debug.Log(mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition); //creating the collider of our target, with the overlap
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject) //if the mouse overlaps woth a draggable object (a collider) we move it 
        {
            selectedObject.transform.position = mousePosition + offset; //doing the moving
            if (Input.GetMouseButtonDown(1)) //if RMB pressed
            {
                //doing the actual rotating
                RotateByDegrees(selectedObject);
            }

        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //LMB raised while over movable object, aka dropping it
        {
            var currentPos = selectedObject.transform.position; //fetch the current objects position
            
            //doing grid snapping, rounding to nearest whole, need to scale by grid within bounds of our "bag' rectangle
            selectedObject.transform.position = new Vector3(Mathf.Round(currentPos.x),
                                         Mathf.Round(currentPos.y),
                                         Mathf.Round(currentPos.z));
            selectedObject = null;
        }
    }

}
