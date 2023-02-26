using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //declaring the two diff tile colors based on logic in gridManager
    [SerializeField] private Color baseColor, offsetColor;

    //need to use sprite renderer for colors
    [SerializeField] private SpriteRenderer renderah;

    [SerializeField] private GameObject highlights;

    [SerializeField] public Collider2D bonk;

    [SerializeField] public ContactPoint2D[] bonkArray = new ContactPoint2D[9]; //here can tweak size of array for number of pieces

    [SerializeField] public bool isSolved = false;

     public void Init(bool isOffset)
    {
        renderah.color = isOffset ? offsetColor : baseColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Packages")
        {
            isSolved = true;
            Debug.Log("yoinkies");
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bonk = gameObject.GetComponent<Collider2D>();
        if (other.gameObject.tag == "Packages" && bonk.GetContacts(bonkArray) == 0)//AND THERE IS NOT ANOTHER PACKAGE ON THE SQUARE
        {
            isSolved = false;
            Debug.Log("NO YOINKIES");
        }

    }

    void OnMouseExit()
    {
        highlights.SetActive(false);
    }

}
