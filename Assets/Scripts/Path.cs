using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added for game timer text
using UnityEngine.SceneManagement;

public class Path : MonoBehaviour
{
    [SerializeField] Transform[] Points; // array to hold points

    [SerializeField]private float moveSpeed; // how fast the character moves

    [SerializeField]private float hideTimer = 2; // how long you can hide for

    [SerializeField]private float hitTimer = 2; // how long you're stunned when hit

    [SerializeField]private float gameTimer = 180; // timer for entire minigame

    [SerializeField]private GameObject brella; // temp for umbrella?

    [SerializeField]private Text timerText;

    private float keyAlt = 0; // used to check for alternate key press

    private int pointIndex; // for counting the points in the array

    private bool hide = false; // used to check if character is hidden

    private bool hideTimerRun = false; // used to start and stop the hiding timer

    private bool hitTimerRun = false; // used to start and stop the hit timer

    private bool hit = false; // used to check if character has been hit

    // private bool camMove = false; // used to translate camera

    // private bool stop = false;

    // private bool v3 = true;

    // private bool moved = true;

    private Rigidbody2D rb;

    [SerializeField]private Camera cam;


    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointIndex].transform.position; // starting the game goes to the first point
        brella.SetActive(false); // initially disable the umbrella
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // WHILE THERE ARE STILL POINTS TO GET TOWARDS
        if(pointIndex <= Points.Length - 1){
            // CAM OG 0.88
            
            
            
//             // TESTING CAMERA STUFF

//             if((pointIndex == 5 || pointIndex == 11) && v3 == true){
//                 camMove = true;
//                 stop = false;
//                 // moved = true;
//             } else{
//                 stop = false;
//                 camMove = false;
//                 // moved = false;
//             }
// //moved == false 
//             if(camMove == true && stop == false && v3 == true){
//                 cam.transform.position = new Vector3(cam.transform.position.x + 0.02F, cam.transform.position.y, cam.transform.position.z);
//                 if(((cam.transform.position.x) >= 19) || ((cam.transform.position.x >= 37))){
//                     Debug.Log("Camera debug: " + cam.transform.position.x);
//                     camMove = true;
//                     // if((pointIndex != 5) || (pointIndex != 11)){
//                         stop = true;
//                     // }
//                 }
//             }




            // TIMER STUFF (GAME TIMER)
            if(gameTimer >= 0){ // Decrease game timer
                gameTimer -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(gameTimer % 60);
                timerText.text = string.Format("{0}", seconds);
            } else{ // Else timer is up and player loses
                SceneManager.LoadScene("Lose");
                Debug.Log("Testing switchin scenes");
            }

            // CHARACTER IS HIDING

            if(hide && hideTimerRun){ // character is hidden
                if(hideTimer > 0){ // there is still time to hide
                    hideTimer -= Time.deltaTime; // subtract from the time
                    brella.transform.position = new Vector3(transform.position[0], transform.position[1], 0); // set umbrella position to player position
                    // rb.isKinematic = true; // disables collisions & applied forces for player
                } else{ // otherwise the timer is out
                    hide = false; // set hidden to false
                    hideTimerRun = false; // stop the timer
                    hideTimer = 2; // reset the timer
                    // rb.isKinematic = false; // enables collisions & applie dforces for player
                    brella.SetActive(false); // disable the umbrella
                }
            }

            // CHARACTER MOVEMENT

            if(!hide && !hit){
                // Character moves when alternating the "A" & "D" keys
                //       - requires the keyAlt variable

                //Position if moved when alternating the keys "A" and "D"
                if(Input.GetKeyDown(KeyCode.A) && keyAlt == 0){
                    transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed);
                    keyAlt = 1;
                } else if(Input.GetKeyDown(KeyCode.D) && keyAlt == 1){
                    transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed);
                    keyAlt = 0;
                }
            }

            // CHARACTER HIDE / SHIELD INPUT

            if(Input.GetKeyDown(KeyCode.DownArrow)){
                // GameObject.Find("brella").transform.position = transform.position(transform.position[0], transform.position[1], 0); // might break the code
                hide = true;
                hideTimerRun = true;
                brella.SetActive(true);
            }

            // HITTING A POINT MOVES THE INDEX TO THE NEXT ONE

            if(transform.position == Points[pointIndex].transform.position){
                pointIndex += 1; // change to the next point of travel
                Debug.Log("pointIndex: " + pointIndex);
            }
            
            // CHARACTER GOT HIT

            if(hit && hitTimerRun){
                if(hitTimer > 0){
                    hitTimer -= Time.deltaTime;
                    transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
                } else{
                    hit = false;
                    hitTimerRun = false;
                    hitTimer = 2;
                }
            }
        } else if(gameTimer >= 0 && pointIndex == Points.Length){ // Reach the end while there is still time
                SceneManager.LoadScene("Win");
            } 
    }
    
    // COLLISION (W/ SPRINKLERS)
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy" && !hide && !hit){
            hit = true;
            hitTimerRun = true;
        }
    }
}

// JUNK YARD

            // Character moves continuously - Time.deltaTime (consistent movement regardless of frame rate)
            // transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed * Time.deltaTime); 
            
            // Character moves when presing the "space" key
                //   - requires the keyAlt variable
                // if(Input.GetKeyDown("space")){
                //     transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed);
                // }

            // Return to previous point when hit

            // rb.isKinematic = true;
                // moveSpeed = 5F;
                // transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex - 1].transform.position, moveSpeed * Time.deltaTime);
                // if(transform.position == Points[pointIndex - 1].transform.position){
                //     hit = false;
                //     rb.isKinematic = false;
                //     moveSpeed = 0.15F;



