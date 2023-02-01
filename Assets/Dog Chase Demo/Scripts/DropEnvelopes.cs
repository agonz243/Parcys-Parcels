using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnvelopes : MonoBehaviour
{
    private float horizontalMin; // Min horizontal size of the play area
    private float horizontalMax; // Max horizontal size of the play area
    private float verticalMin; // Min vertical size of the play area
    private float verticalMax; // Max vertical size of the play area

    private Camera cam;
    public int envelopeCount; // The number of envelopes to drop
    public GameObject envelope;
    //private GameObject[] envelopes;

    // Start is called before the first frame update
    void Start()
    {
        cam  = Camera.main; // Get the main camera

        // Calculate the min and max size of camera in world units
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        horizontalMin = -halfWidth;
        horizontalMax =  halfWidth;
        verticalMin = -halfHeight;
        verticalMax = halfHeight;


        for (int i = 0; i < envelopeCount; i++) {
            GameObject currEnvelope = Instantiate(envelope);
            currEnvelope.transform.position = new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
