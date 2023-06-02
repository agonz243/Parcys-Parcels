using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParcy : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject movingObject;
    
    public void MoveReminder() {
        movingObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 1, playerObject.transform.position.z);
    }
}