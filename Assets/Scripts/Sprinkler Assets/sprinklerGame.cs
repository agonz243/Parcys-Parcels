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
        private int prevPointIndex;
        private int pointIndex;
        private bool keyAlt;
        private bool backKeyAlt;
        private bool hide;
        private bool hit;
        private bool hitTimerRun;
        private float hitTimer;
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
        myPlayer = new Player(0.3F, 5, 5);  
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
        // Debug.Log("player index: " + myPlayer.getPointIndex());
        // Debug.Log("next point: " + Points[myPlayer.getPointIndex()]);
        // Debug.Log("Player Position: " + transform.position + " , Point Position: " + Points[myPlayer.getPointIndex()].transform.position);
        // Debug.Log("Point Index: " + myPlayer.getPointIndex() + " , Prev Point Index: " + myPlayer.getPrevPointIndex());

        // Rotating Player Sprite
        // if(transform.localRotation.z != Vector3.Angle(transform.position,Points[myPlayer.getPointIndex()].transform.position)){
                //     // transform.localRotation.z = Vector3.Angle(transform.position,Points[myPlayer.getPointIndex()].transform.position);
                //     transform.Rotate(0, 0, Vector3.Angle(transform.position,Points[myPlayer.getPointIndex()].transform.position));
                // }
        // Debug.Log("Angle: " + Vector3.Angle((Points[myPlayer.getPointIndex()].transform.position), Points[myPlayer.getPrevPointIndex()].transform.position));
        
        // float difY = Points[myPlayer.getPrevPointIndex()].transform.position.y - Points[myPlayer.getPointIndex()].transform.position.y;
        // float difX = Points[myPlayer.getPrevPointIndex()].transform.position.x - Points[myPlayer.getPointIndex()].transform.position.x;
        // // Debug.Log("Inverse Tan: " + (Mathf.Rad2Deg*Mathf.Atan((difY/difX) - 90)));
        // float atan = (Mathf.Rad2Deg*Mathf.Atan((difY/difX))); //- 90
        // Debug.Log("Atan: " + atan);

        // Player's location is the same as the next point
        if(transform.position == Points[myPlayer.getPointIndex()].transform.position && myPlayer.getMoving()){
            myPlayer.setPrevPointIndex(myPlayer.getPointIndex());
            myPlayer.setPointIndex(myPlayer.getPointIndex() + 1);
            transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
        
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
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(true);
                spriteChange.ChangeSprite("u1");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
            } else if(Input.GetKeyDown(KeyCode.D) && myPlayer.getKeyAlt() == true){
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(false);
                spriteChange.ChangeSprite("u2");
                myPlayer.setMoving(true);
                transform.up = Points[myPlayer.getPointIndex()].transform.position - transform.position;
            }
        
            // Backwards Movement "Z" <-- --> "C"
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
            } else {
                myPlayer.setMoving(false);
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
        if(myPlayer.getHide() == true){
            return;
        }
        if(myPlayer.getHit() == true){
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
