using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// using UnityEngine.InputSystem;

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

    public GameObject player;

    // Countdown at the beginning of the scene
    public float countdownTimeLeft;
    public int beginGame;
    [SerializeField]private TextMeshProUGUI countdownTxt;
    public GameObject transRec;
    public float alpha = 0.1f;//half transparency
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

        beginGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginGame == 0){
            countdownTimeLeft -= Time.deltaTime;
            alpha -= 0.001f;

            transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, alpha);

            if (seconds != Mathf.FloorToInt(countdownTimeLeft % 60)){
                squeakSource.Play();
            }

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
                dummyDog.SetActive(false);
                dogAI.SetActive(true);
                stopTime.SetActive(true);
                barkSource.Play();
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

            // changes angle of sprite based on what direction its moving
            if(movement.x == 1 && movement.y == 1) {
                float angle = player.transform.eulerAngles.z;
                float rotateAngle = 315f - angle;
                if(Mathf.Floor(angle) != 315f) {
                    player.transform.Rotate (0, 0, rotateAngle);
                }
            } else if (movement.x == -1 && movement.y == 1) {
                float angle = player.transform.eulerAngles.z;
                float rotateAngle = 45f - angle;
                if(Mathf.Floor(angle) != 45f) {
                    player.transform.Rotate (0, 0, rotateAngle);
                }
            // } else if (movement.x == 1 && movement.y == -1) {
            //     float angle = transform.eulerAngles.z;
            //     float rotateAngle = 45f - angle;
            //     if(Mathf.Floor(angle) != 45f) {
            //         transform.Rotate (0, 0, rotateAngle);
            //     }
            // } else if (movement.x == -1 && movement.y == -1) {
            //     float angle = transform.eulerAngles.z;
            //     float rotateAngle = 315f - angle;
            //     if(Mathf.Floor(angle) != 315f) {
            //         transform.Rotate (0, 0, rotateAngle);
            //     }
            } else {
                float angle = player.transform.eulerAngles.z;
                float rotateAngle = 0f - angle;
                if(Mathf.Floor(angle) != 0f) {
                    player.transform.Rotate (0, 0, rotateAngle);
                }
            }


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
}
