using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNdrop2D : MonoBehaviour
{
    
    //taking instance of grid class, in order to access tile positioning for shape "snapping"
    public gridManager gridCoords;

    //public List<Vector3> snapPoints = new gridCoords.tilesnapXY;
    
    //defining for later use
    public GameObject selectedObject;
    Vector3 offset;

    public Vector3 targetPos;
    public float gridSize = 15f;

    void Update()
    {
        //rotating function======================================
        void RotateByDegrees(GameObject wespin)
        {
            Vector3 rotationToAdd = new Vector3(0, 0, 90); //was 45
            wespin.transform.Rotate(rotationToAdd);
        }
        //========================================================


    //grid snapping fucntion PLEASE GOD WORK IM GOING TO DO IT====================



    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
    //=============================================================================


    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //fetching mouse position relative to camera
        if (Input.GetMouseButtonDown(0)) //if LMB pressed
        {
            //mouse position finding bounds
            Debug.Log(mousePosition);

            //collider reference for mouse overlap on specific layer
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, -10, 10); 
            
            //if we are overlapping with something
            if (targetObject)
            {
                //if we clicked while overlapping w something, that is now our selected object (being held)
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject) //if currently holding an object with mouse click 
        {
            selectedObject.transform.position = mousePosition + offset; //doing the moving WAS PLUS OFFSET
            if (Input.GetMouseButtonDown(1)) //if RMB pressed
            {
                //doing the actual rotating, function above 
                RotateByDegrees(selectedObject);
            }

        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //LMB raised while holding movable object, aka dropping it
        {
            var currentPos = selectedObject.transform.position; //fetch the current objects position

            //want to implement snap to "bag" grid here
            //Debug.Log(tilesnapXY);

            //selectedObject.transform.position = new Vector3(
            //RoundToNearestGrid(currentPos.x),
            //RoundToNearestGrid(currentPos.y),
            //RoundToNearestGrid(currentPos.z));

            // selectedObject.transform.position = new Vector3(Mathf.Round(mousePosition.x),
            //                            Mathf.Round(mousePosition.y));

            //selected object set to null, no longer holding something
            selectedObject = null;
        }
    }


}
