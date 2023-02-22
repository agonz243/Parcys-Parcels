using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start method deleted
    [SerializeField]public float moveSpeed = 5f;

    // public float collisionOffset = 0.05f;
    // public ContactFilter2D movementFilter;
    // private Vector2 moveInput;
    // private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    public Rigidbody2D rigidBody;
    public Animator animator; //(will be used when sprites are done!) 

    Vector2 movement; // can store horizontal and verticle

    private int envelopesHeld; // The amount of envelopes currently held for the dog minigame

    public AudioSource squeakSource;
    public AudioSource envCollectSource;

    void Start()
    {
        envelopesHeld = 0;
        // rigidBody = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        // Movement (To have same moveSpeed * Time.FixedDeltaTime[amount of time elapsed since function was last called])
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Try to move player in input direction, followed by left right and up down input if failed
        // bool success = MovePlayer(moveInput);

        // if (!success){
        //     // Try Left / Right
        //     success = MovePlayer(new Vector2(moveInput.x , 0));

        //     if (!success){
        //         success = MovePlayer(new Vector2(0, moveInput.y));
        //     }
        // }
    }

    // public bool MovePlayer(Vector2 direction){
    //     // Check for potential collisions
    //     int count = rigidBody.Cast(
    //         direction,      // X and Y values b/w -1 and 1 that represent the direction from the body to look for collisions
    //         movementFilter, // The settings that determine where a colliusion can occur on such as layers to collide with
    //         castCollisions, // List of collisions to store the found collisions into after the Cast is finished
    //         moveSpeed * Time.fixedDeltaTime + collisionOffset);     // The amount to cast equal to the movement plus
        
    //     if (count == 0){
    //         Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;

    //         // No collisions
    //         rigidBody.MovePosition(rigidBody.position + moveVector);
    //         return true;
    //     }
    //     // } else {
    //     //     // Print collisions
    //     //     foreach (RaycastHit2D hit in castCollisions){
                
    //     //     }
    //     // }
    // }

    // COLLISION W/ SPRINKLERS
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Collectable"){
            envCollectSource.Play();
            Destroy(collision.gameObject);
            envelopesHeld++;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mailbox")
        {
            mailboxCheck mbc = collision.gameObject.GetComponent<mailboxCheck>();
            mbc.envelopesInBox += envelopesHeld;
            envelopesHeld = 0;

            if (mbc.envelopesInBox == 10){
                SceneManager.LoadScene("WinDogGame");
            }
        } else if (collision.gameObject.tag == "Enemy"){
            SceneManager.LoadScene("LoseDogGame");
        } else if (collision.gameObject.tag == "Obstacle"){
            squeakSource.Play();
            Debug.Log("Obstacle");
            moveSpeed -= 1;
        }
    }
}
