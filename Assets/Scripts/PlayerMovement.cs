using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Start method deleted
    [SerializeField]public float moveSpeed = 35f;
    
    public Rigidbody2D rigidBody;
    public Animator animator; //(will be used when sprites are done!) 

    Vector2 movement; // can store horizontal and verticle

    private int envelopesHeld; // The amount of envelopes currently held for the dog minigame

    public AudioSource squeakSource;
    public AudioSource envCollectSource;
    public AudioSource barkSource;
    public AudioSource mailboxSource;

    // Initialize variables to have the dog bark randomly
    public float timer;
    public float randomNum;

    // Limits how many enevelopes the player can carry
    [SerializeField] private int envelopeLimit = 4;

    private int noCollisionParcyLayer;
    private int defaultLayer = 0;

    // Countdown at the beginning of the scene
    public float countdownTimeLeft;
    public int beginGame;
    [SerializeField]private TextMeshProUGUI countdownTxt;
    public GameObject transRec;
    public float alpha = 0.6f;//half transparency
    private Material currentMat;

    public GameObject dogAI;
    public GameObject dummyDog;

    public GameObject stopTime;

    public float seconds;

    void Start()
    {
        envelopesHeld = 0;
        randomNum = Random.Range(1.0f, 6.0f);
        timer = 0.0f;
        noCollisionParcyLayer = LayerMask.NameToLayer("ParcyNoCollectable");
        
        // Countdown initializations:
        beginGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginGame == 0){
            countdownTimeLeft -= Time.deltaTime;
            
            alpha -= 0.001f;

            transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, alpha);

            seconds = Mathf.FloorToInt(countdownTimeLeft % 60);

            Debug.Log(alpha);

            if (countdownTimeLeft < 1){
                countdownTxt.text = string.Format("GO");
            } else {
                countdownTxt.text = string.Format("{0}", seconds);
            }

            if(countdownTimeLeft <= 0) {
                countdownTimeLeft = 0;
                countdownTxt.text = string.Format("GO");
                countdownTxt.faceColor = new Color32(0, 0, 0, 0);
                transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, 0f);
                beginGame = 1;
                dummyDog.SetActive(false);
                dogAI.SetActive(true);
                stopTime.SetActive(true);
            }
            
        } else {
            if (envelopesHeld >= envelopeLimit){
                gameObject.layer = noCollisionParcyLayer;
            } else {
                gameObject.layer = defaultLayer;
            }
            // not good to do physics related stuff here where framerate can change

            // Input
            // Might be a newer input system
            // GetAxisRaw provides val between -1 & 1 (Left = -1) (Right = 1) (Nothing = 0)
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            // Lines below used for changing the sprite during player movement!
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            timer += Time.deltaTime;

            if (timer > randomNum){
                barkSource.Play();
                randomNum = Random.Range(1.0f, 6.0f);
                timer = 0.0f;
            }
        }

        // updateCountdown(countdownTimeLeft);

    }

    void FixedUpdate()
    {
        // Movement (To have same moveSpeed * Time.FixedDeltaTime[amount of time elapsed since function was last called])
        rigidBody.MovePosition(rigidBody.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Collectable" && envelopesHeld < envelopeLimit){
            envCollectSource.Play();
            Destroy(collision.gameObject);
            EnvelopeInventory envI = this.GetComponent<EnvelopeInventory>();
            envI.envelopesInInventory ++;
            envelopesHeld++;
        } 

        if (collision.gameObject.tag == "Mailbox")
        {
            mailboxCheck mbc = collision.gameObject.GetComponent<mailboxCheck>();
            EnvelopeInventory envI = this.GetComponent<EnvelopeInventory>();
            mbc.envelopesInBox += envelopesHeld;
            envelopesHeld = 0;
            envI.envelopesInInventory = 0;
            mailboxSource.Play();

            if (mbc.envelopesInBox == 10){
                scoreTracker.dogWin = true;
                SceneManager.LoadScene("WinDogGame");
            }
        } else if (collision.gameObject.tag == "Enemy"){
            SceneManager.LoadScene("LoseDogGame");
        } else if (collision.gameObject.tag == "Obstacle"){
            squeakSource.Play();
            Debug.Log("Obstacle");
        }
    }

    // void ChangeAlpha(Material mat, float alphaVal)
    // {
    //     Color oldColor = mat.color;
    //     Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
    //     mat.SetColor("_Color", newColor);
    // }
}
