using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dragNdrop2D : MonoBehaviour
{
    
    //taking instance of grid class, in order to access tile positioning for shape "snapping"
    public gridManager gridCoords;

    public List<Transform> snapPoints = new List<Transform>();

    //float for where to snap for grid squares
    public float snapRange = 10.0f;

    //defining for later use
    public GameObject selectedObject;
    Vector3 offset;

    //here keeping track of values for testing (OLD VALUES)
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

    public void snapFncn(GameObject pckg, List<Transform> ex)
    {
        //set float for snap distance limit
        float closestDist = -1;
        //on start, there is not a closest tile
        Transform closestTile = null;
        //want to  run through each position of every tile in grid and make them snappable
        //here we define the behavior for snapping, if we are within a range of an object, it becomes the object to snap to
        foreach(Transform snapTile in ex)
        {
            //get the distance from the transform(ie tile) to the package
            float currDist = Vector2.Distance(pckg.transform.localPosition, snapTile.localPosition);

            //if we dont have a current closest tile, or we are closer than prev
            if(closestTile == null || currDist < closestDist)
            {
                //closest tile is now the tileobj closest
                closestTile = snapTile;
                closestDist = currDist;
            }
        }

        if(closestTile != null && closestDist <= snapRange)
        {
            pckg.transform.localPosition = closestTile.localPosition;
        }
    }

    void Start()
    {
        gridCoords = GameObject.Find("gridManager").GetComponent<gridManager>();
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

        */


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //fetching mouse position relative to camera
        if (Input.GetMouseButtonDown(0)) //if LMB pressed
        {
            //mouse position finding bounds
            //Debug.Log(mousePosition);

            //collider reference for mouse overlap on specific layer
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, -10, 10);
            Debug.Log("HOVERING OBJ");
            
            //if we are overlapping (aka clicked on) with something
            if (targetObject)
            {

                // Play click sound when piece is clicked
                if (!clickSource.isPlaying){
                    clickSource.Play();
                }
                
                //if we clicked while overlapping w something, that is now our selected object (being held)
                selectedObject = targetObject.transform.gameObject;
                Debug.Log("SELECTING OBJ");
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject) //if currently holding an object with mouse click 
        {
            Debug.Log("OBJ HELD");
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

            if (!dropSource.isPlaying){
                dropSource.Play();
            }


            var currentPos = selectedObject.transform.position; //fetch the current objects position
            
            //taking instance of our gridManager to access the list of our vertices for each tile instantiated, using as snap points
            snapPoints = gridCoords.tilesnapXY;
            snapFncn(selectedObject, snapPoints);
            
            //end state here
            bool winnered;
            var test = FindObjectOfType<gridManager>();
            winnered = test.SolveaDaPuzzle();
            
            if(winnered == true)
            {
                SceneManager.LoadScene("WinPuzzleGame");
            }

            //currentPos.x = Mathf.Round(currentPos.x / gridWidth) * gridWidth;
            //currentPos.y = Mathf.Round(currentPos.y / gridHeight) * gridHeight;
             //selectedObject.transform.position = new Vector3(Mathf.Round(mousePosition.x),
                                        //Mathf.Round(mousePosition.y));


            //selected object set to null, no longer holding something
            selectedObject = null;
            Debug.Log("OBJ NULL");
        }
    }


}
