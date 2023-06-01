using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{


    public bool Placed { get; private set; }
    public BoundsInt area;
    public bool overlap;
    public bool bounds;
    public int packageOverlapCount;

    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private GameObject pckge;

    [SerializeField] public Collider2D collida;
    private void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
    }

    #region Collision Detect
    //here want to establish no overlap for boxes, on collision enter and exit used for tile collision transparency adjust, and box stacking check

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.GetComponent<Collider2D>().gameObject.tag == "Mailbox")
        {
            rend.color = new Color(1f, 1f, 1f, .4f); //want to adjust transparency here of our package
        }
        if (coll.GetComponent<Collider2D>().gameObject.tag == "Packages")
        {
            //want to get the base position of package on pickup, store, and set current grabbed object to the base position on overlap
        }

    }

    
    private void OnTriggerExit2D(Collider2D coll)
    {


        if (coll.GetComponent<Collider2D>().gameObject.tag == "Mailbox")
        {
            rend.color = new Color(1f, 1f, 1f, 1f);
        }

        if (coll.GetComponent<Collider2D>().gameObject.tag == "Packages")
        {


        }

    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "Packages")
        {
            packageOverlapCount++;
        }
        //if statement for bounds collision
        if(coll.collider.gameObject.tag == "Bounds")
        {
            bounds = true;
        }

        OverlapCheck();
    }



    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "Packages")
        {
            packageOverlapCount--;
        }

        if (coll.collider.gameObject.tag == "Bounds")
        {
            bounds = false;
        }

        OverlapCheck();
    }

    private void OverlapCheck() 
    {
        if (packageOverlapCount > 0) {
            overlap = true;
        } else {
            overlap = false;
        }
    }

    #endregion

}
