using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    
    public GameObject Player;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        yOffset = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UI pos: " + this.transform.position);
        Debug.Log("player pos: " + Player.transform.position);
        this.transform.position = Player.transform.position;
        // this.transform.position.y -= yOffset;
    }
}
