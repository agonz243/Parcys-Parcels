using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnvelopeInventory : MonoBehaviour
{   
    [HideInInspector]
    public int envelopesInInventory;
    public TextMeshProUGUI envelopesCount;
    
    // Start is called before the first frame update
    void Start()
    {
        envelopesInInventory = 0;
    }

    // Update is called once per frame
    void Update()
    {
        envelopesCount.text = "Envelopes in Bag: " + envelopesInInventory.ToString();
    }
}
