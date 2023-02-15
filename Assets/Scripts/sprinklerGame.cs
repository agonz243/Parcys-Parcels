// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class sprinklerGame : MonoBehaviour
// {
//     // Array to hold traversal points
//     [SerializeField] Transform[] Points;
    
//     // General game / scene timers
//     private float gameTimer = 45;
//     private float hideTimer = 1;
//     private float hitTimer = 2.5F;

//     // Flags to start / stop timers
//     private bool hideTimerRun;
//     private bool hitTimerRun;

//     // Text objects to display: game timer, lives, umbrella uses
//     // [SerializeField] private Text gameTimerText;
//     // [SerializeField] private Text livesText;
//     // [SerializeField] private Text umbrellaUseText;

//     public class Player{
//         private float moveSpeed;
//         private float umbrellaUse;
//         private int pointIndex;
//         private bool keyAlt;
//         private bool backKeyAlt;
//         private bool hide;
//         private bool hit;
//         private int lives;



//         void playerHit()
//         {
//             hit = true;
//             lives --;
//         }

//         void movement()
//         {
//             if(!hide && !hit){
//                 // Moving forward
//                 if(Input.GetKeyDown(KeyCode.A) && keyAlt == false){
//                     transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed);
//                     keyAlt = true;
//                 } else if(Input.GetKeyDown(keyCode.D) && keyAlt == true){
//                     transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed);
//                     keyAlt = false;
//                 }

//                 // Moving backwards
//                 else if(Input.GetKeyDown(keyCode.Z) && keyAlt == false)
//                 {
//                     transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex - 1].transform.position, moveSpeed);
//                     backKeyAlt = true;
//                 } else if(Input.GetKeyDown(keyCode.C) && keyAlt){
//                     transform.position = Vector2.MoveTowards(transform.position, Points[pointIndex - 1].transform.position, moveSpeed);
//                     backKeyAlt = false;
//                 }
//             }
//         }

//     }


//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
