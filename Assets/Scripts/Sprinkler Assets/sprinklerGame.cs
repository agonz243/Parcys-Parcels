using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class sprinklerGame : MonoBehaviour
{
    // Array to hold traversal points
    [SerializeField] Transform[] Points;

    // Function to change sprite?
    [SerializeField] sprinklerSpriteChange spriteChange;

    // Animator Stuff for Umbrella
    [SerializeField] Animator animator; 

    // Stuff for Particle System
    [SerializeField] private ParticleSystem mailParticles;
    private bool dropMail = false;

    // Stuff for tracking if player is idle and should be
    // reminded of controls
    // How long, in seconds, player must idle before being reminded
    [SerializeField]private static float reminderInterval = 5f;
    private float reminderTimer = reminderInterval;
 

    // Getting timer from sprinklerTimer.cs
    sprinklerTimer SprinklerTimer;
    [SerializeField] private GameObject sTimer;

    // Stuff for Stun
    private Vector2 velocity = new Vector2(0,0);
    private float smoothTime = 2F;
    
    // General game / scene timers
    private float hideTimer = 2F;
    private float hitTimer = 2F;

    private float hideTimerSet = 2F;
    private float hitTimerSet = 2F;

    private float iFrameTimer = 1F;
    private float iFrameTimerSet = 1F;

    // Flags to start / stop timers
    // private bool hideTimerRun = false;

    // Text objects to display: game timer, lives, umbrella uses
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI umbrellaUseText;
    [SerializeField] private GameObject brella; // temp for umbrella?

    // Audio Sources
    public AudioSource umbrellaSource;
    public AudioSource shakeSource;
    private bool play = false;

    // Player class
    public class Player{
        private float moveSpeed;
        private int umbrellaUse;
        private int prevPointIndex;
        private int pointIndex;
        private bool keyAlt;
        private bool backKeyAlt;
        private bool hide;
        private bool hit;
        private bool hitTimerRun;
        private bool iFrameTimerRun;
        private int lives;
        private bool moving;

        // Player attributes
        public Player(float ms, int umb, int liv)
        {
            moveSpeed = ms;
            umbrellaUse = umb;
            lives = liv;

            pointIndex = 0;
            prevPointIndex = 1;

            keyAlt = false;
            backKeyAlt = false;
            hide = false;
            hit = false;
            hitTimerRun = false;
            iFrameTimerRun = false;
            moving = false;
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

        public bool getIFrameTimerRun() {
            return iFrameTimerRun;
        }

        public void setIFrameTimerRun(bool state) {
            iFrameTimerRun = state;
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

        public int getPrevPointIndex(){
            return prevPointIndex;
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

        public void setPrevPointIndex(int pv){
            prevPointIndex = pv;
        }
        public bool getMoving(){
            return moving;
        }
        public void setMoving(bool state){
            moving = state;
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
        //    (moveSpeed, umbrella uses, lives)
        // myPlayer = new Player(0.225F, 5, 3);  

        // Create new Player from class above
        myPlayer = new Player(0.3F, 5, 100);  
        transform.position = Points[myPlayer.getPointIndex()].transform.position; // starting the game goes to the first point
        brella.SetActive(false); // initially disable the umbrella
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayer.getPointIndex() != Points.Length){
            reminderTimer -= Time.deltaTime;
            movement();
            hide();
        }

        if (reminderTimer <= 0) {
            Debug.Log("REMIND REMIND REMIND");
        }
        
        textDisplay();

        if(myPlayer.getHit() == true && myPlayer.getIFrameTimerRun() == false){
            stun();
        }

        if(iFrameTimer > 0 && myPlayer.getIFrameTimerRun() == true){
            iFrameTimer -= Time.deltaTime;
        } else {
            myPlayer.setIFrameTimerRun(false);
            iFrameTimer = iFrameTimerSet;
        }
        
        if(SprinklerTimer.getTimer() > 1 && myPlayer.getPointIndex() == Points.Length){
            scoreTracker.sprinklerWin = true;
            SceneManager.LoadScene("Win");
        } else if(myPlayer.getLives() == 0){
            SceneManager.LoadScene("Lose");
        }
    }

    void movement(){

        // Player's location is the same as the next point
        if(transform.position == Points[myPlayer.getPointIndex()].transform.position && myPlayer.getMoving()){
            myPlayer.setPrevPointIndex(myPlayer.getPointIndex());
            myPlayer.setPointIndex(myPlayer.getPointIndex() + 1);
            if(myPlayer.getPointIndex() < Points.Length) {
                transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
            }
            
        // Player's location is equal to prev point
        } else if(transform.position == Points[myPlayer.getPrevPointIndex()].transform.position && myPlayer.getPrevPointIndex() >= 1 && myPlayer.getMoving()){ //(myPlayer.getPointIndex() != 1 || myPlayer.getPointIndex() != 0)
            myPlayer.setPointIndex(myPlayer.getPrevPointIndex());
            myPlayer.setPrevPointIndex(myPlayer.getPrevPointIndex() - 1);
            transform.up = Points[myPlayer.getPrevPointIndex()].transform.position - transform.position;
        }

        // If player isn't currently hiding or hit
        if(myPlayer.getHide() == false && myPlayer.getHit() == false){
           // Forward Movement "A" <-- --> "D"
           if(Input.GetKeyDown(KeyCode.A) && myPlayer.getKeyAlt() == false){
                // Reset control reminder timer
                reminderTimer = reminderInterval;

                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(true);
                spriteChange.ChangeSprite("u1");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
            } else if(Input.GetKeyDown(KeyCode.D) && myPlayer.getKeyAlt() == true){
                // Reset control reminder timer
                reminderTimer = reminderInterval;

                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(false);
                spriteChange.ChangeSprite("u2");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
            }
            /*
            Backwards Movement "Z" <-- --> "C"
            else if(Input.GetKeyDown(KeyCode.Z) && myPlayer.getBackKeyAlt() == false){
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPrevPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(true);
                spriteChange.ChangeSprite("u1");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPrevPointIndex()].transform.position - transform.position;
            } else if(Input.GetKeyDown(KeyCode.C) && myPlayer.getBackKeyAlt() == true){ 
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPrevPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(false);
                spriteChange.ChangeSprite("u2");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPrevPointIndex()].transform.position - transform.position;
            } 
            */
            else {
                myPlayer.setMoving(false);
            }
            
        }
        
    }

    void hide(){
        // Hide Input (Input.GetKeyDown(KeyCode.W) --> Input.GetKey(KeyCode.W))  && hideTimerRun == false
        if(Input.GetKey(KeyCode.W) && myPlayer.getHit() == false ){ // && hideTimer >= 0
            myPlayer.setHide(true);
            brella.SetActive(true);

            // Check if noise is already playing  
            if (!umbrellaSource.isPlaying && play == false){
                umbrellaSource.Play();
                play = true;
                animator.Play("Umbrella Up", 0, 0); // play animation once?
            }
        }
        // if(hideTimer < 0.1){
        //     brella.SetActive(false); // disable the umbrella
        //     myPlayer.setHide(false);
        // }
        if(Input.GetKeyUp(KeyCode.W)){
            animator.Play("Umbrella Down", 0, 0); // play animation once?
            myPlayer.setHide(false);
            brella.SetActive(false);
            hideTimer = hideTimerSet; // reset the timer
            myPlayer.setIFrameTimerRun(true);
            iFrameTimer = iFrameTimerSet;
            play = false;
            
        }

        // Hide Funcionality
        if(myPlayer.getHide() == true ){ // && hideTimer >= 0
            brella.transform.position = new Vector3(transform.position[0], transform.position[1], -1); // set umbrella position to player position
            //hideTimer -= Time.deltaTime;
        } else{
            brella.SetActive(false); // disable the umbrella
            myPlayer.setHide(false);
        }
    }

    public void stun(){
        if(myPlayer.getHit() == true && myPlayer.getHitTimerRun() == true){
            if(dropMail == false){
                mailParticles.transform.position = new Vector3(transform.position[0], transform.position[1], -1); // set particle system position to player position
                mailParticles.Play();
                dropMail = true;
                // transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPrevPointIndex()].transform.position, myPlayer.getMoveSpeed()); // move player backwards after getting hit
                // transform.position = Vector2.SmoothDamp(transform.position, Points[myPlayer.getPrevPointIndex()].transform.position, ref velocity, smoothTime);
            }
            if(hitTimer > 0){
                hitTimer -= Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
                transform.position = Vector2.SmoothDamp(transform.position, Points[myPlayer.getPrevPointIndex()].transform.position, ref velocity, smoothTime);
            } else {
                myPlayer.setHit(false);
                myPlayer.setHitTimerRun(false);
                myPlayer.setIFrameTimerRun(true);
                iFrameTimer = iFrameTimerSet;
                hitTimer = hitTimerSet;
                dropMail = false;
            }
        }
    }

    void textDisplay(){
        // Connecting variables to text on screen
        livesText.text = string.Format("{0}", myPlayer.getLives());
        umbrellaUseText.text = string.Format("{0}", myPlayer.getUmbrellaUse());
    }

    public void playerHit(){
        if(myPlayer.getHide() == true){
            return;
        }
        if(myPlayer.getHit() == true){
            return;
        }
        if(myPlayer.getIFrameTimerRun() == true) {
            return;
        }
        if(myPlayer.getLives() != 0){
            myPlayer.decLives();
        }
        if (!shakeSource.isPlaying){
            shakeSource.Play();
        }
        myPlayer.setHit(true);
        myPlayer.setHitTimerRun(true);
    }
}
