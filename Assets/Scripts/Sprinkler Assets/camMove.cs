using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camMove : MonoBehaviour
{
    [SerializeField]private float stopCam;
    [SerializeField]private float camMoveSpeed;
    [SerializeField]private Camera cam;

    private bool move = false;

    void Update(){
        if(move == true && cam.transform.position.x < stopCam){
            cam.transform.position = new Vector3(cam.transform.position.x + camMoveSpeed, cam.transform.position.y, cam.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Collision detected!");
        if(collision.gameObject.tag == "Player"){
            Debug.Log("Player collision detected!");
            move = true;
        }
    }
}
