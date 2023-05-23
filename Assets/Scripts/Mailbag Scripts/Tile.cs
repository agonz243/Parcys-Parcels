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

    // Countdown at the beginning of the scene
    public float countdownTimeLeft;
    public int beginGame;
    [SerializeField]private TextMeshProUGUI countdownTxt;
    public GameObject transRec;
    public float alpha = 0.1f;//half transparency
    private Material currentMat;
    public GameObject stopTime;
    public float seconds;

    // Start is called before the first frame update
    void Start()
    {
        // Bool for Countdown
        beginGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginGame == 0){
            countdownTimeLeft -= Time.deltaTime;
            alpha -= 0.001f;

            transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, alpha);

            // if (seconds != Mathf.FloorToInt(countdownTimeLeft % 60)){
            //     squeakSource.Play();
            // }

            seconds = Mathf.FloorToInt(countdownTimeLeft % 60);

            // Debug.Log(alpha);

            if (countdownTimeLeft < 1){
                countdownTxt.outlineColor = new Color (0.12f, 0.48f, 0.16f, 1f); // dark green outline
                countdownTxt.faceColor = new Color (0f, 1f, 0f, 1f); // green font
                countdownTxt.text = string.Format("GO!");
            } else if (countdownTimeLeft < 2){
                countdownTxt.outlineColor = new Color (1f, 0.67f, 0.04f, 1f); // dark yellow outline
                countdownTxt.faceColor = new Color (1f, 0.78f, 0.31f, 1f); // yellow font
                countdownTxt.text = string.Format("{0}", seconds);
            } else if (countdownTimeLeft < 3) {
                countdownTxt.outlineColor = new Color (0.91f, 0.45f, 0.30f, 1f); // dark orange outline
                countdownTxt.faceColor = new Color(1.0f, 0.64f, 0.0f, 1f); // orange font
                countdownTxt.text = string.Format("{0}", seconds);
            } else if (countdownTimeLeft < 4) {
                // countdownTxt.outlineColor = new Color (0.51f, 0.20f, 0.25f, 1f); // dark red outline
                countdownTxt.outlineColor = new Color (0.47f, 0.09f, 0.15f, 1f); // dark red outline
                countdownTxt.faceColor = new Color (0.66f, 0.14f, 0.21f, 1f); // red font
                countdownTxt.text = string.Format("{0}", seconds);
            }

            if(countdownTimeLeft <= 0) {
                countdownTimeLeft = 0;
                // countdownTxt.text = string.Format("GO!");
                countdownTxt.faceColor = new Color32(0, 0, 0, 0);
                countdownTxt.outlineWidth = 0.0f;
                countdownTxt.outlineColor = new Color32(0, 0, 0, 0);
                transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, 0f);
                beginGame = 1;
                stopTime.SetActive(true);
            }

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
