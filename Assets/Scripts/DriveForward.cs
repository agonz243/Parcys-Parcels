using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveForward : MonoBehaviour
{
    public GameObject road;
    public GameObject houses;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space")) 
        {
            iTween.MoveBy(houses, iTween.Hash("y", -20, "easeType", 
                "easeInOutCubic", "time", 3f));

            iTween.MoveBy(road, iTween.Hash("y", -20, "easeType", 
                "easeInOutCubic", "time", 3f));
        }
    }
}
