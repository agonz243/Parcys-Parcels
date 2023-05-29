using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeInventory : MonoBehaviour
{   
    [HideInInspector]
    public int envelopesInInventory;
    public Sprite[] envelopeIndicatorSprites;
    public GameObject envelopeIndicator;
    private SpriteRenderer indicatorSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        envelopesInInventory = 0;
        indicatorSprite = envelopeIndicator.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         indicatorSprite.sprite = envelopeIndicatorSprites[envelopesInInventory];
    }
}
