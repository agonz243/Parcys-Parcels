using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragNdrop2D : MonoBehaviour
{
    
    //taking instance of grid class, in order to access tile positioning for shape "snapping"
    public gridManager gridCoords;

    public List<Transform> snapPoints = new List<Transform>();
    
    //defining for later use
    public GameObject selectedObject;
    Vector3 offset;

    public Vector3 targetPos;
    public float gridSize = 10f;
        
    public int gridWidth = 104;
    public int gridHeight = 72;



    IEnumerator RotateMe(GameObject pckg, Vector3 byAngles, float inTime)
    {
        var fromAngle = pckg.transform.rotation;
        var toAngle = Quaternion.Euler(pckg.transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            pckg.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }


    void Update()
    {
        //grid snapping fucntion PLEASE GOD WORK IM GOING TO DO IT====================


        /*
            float RoundToNearestGrid(float pos)
            {
                    float closestDistance = -1;
                    Transform closestSnap = null;
                    foreach(Transform snapPoint in snapPoints)
                    {
                        //float currentDistance = Vector2.Distance()
                    }

            }
        */
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
            selectedObject.transform.position = mousePosition + offset; //doing the moving PLUS OFFSET
            if (Input.GetMouseButtonDown(1)) //if RMB pressed
            {
                //doing the actual rotating, function above 
                //Vector3 currRotash = selectedObject.transform.eulerAngles;

                //always going to start at a rotation of zero, REMEMBER TO ZERO THIS OUT AT PICK UP TIME
                //RotateByDegreesZeroto90(selectedObject, selectedObject.transform.eulerAngles);

                StartCoroutine(RotateMe(selectedObject, Vector3.forward * 90, 0.8f));

            }

        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //LMB raised while holding movable object, aka dropping it
        {
            var currentPos = selectedObject.transform.position; //fetch the current objects position

            //taking instance of our gridManager to access the list of our vertices for each tile instantiated, using as snap points
            //snapPoints = gridCoords.tilesnapXY;
            //want to implement snap to "bag" grid here
            //Debug.Log(tilesnapXY);

            currentPos.x = Mathf.Round(currentPos.x / gridWidth) * gridWidth;
            currentPos.y = Mathf.Round(currentPos.y / gridHeight) * gridHeight;
            // selectedObject.transform.position = new Vector3(Mathf.Round(mousePosition.x),
            //                            Mathf.Round(mousePosition.y));

            //selected object set to null, no longer holding something
            selectedObject = null;
        }
    }


}
