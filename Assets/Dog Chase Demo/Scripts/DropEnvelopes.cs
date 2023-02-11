using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnvelopes : MonoBehaviour
{
    private float horizontalMin; // Min horizontal size of the play area
    private float horizontalMax; // Max horizontal size of the play area
    private float verticalMin; // Min vertical size of the play area
    private float verticalMax; // Max vertical size of the play area
    public float camEdgeOffset; // An offset to keep enevelopes from spawning on edge of camera

    private Camera cam;
    public int envelopeCount; // The number of envelopes to drop
    public GameObject envelope;

    // Start is called before the first frame update
    void Start()
    {
        cam  = Camera.main; // Get the main camera
        camEdgeOffset = 10;

        // Calculate the min and max size of camera in world units
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        // Bounds for spawining envelopes
        horizontalMin = -halfWidth + camEdgeOffset;
        horizontalMax =  halfWidth - camEdgeOffset;
        verticalMin = -halfHeight + camEdgeOffset;
        verticalMax = halfHeight - camEdgeOffset;


        // Spawn envelopes at random locations within bounds
        for (int i = 0; i < envelopeCount; i++) {
            // Create envelope clone
            GameObject currEnvelope = Instantiate(envelope);

            // Generate random location
            Vector2 randomPos = randomVec(); 

        // If an object exists at that position, regenerate a new position
         RaycastHit2D hit = Physics2D.Raycast(randomPos, -Vector2.up, 0f);
         int debugCheck = 0;
         while (hit.collider != null) 
         {
            randomPos = randomVec();
            hit = Physics2D.Raycast(randomPos, -Vector2.up, 0f);
            debugCheck++;
         }

         currEnvelope.transform.position = randomPos;
        }
    }

    Vector2 randomVec() 
    {
        float randomX = Random.Range(horizontalMin, horizontalMax);
        float randomY = Random.Range(verticalMin, verticalMax);
        Vector2 randomPosition = new Vector2(randomX, randomY);
        return randomPosition;
    }
}
