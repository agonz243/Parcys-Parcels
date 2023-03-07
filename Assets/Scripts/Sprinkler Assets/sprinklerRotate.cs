using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinklerRotate : MonoBehaviour
{
    // public GameObject beam;
    public float rotationSpeed;
    public string axis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(axis)
        {
            case "x":
                transform.Rotate(new Vector3(rotationSpeed, 0, 0) * Time.deltaTime);
                break;
            case "y":
                transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
                break;
            case "z":
                transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
                break;
        }
        
    }
}
