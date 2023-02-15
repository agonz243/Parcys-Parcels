using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinklerGame : MonoBehaviour
{
    // Array to hold traversal points
    [SerializeField] Transform[] Points;
    
    // General game / scene timers
    private float gameTimer = 45;
    private float hideTimer = 1;
    private float hitTimer = 2.5F;

    // Flags to start / stop timers
    private bool hideTimerRun = false;
    private bool hitTimerRun = false;

    // Text objects to display: game timer, lives, umbrella uses
    // [SerializeField] private Text gameTimerText;
    // [SerializeField] private Text livesText;
    // [SerializeField] private Text umbrellaUseText;
    [SerializeField]private GameObject brella; // temp for umbrella?


    public class Player{
        private float moveSpeed;
        private int umbrellaUse;
        private int pointIndex;
        private bool keyAlt;
        private bool backKeyAlt;
        private bool hide;
        private bool hit;
        private int lives;

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
        }
        public float getMoveSpeed(){
            return moveSpeed;
        }

        public int getUmbrellaUse(){
            return umbrellaUse;
        }

        public void setUmbrellaUse(int num){
            umbrellaUse = num;
        }

        public bool getHit(){
            return hit;
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

        public void playerHit(){
            hit = true;
            lives --;
        }
        
    }
    public Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = new Player(0.225F, 5, 3);  
        transform.position = Points[myPlayer.getPointIndex()].transform.position; // starting the game goes to the first point
        brella.SetActive(false); // initially disable the umbrella
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        hide();
    }

    void movement(){
        Debug.Log("here");
        if(transform.position == Points[myPlayer.getPointIndex()].transform.position){
            myPlayer.setPointIndex(myPlayer.getPointIndex() + 1);
        }

        // If player isn't currently hiding or hit
        if(myPlayer.getHide() == false && myPlayer.getHit() == false){
           
           // Forward Movement
           if(Input.GetKeyDown(KeyCode.A) && myPlayer.getKeyAlt() == false){
                Debug.Log("keyCode.A");
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(true);
            } else if(Input.GetKeyDown(KeyCode.D) && myPlayer.getKeyAlt() == true){
                Debug.Log("keyCode.D");
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex()].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setKeyAlt(false);
            }  
        
            // Backwards Movement
            if(Input.GetKeyDown(KeyCode.Z) && myPlayer.getBackKeyAlt() == false){
                Debug.Log("keyCode.Z");
                Debug.Log("v2 pointIndex: " + myPlayer.getPointIndex());
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex() - 1].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(true);
            } else if(Input.GetKeyDown(KeyCode.C) && myPlayer.getBackKeyAlt() == true){ 
                Debug.Log("keyCode.C");
                Debug.Log("v2 pointIndex: " + myPlayer.getPointIndex());
                transform.position = Vector2.MoveTowards(transform.position, Points[myPlayer.getPointIndex() - 1].transform.position, myPlayer.getMoveSpeed());
                myPlayer.setBackKeyAlt(false);
            } 
        }
        
    }

    void hide(){
        if(Input.GetKeyDown(KeyCode.W) && myPlayer.getHide() == false && myPlayer.getHit() == false && myPlayer.getUmbrellaUse() != 0){
            myPlayer.setHide(true);
            hideTimerRun = true;
            myPlayer.setUmbrellaUse(myPlayer.getUmbrellaUse() - 1);
            brella.SetActive(true);
            Debug.Log("keyCode.W");
        }

        if(myPlayer.getHide() == true && hideTimerRun == true){
           if(hideTimer > 0){ // there is still time to hide
                hideTimer -= Time.deltaTime; // subtract from the time
                brella.transform.position = new Vector3(transform.position[0], transform.position[1], 0); // set umbrella position to player position
            } else{ // otherwise the timer is out
                myPlayer.setHide(false);
                hideTimerRun = false; // stop the timer
                hideTimer = 1; // reset the timer
                brella.SetActive(false); // disable the umbrella
            } 
        }
    }
}
