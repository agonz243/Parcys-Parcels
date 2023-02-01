using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start method deleted
    [SerializeField]public float moveSpeed = 5f;
    
    public Rigidbody2D rigidBody;
    // public Animator animator; (will be used when sprites are done!) 

    Vector2 movement; // can store horizontal and verticle

    private int envelopesHeld; // The amount of envelopes currently held for the dog minigame

    void Start()
    {
        envelopesHeld = 0;
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
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Movement (To have same moveSpeed * Time.FixedDeltaTime[amount of time elapsed since function was last called])
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // COLLISION W/ SPRINKLERS
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Collectable"){
            Destroy(collision.gameObject);
            envelopesHeld++;
        } else if (collision.gameObject.tag == "Mailbox")
        {
            mailboxCheck mbc = collision.gameObject.GetComponent<mailboxCheck>();
            mbc.envelopesInBox += envelopesHeld;
            envelopesHeld = 0;
            
        }
    }
}
