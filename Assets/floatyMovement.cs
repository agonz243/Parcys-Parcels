using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatyMovement : MonoBehaviour
{
    private float floatRange = 3.0f;
    private float speed = 3.0f;
    private float startX;
    private float startY;
    private float startZ;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startX + Mathf.Sin(Time.time * speed) * floatRange / 2.0f, startY, startZ);
    }
}
