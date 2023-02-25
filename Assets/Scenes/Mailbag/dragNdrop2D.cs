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

    // Audio Sources
    public AudioSource clickSource;
    public AudioSource dropSource;

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
        /*

        //rotating function============================================================================================================
        void RotateByDegreesZeroto90(GameObject wespin, Vector3 wespinAngle)
        {
            Vector3 zero = new Vector3(0f, 0f, 0f);
            Vector3 ninety = new Vector3(0f, 0f, 90f);
            Vector3 one80 = new Vector3(0f, 0f, 180f);
            Vector3 two70 = new Vector3(0f, 0f, 270f);
            Vector3 three60 = new Vector3(0f, 0f, 360f);
            Vector3 rotationToAdd = new Vector3(0f, 0f, 0f);
            if (wespinAngle.z >= zero.z && wespinAngle.z < ninety.z)
            {
                 rotationToAdd = new Vector3(0f, 0f, 90f); //was 45
            }else if (wespinAngle.z >= ninety.z && wespinAngle.z < one80.z)
            {
                 rotationToAdd = new Vector3(0f, 0f, 180f);
            } else if (wespinAngle.z >= one80.z && wespinAngle.z < two70.z)
            {
                 rotationToAdd = new Vector3(0f, 0f, 270f);
            } else if (wespinAngle.z >= two70.z && wespinAngle.z < three60.z)
            {
                 rotationToAdd = new Vector3(0f, 0f, 360f);
            }

            //Vector3 rotationToAdd = new Vector3(0f, 0f, 90f); //was 45
            //might need to take an entire vector here not just .z?
            //need localRotation? dont think needed
            Vector3 currentRotation = wespin.transform.eulerAngles; //see if x y or z with the values

            currentRotation = new Vector3(Mathf.LerpAngle(currentRotation.x, rotationToAdd.x, Time.deltaTime),
                Mathf.LerpAngle(currentRotation.y, rotationToAdd.y, Time.deltaTime),
                Mathf.LerpAngle(currentRotation.z, rotationToAdd.z, Time.deltaTime));

            wespin.transform.eulerAngles = currentRotation;
        }

        //===============================================================================================================================
        /*
        void RotateByDegrees90to180(GameObject wespin)
        {
            Vector3 rotationToAdd = new Vector3(0f, 0f, 180f); //was 45
            //might need to take an entire vector here not just .z?
            //need localRotation? dont think needed
            Vector3 currentRotation = wespin.transform.eulerAngles; //see if x y or z with the values

            currentRotation = new Vector3(Mathf.LerpAngle(currentRotation.x, rotationToAdd.x, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.y, rotationToAdd.y, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.z, rotationToAdd.z, Time.deltaTime * 3));

            wespin.transform.eulerAngles = currentRotation;
        }

        void RotateByDegrees180to270(GameObject wespin)
        {
            Vector3 rotationToAdd = new Vector3(0f, 0f, 270f); //was 45
            //might need to take an entire vector here not just .z?
            //need localRotation? dont think needed
            Vector3 currentRotation = wespin.transform.eulerAngles; //see if x y or z with the values

            currentRotation = new Vector3(Mathf.LerpAngle(currentRotation.x, rotationToAdd.x, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.y, rotationToAdd.y, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.z, rotationToAdd.z, Time.deltaTime * 3));

            wespin.transform.eulerAngles = currentRotation;
        }


        void RotateByDegrees270to360(GameObject wespin)
        {
            Vector3 rotationToAdd = new Vector3(0f, 0f, 360f); //was 45
            //might need to take an entire vector here not just .z?
            //need localRotation? dont think needed
            Vector3 currentRotation = wespin.transform.eulerAngles; //see if x y or z with the values

            currentRotation = new Vector3(Mathf.LerpAngle(currentRotation.x, rotationToAdd.x, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.y, rotationToAdd.y, Time.deltaTime * 3),
                Mathf.LerpAngle(currentRotation.z, rotationToAdd.z, Time.deltaTime * 3));

            wespin.transform.eulerAngles = currentRotation;
        }

        */


        //==================================================================================================================================








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
            
            //if we are overlapping (aka clicked on) with something
            if (targetObject)
            {

                // Play click sound when piece is clicked
                /*
                if (!clickSource.isPlaying){
                    clickSource.Play();
                }
                */
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

                StartCoroutine(RotateMe(selectedObject, Vector3.forward * 90, 1.0f));

            }

        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //LMB raised while holding movable object, aka dropping it
        {
            /*
            if (!dropSource.isPlaying){
                dropSource.Play();
            }*/
            var currentPos = selectedObject.transform.position; //fetch the current objects position

            //taking instance of our gridManager to access the list of our vertices for each tile instantiated, using as snap points
            //snapPoints = gridCoords.tilesnapXY;
            //want to implement snap to "bag" grid here
            //Debug.Log(tilesnapXY);

            //currentPos.x = Mathf.Round(currentPos.x / gridWidth) * gridWidth;
            //currentPos.y = Mathf.Round(currentPos.y / gridHeight) * gridHeight;
             //selectedObject.transform.position = new Vector3(Mathf.Round(mousePosition.x),
                                        //Mathf.Round(mousePosition.y));

            //selected object set to null, no longer holding something
            selectedObject = null;
        }
    }


}
