using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("mouse enabled");
        Cursor.visible = true; 
    }
}
