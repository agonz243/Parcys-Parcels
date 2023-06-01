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
    public SpriteRenderer objective;

    public Sprite objectiveEmpty;
    public Sprite objectiveFull;

    public GameObject mailboxArrow; // Arrow to highlight mailbox
    
    // Start is called before the first frame update
    void Start()
    {
        envelopesInInventory = 0;
        indicatorSprite = envelopeIndicator.GetComponent<SpriteRenderer>();
        objective.sprite = objectiveEmpty;
    }

    // Update is called once per frame
    void Update()
    {
         indicatorSprite.sprite = envelopeIndicatorSprites[envelopesInInventory];

         if (envelopesInInventory == 4)
         {
            mailboxArrow.SetActive(true);
            objective.sprite = objectiveFull;
         } else {
            objective.sprite = objectiveEmpty;
            mailboxArrow.SetActive(false);
         }
    }
}
