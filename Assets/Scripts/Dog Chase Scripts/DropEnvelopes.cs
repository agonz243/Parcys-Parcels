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
    private Vector2 envelopeSize;
    public GameObject[] envelopes;

    public Sprite[] envelopeSprites;
    
    private Vector3 scaleUpVec;
    private Vector3 scaleDownVec;
    private float scaleTime;
    public float searchRadius;
    private bool envelopeInRadius; // Flag used for cluster check

    // Start is called before the first frame update
    void Start()
    {
        cam  = Camera.main; // Get the main camera
        camEdgeOffset = 10;
        envelopeCount = 10;
        scaleUpVec = new Vector3(10.0f, 10.0f, 0.0f);
        scaleDownVec = new Vector3(-0.25f, -0.25f, 0.0f);
        scaleTime = 0.0f;
        envelopes = new GameObject[envelopeCount];
        envelopeSize = envelope.GetComponent<BoxCollider2D>().bounds.size;
        //searchRadius = 500f; // Radius to check for other envelopes (avoids clustering)


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
            envelopes[i] = currEnvelope;

            // Scale envelope to largest size
            currEnvelope.transform.localScale += scaleUpVec;

            // Set sprite
            currEnvelope.gameObject.GetComponent<SpriteRenderer>().sprite = envelopeSprites[Random.Range(0, envelopeSprites.Length)];

            // Generate random location
            Vector2 randomPos = randomVec(); 

            // Check if another envelope is within range to avoid clustering
            //envelopeInRadius = checkInRadius(randomPos, searchRadius);

            // If an object exists at that position, regenerate a new position
            RaycastHit2D hit = Physics2D.BoxCast(randomPos, envelopeSize, 0f, Vector2.down, 0f);
            int debugCheck = 0;
            while (hit.collider != null) 
            {
                randomPos = randomVec();
                //envelopeInRadius = checkInRadius(randomPos, searchRadius);
                hit = Physics2D.BoxCast(randomPos, envelopeSize, 0f, Vector2.down, 0f);
                debugCheck++;
                if (debugCheck > 20) 
                { 
                    Debug.Log("Spawn limit exceeded, breaking");
                    break; 
                }
            }

            currEnvelope.transform.position = randomPos;
        }
    }

    void Update() 
    {
        scaleTime += Time.deltaTime;
        if (scaleTime < 0.8f) 
        {
            for (int n = 0; n < envelopeCount; n++) 
            {
                envelopes[n].transform.localScale += scaleDownVec;
            }
        }
    }

    Vector2 randomVec() 
    {
        float randomX = Random.Range(horizontalMin, horizontalMax);
        float randomY = Random.Range(verticalMin, verticalMax);
        Vector2 randomPosition = new Vector2(randomX, randomY);
        return randomPosition;
    }

    // Checks if any envelopes exist within a radius centered around a point
    // Returns true if there is an envelope within the radius, false otherwise
    bool checkInRadius(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name == "Envelope Collectable(Clone)")
            {
                // if envelope is in range, return true
                Debug.Log("Envelope in range hehe");
                return true;
            }
        }

        return false;
    }
}
