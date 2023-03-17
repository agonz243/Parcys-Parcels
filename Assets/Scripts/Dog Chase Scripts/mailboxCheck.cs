using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mailboxCheck : MonoBehaviour
{

    [HideInInspector]
    public int envelopesInBox;
    public TextMeshProUGUI displayCount;
    public Sprite spriteWithMail;

    // Start is called before the first frame update
    void Start()
    {
        envelopesInBox = 0;
    }

    // Update is called once per frame
    void Update()
    {
        displayCount.text = envelopesInBox.ToString();

        if (envelopesInBox >= 1) { 
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteWithMail;
        }
    }
}
