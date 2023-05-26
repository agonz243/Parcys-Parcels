using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour
{
    // Time to win for each game
    public TextMeshProUGUI time1;
    public TextMeshProUGUI time2;
    public TextMeshProUGUI time3;

    // Win/Lose Sprites
    public Sprite dogWin;
    public Sprite dogLose;
    public Sprite sprinklerWin;
    public Sprite sprinklerLose;
    public Sprite mailbagWin;
    public Sprite mailbagLose;

    // Photos
    public SpriteRenderer dogPhoto;
    public SpriteRenderer sprinklerPhoto;
    public SpriteRenderer mailbagPhoto;

    // Start is called before the first frame update
    void Start()
    {   

        if (scoreTracker.dogWin) {
            time1.text = "Stampy had me huffin' n' puffin' but I still got all the mail in the box in " + scoreTracker.dogTime + " seconds!";
            dogPhoto.sprite = dogWin;
        } else {
            time1.text = "Stampy really got me this time, but I can always try again. Maybe I should use the hedges more to my advantage!";
            dogPhoto.sprite = dogLose;
        }

        if (scoreTracker.sprinklerWin) {
            time2.text = "Sprinklers work hard but I work harder" + Environment.NewLine + "I got the job done in " + scoreTracker.sprinklerTime + " seconds and only used my umbrella " + scoreTracker.umbrellaUse + " times! ";
            sprinklerPhoto.sprite = sprinklerWin;
        } else {
            time2.text = "I really wasn’t ready to audition for Catworld Ninja Warrior ...my hat felt wet like soggy stream-water socks!";
            sprinklerPhoto.sprite = sprinklerLose;
        }

        if (scoreTracker.mailbagWin) {
            time3.text = "Just call me Tetris God, ‘cause I stacked those packages maaad neat in " + scoreTracker.mailbagTime + " seconds!";
            mailbagPhoto.sprite = mailbagWin;
        } else {
            time3.text = "I hope there’s lots of bubble wrap in those packages… so many crumpled corners 8,'{";
            mailbagPhoto.sprite = mailbagLose;
        }
    }
}
