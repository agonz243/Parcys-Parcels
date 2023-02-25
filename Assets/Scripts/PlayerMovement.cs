using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start method deleted
    [SerializeField]public float moveSpeed = 25f;
    
    public Rigidbody2D rigidBody;
    public Animator animator; //(will be used when sprites are done!) 

    Vector2 movement; // can store horizontal and verticle

    private int envelopesHeld; // The amount of envelopes currently held for the dog minigame
    private bool speedDecreased; // Speed decreased when holding more than 3 envelopes

    public AudioSource squeakSource;
    public AudioSource envCollectSource;
    public AudioSource barkSource;
    public AudioSource mailboxSource;

    // Initialize variables to have the dog bark randomly
    public float timer;
    public float randomNum;

    void Start()
    {
        envelopesHeld = 0;
        randomNum = Random.Range(1.0f, 6.0f);
        timer = 0.0f;
        speedDecreased = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    void FixedUpdate()
    {
        // Movement (To have same moveSpeed * Time.FixedDeltaTime[amount of time elapsed since function was last called])
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // COLLISION W/ SPRINKLERS
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Collectable"){
            envCollectSource.Play();
            Destroy(collision.gameObject);
            EnvelopeInventory envI = this.GetComponent<EnvelopeInventory>();
            envI.envelopesInInventory ++;
            envelopesHeld++;
            if (envelopesHeld > 3){
                moveSpeed --;
                speedDecreased = true;
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mailbox")
        {
            mailboxCheck mbc = collision.gameObject.GetComponent<mailboxCheck>();
            EnvelopeInventory envI = this.GetComponent<EnvelopeInventory>();
            mbc.envelopesInBox += envelopesHeld;
            envelopesHeld = 0;
            envI.envelopesInInventory = 0;
            if (speedDecreased == true){
                speedDecreased = false;
                moveSpeed = 25f;
            }
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
            // moveSpeed -= 1;
        }
    }
}
