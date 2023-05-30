using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameLetter : MonoBehaviour
{

    public TextMeshProUGUI page1;
    // Start is called before the first frame update
    void Start()
    {
        string introText = "Hi Penpal!" + Environment.NewLine + Environment.NewLine + "Thanks so much for writing back! I think I’ll give your advice a try tomorrow morning; I’m gonna need a good night’s sleep to reset after today.";

        string introParagraph = introText + Environment.NewLine + Environment.NewLine;

        page1.text = introParagraph;
        
        if(scoreTracker.firstMinigame == "Dog") {
            if (scoreTracker.dogWin) {
                page1.text += "You shoulda seen me runnin’ circles round this dog who lives on one of my routes!! His name is Stampy; we always have a good time playing chase while I try to deliver the mail. I fumbled the bag (of envelopes haha) at the beginning, but I still got all the mail in the box!";
            } else {
                page1.text += "There’s a sweet Shiba Inu named Stampy who lives on one of my routes, and we love to play chase while I try to deliver the mail. I fumbled the bag (of envelopes haha) at the beginning, and he caught me this time :( He slobbered all over the mail before I could stop him >.< He’s still a very good boy, though, and now he knows envelopes are not toys!";
            }
        } else if (scoreTracker.firstMinigame == "Sprinkler") {
            if (scoreTracker.sprinklerWin) {
                page1.text += "I took a different route than usual, and when I got to one of the houses I saw their sprinklers were still on! They had a sign showing I couldn’t step on the grass so I had to follow the stone path while dodging the water droplets!! Gonna start putting bobbin’ ‘n’ weavin’ on my resume, ‘cause those sprinklers ate my dust!";
            } else {
                page1.text += "I took a different route than usual, and when I got to one of the houses I saw their sprinklers were still on! They had a sign showing I couldn’t step on the grass so I had to follow the stone path while dodging the water droplets!! Unfortunately, I ended up drenched and had to pause to wring my hat out, so I was late to my next stop. I’ll keep an emergency towel in my truck from now on!";
            }
        } else if (scoreTracker.firstMinigame == "Mailbag") {
            if (scoreTracker.mailbagWin) {
                page1.text += "I’m not used to so many packages being ordered to one house, but I really got to flex my stacking skills 8:D I got those packages nice and orderly without squashing a single corner! ";
            } else {
                page1.text += "mailbag nay";
            }
        } else {
            page1.text += "somethings wrong - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
