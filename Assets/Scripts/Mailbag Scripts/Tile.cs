using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] public int tileCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Countdown.counting){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


     public void Init(bool isOffset)
    {
        renderah.color = isOffset ? offsetColor : baseColor;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

        if(coll.gameObject.tag == "Tile/Grid")
        {
            Physics2D.IgnoreCollision( GetComponent<Collider2D>(), this.GetComponent<Collider2D>()); //switched order of operands
        }
        if(coll.collider.gameObject.tag == "Packages")
        {
            isSolved = true;
            tileCount++;
            coll.otherCollider.GetContacts(bonkArray);
        }
       
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        bonk = gameObject.GetComponent<Collider2D>();


        if (coll.gameObject.tag == "Tile/Grid")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }

        if (coll.gameObject.tag == "Packages")//AND THERE IS NOT ANOTHER PACKAGE ON THE SQUARE bonk.GetContacts(bonkArray) == 0
        {
            int numCollisions = this.GetComponent<Rigidbody2D>().GetContacts(bonkArray);
            tileCount--;
            if(tileCount != 0)
            {
                isSolved = true;
            }
            else
            {
                isSolved = false;
            }
        }

    }

    void OnMouseExit()
    {
        highlights.SetActive(false);
    }

}
