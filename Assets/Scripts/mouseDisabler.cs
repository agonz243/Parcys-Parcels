using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDisabler : MonoBehaviour
{
    
    void OnEnable()
    {
        Debug.Log("mouse disabled");
        Cursor.visible = false; 
    }
}
