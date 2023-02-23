using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class sprinklerGame : MonoBehaviour
{
    // Array to hold traversal points
    [SerializeField] Transform[] Points;

    // Getting timer from sprinklerTimer.cs
    sprinklerTimer SprinklerTimer;
    [SerializeField] private GameObject sTimer;
    
    // General game / scene timers
    private float hideTimer = 1F;
    private float hitTimer = 2F;

    private float hideTimerSet = 1F;
    private float hitTimerSet = 2F;

    // Flags to start / stop timers
    private bool hideTimerRun = false;
    // private bool hitTimerRun = false;

    // Text objects to display: game timer, lives, umbrella uses
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI umbrellaUseText;
    [SerializeField] private GameObject brella; // temp for umbrella?

    // Audio Sources
    public AudioSource umbrellaSource;
    public AudioSource shakeSource;

    // Player class
    public class Player{
        private float moveSpeed;
        private int umbrellaUse;
        private int pointIndex;
        private bool keyAlt;
        private bool backKeyAlt;
        private bool hide;
        private bool hit;
        private bool hitTimerRun;
        private float hitTimer;
        private int lives;

        // Player attributes
        public Player(float ms, int umb, int liv)
        {
            moveSpeed = ms;
            umbrellaUse = umb;
            lives = liv;

            pointIndex = 0;
            keyAlt = false;
            backKeyAlt = false;
            hide = false;
            hit = false;
            hitTimerRun = false;
            // hitTimer = 2.5F;
        }

        // Player methods
        public float getMoveSpeed(){
            return moveSpeed;
        }

        public int getUmbrellaUse(){
            return umbrellaUse;
        }

        public void setUmbrellaUse(int num){
            umbrellaUse = num;
        }

        public int getLives(){
            return lives;
        }

        public void decLives(){
            lives--;
        }

        public bool getHit(){
            return hit;
        }

        public void setHit(bool state){
            hit = state;
        }

        public bool getHitTimerRun(){
            return hitTimerRun;
        }

        public void setHitTimerRun(bool state){
            hitTimerRun = state;
        }

        public bool getHide(){
            return hide;
        }

        public void setHide(bool state){
            hide = state;
        }

        public int getPointIndex(){
            return pointIndex;
        }

        public bool getKeyAlt(){
            return keyAlt;
        }
        
        public void setKeyAlt(bool state){
            keyAlt = state;
        }

        public bool getBackKeyAlt(){
            return backKeyAlt;
        }

        public void setBackKeyAlt(bool state){
            backKeyAlt = state;
        }

        public void setPointIndex(int pv){
            pointIndex = pv;
        }

        
    }
    public Player myPlayer;
    void Awake()
    {
        SprinklerTimer = sTimer.GetComponent<sprinklerTimer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // moveSpeed, umbrella uses, lives
        // myPlayer = new Player(0.225F, 5, 3);  
        myPlayer = new Player(0.35F, 5, 100);  
        transform.position = Points[myPlayer.getPointIndex()].transform.position; // starting the game goes to the first point
        brella.SetActive(false); // initially disable the umbrella
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayer.getPointIndex() != Points.Length){
            movement();
            hide();
        }
        
        textDisplay();

        if(myPlayer.getHit() == true){
            stun();
        }
        
        if(SprinklerTimer.getTimer() > 1 && myPlayer.getPointIndex() == Points.Length){
            SceneManager.LoadScene("Win");
        } else if(myPlayer.getLives() == 0){
            SceneManager.LoadScene("Lose");
        }
    }

    void movement(){
        // Debug.Log("here");
        Debug.Log("player index: " + myPlayer.getPointIndex());
        // Debug.Log("next point: " + Points[myPlayer.getPointIndex()]);
        Debug.Log("Player Position: " + transform.position + " , Point Position: " + Points[myPlayer.getPointIndex()].transform.position);
        if(transform.position == Points[myPlayer.getPointIndex()].transform.position){
            // Debug.Log("player index: " + myPlayer.getPointIndex());
            // Debug.Log("Increase Point Index");
            myPlayer.setPointIndex(myPlayer.getPointIndex() + 1);
        } else if(transform.position == Points[myPlayer.getPointIndex() - 1].transform.position ){ //(myPlayer.getPointIndex() != 1 || myPlayer.getPointIndex() != 0)
            // Debug.Log("player index: " + myPlayer.getPointIndex());
            myPlayer.setPointIndex(myPlayer.getPointIndex() - 1);
        }

        // If player isn't currently hiding or hit
        if(myPlayer.getHide() == false && myPlayer.getHit() == false){
           
           // Forward Movement "A" <-- --> "D"
           if(Input.GetKeyDown(KeyCode.A) && myPlayer.getKeyAlt() == false){
                // Debug.Log("keyCode.A");
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(true);
            } else if(Input.GetKeyDown(KeyCode.D) && myPlayer.getKeyAlt() == true){
                // Debug.Log("keyCode.D");
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(false);
            }  
        
            // Backwards Movement "Z" <-- --> "C"
            if(Input.GetKeyDown(KeyCode.Z) && myPlayer.getBackKeyAlt() == false){
                // Debug.Log("keyCode.Z");
                // Debug.Log("v2 pointIndex: " + myPlayer.getPointIndex());
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex() - 1].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(true);
            } else if(Input.GetKeyDown(KeyCode.C) && myPlayer.getBackKeyAlt() == true){ 
                // Debug.Log("keyCode.C");
                // Debug.Log("v2 pointIndex: " + myPlayer.getPointIndex());
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex() - 1].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(false);
            }
        }
        
    }

    void hide(){
        // Hide Input
        if(Input.GetKeyDown(KeyCode.W) && myPlayer.getHide() == false && myPlayer.getHit() == false && myPlayer.getUmbrellaUse() != 0){
            myPlayer.setHide(true);
            hideTimerRun = true;
            myPlayer.setUmbrellaUse(myPlayer.getUmbrellaUse() - 1);
            brella.SetActive(true);
            // Debug.Log("keyCode.W");
            if (!umbrellaSource.isPlaying){
                umbrellaSource.Play();

            }
        }

        // Hide Funcionality
        if(myPlayer.getHide() == true && hideTimerRun == true){
           if(hideTimer > 0){ // there is still time to hide
                hideTimer -= Time.deltaTime; // subtract from the time
                brella.transform.position = new Vector3(transform.position[0], transform.position[1], 0); // set umbrella position to player position
            } else{ // otherwise the timer is out
                myPlayer.setHide(false);
                hideTimerRun = false; // stop the timer
                hideTimer = hideTimerSet; // reset the timer
                brella.SetActive(false); // disable the umbrella
            } 
        }
    }

    public void stun(){
        if(myPlayer.getHit() == true && myPlayer.getHitTimerRun() == true){
            if(hitTimer > 0){
                hitTimer -= Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
            } else{
                myPlayer.setHit(false);
                myPlayer.setHitTimerRun(false);
                hitTimer = hitTimerSet;
            }
        }
    }

    void textDisplay(){
        // Connecting variables to text on screen
        livesText.text = string.Format("{0}", myPlayer.getLives());
        umbrellaUseText.text = string.Format("{0}", myPlayer.getUmbrellaUse());
    }

    public void playerHit(){
        if (!shakeSource.isPlaying){
            shakeSource.Play();
        }
        if(myPlayer.getHide() == true){
            return;
        }
        if(myPlayer.getHit() == true){
            return;
        }
        if(myPlayer.getLives() != 0){
            myPlayer.decLives();
        }
        myPlayer.setHit(true);
        myPlayer.setHitTimerRun(true);
    }
}
