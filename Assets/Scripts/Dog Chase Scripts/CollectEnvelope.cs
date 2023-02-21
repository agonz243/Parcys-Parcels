using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnvelope : MonoBehaviour
{
    private void OnTriggerEnter2d(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Collected envelope");
            Destroy(col.gameObject);
        }
    }
}
