using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{


    public bool Placed { get; private set; }
    public BoundsInt area;

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

       //Debug.Log("MAkinit");
        
            Debug.Log("MAkinit");

            if (coll.collider.gameObject.tag == "Packages")
        {
            //want to set the original coords of the package where it was last set down (maybe origin set down?) could make sure it saves coords of spawn location
            Debug.Log("POGGERS");
            //if package is being held CURRENTLY
            //if(selectedObject == null)
            //{
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("OHMYFGOAFWAfera");
                //this.gameObject.transform.position = setXY(this.gameObject.transform.position, Random.Range(85, 117), Random.Range(-15, 72));
                this.gameObject.transform.position = new Vector3(0, 0, 0);
                //}
                //else
                //{

                //}
            }

        }
      
        
    }

    #endregion


    //setting X and Y values 
    public Vector3 setXY(Vector3 pckg, float x, float y)
    {
        pckg.x = x;
        pckg.y = y;
        return pckg;
    }
}
