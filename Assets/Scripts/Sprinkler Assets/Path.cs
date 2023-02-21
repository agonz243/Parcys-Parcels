using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added for game timer text
using UnityEngine.SceneManagement;

public class Path : MonoBehaviour
{
    // UNITY EDITOR VARIABLES

    // drag in points from the hierarchy into the array
    [SerializeField] Transform[] Points; // array to hold points

    // amount of distance player travels when a key is pressed, can be adjusted in UI
    [SerializeField]private float moveSpeed = 0.225F; // how fast the character moves

    // was adjusted from 2 --> 1
    [SerializeField]private float hideTimer = 1; // how long you can hide for

    // was adjusted from 2 --> 2.5
    [SerializeField]private float hitTimer = 2.5F; // how long you're stunned when hit

    // adjusted based on the version / playtesting
    [SerializeField]private float gameTimer = 45; // timer for entire minigame
    
    [SerializeField]private int brellaUse = 5; // how many times the umbrella can be used

    // for visuals without assets, drag brella from hierarchy into UI
    [SerializeField]private GameObject brella; // temp for umbrella?

    // drag the text under canvas into UI
    [SerializeField]private Text timerText;

    [SerializeField]private Text livesText;

    [SerializeField]private Text brellaText;

    // drag the camera into UI to reset camera position after scrolling
    [SerializeField]private Camera cam;

    private float keyAlt = 0; // used to check for alternate key press forwards

    private float backKeyAlt = 0; // used to check for alternate key press backwards

    private int pointIndex = 0; // for counting the points in the array

    private bool hide = false; // used to check if character is hidden

    private bool hideTimerRun = false; // used to start and stop the hiding timer

    private bool hitTimerRun = false; // used to start and stop the hit timer

    private bool hit = false; // used to check if character has been hit

    private int lives = 3; // for player live 

    public AudioSource wetSource;
    public AudioSource umbrellaSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointIndex].transform.position; // starting the game goes to the first point
        brella.SetActive(false); // initially disable the umbrella
        cam.transform.position = new Vector3(0.88F, cam.transform.position.y, cam.transform.position.z); // restarting camera position (for V3)
    }

    // Update is called once per frame
    void Update()
    {
        // WHILE THERE ARE STILL POINTS TO GET TOWARDS
        if(pointIndex <= Points.Length - 1){

            // TIMER STUFF (GAME TIMER) & SCENE WIN / LOSE TRANSITION
            if(gameTimer > 0){ // Decrease game timer
                gameTimer -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(gameTimer % 60);

                // TEXT STUFF (TIMER, LIVES, UMBRELLA USES)
                // timerText.text = string.Format("{0}", seconds);
                // livesText.text = string.Format("{0}", lives);
                // brellaText.text = string.Format("{0}", brellaUse);
            } else{ // Else timer is up and player loses
                SceneManager.LoadScene("Lose");
            }

            if(lives == 0){
                SceneManager.LoadScene("Lose");
            }

            // CHARACTER IS HIDING
            if(hide && hideTimerRun){ // character is hidden
                if(hideTimer > 0){ // there is still time to hide
                    hideTimer -= Time.deltaTime; // subtract from the time
                    brella.transform.position = new Vector3(transform.position[0], transform.position[1], 0); // set umbrella position to player position
                } else{ // otherwise the timer is out
                    hide = false; // set hidden to false
                    hideTimerRun = false; // stop the timer
                    hideTimer = 1; // reset the timer
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

                //Position if moved when alternating the keys "Z" and "C"
                if(Input.GetKeyDown(KeyCode.Z) && backKeyAlt == 0){
                    transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex - 1].transform.position, moveSpeed);
                    Debug.Log("pointIndex: " + pointIndex);
                    backKeyAlt = 1;
                } else if(Input.GetKeyDown(KeyCode.C) && backKeyAlt == 1){
                    Debug.Log("pointIndex: " + pointIndex);
                    transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex - 1].transform.position, moveSpeed);
                    backKeyAlt = 0;
                }
            }

            // CHARACTER HIDE / SHIELD INPUT
            //      enables hiding and puts umbrella on screen
            if(Input.GetKeyDown(KeyCode.W) && !hide && !hit && (brellaUse != 0)){ // can adjust back to KeyCode.DownArrow
                umbrellaSource.Play();
                hide = true; 
                hideTimerRun = true;
                brella.SetActive(true);
                brellaUse --;
            }

            // HITTING A POINT MOVES THE INDEX TO THE NEXT ONE
            if(transform.position == Points[pointIndex].transform.position){
                pointIndex += 1; // change to the next point of travel
            }
            
            // CHARACTER GOT HIT
            if(hit && hitTimerRun){
                if(hitTimer > 0){
                    hitTimer -= Time.deltaTime;
                    transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
                } else{
                    hit = false;
                    hitTimerRun = false;
                    hitTimer = 3;
                }
            }
        }   else if(gameTimer >= 0 && pointIndex == Points.Length){ // Reach the end while there is still time
                SceneManager.LoadScene("Win");
            }
}
    
    // COLLISION (W/ SPRINKLERS)
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy" && !hide && !hit){
            wetSource.Play();
            if(lives != 0){
                lives--;
            }
            hit = true;
            hitTimerRun = true;
        }
    }

    public void playerHit(){
        if(hit){
            return;
        }
        if(lives != 0){
                lives--;
            }
            hit = true;
            hitTimerRun = true;
    }
}