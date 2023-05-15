using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAnimation : MonoBehaviour
{
    public Animator rightMouseAnimator;

    public SpriteRenderer boxRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RotateBox() {
        boxRenderer.transform.Rotate (new Vector3 (0, 0, -90));
    }
}
