using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class displayScores : MonoBehaviour
{
    // Success/Failure status for each minigame
    public TextMeshProUGUI status1;
    public TextMeshProUGUI status2;
    public TextMeshProUGUI status3;

    // Time to win for each game
    public TextMeshProUGUI time1;
    public TextMeshProUGUI time2;
    public TextMeshProUGUI time3;

    // Message for each game
    public TextMeshProUGUI Message;

    // Mail Text
    public TextMeshProUGUI mailbox;
    public int numMail;
    public int mail1 = 0;
    public int mail2 = 0;
    public int mail3 = 0;


    // Buttons
    public Button button1;
    public TextMeshProUGUI button1txt;
    public Button button2;
    public TextMeshProUGUI button2txt;
    public Button button3;
    public TextMeshProUGUI button3txt;

    // Button colors
    public ColorBlock color1;
    public ColorBlock color2;
    public ColorBlock color3;

    public Sprite frogHappy;
    public Sprite frogSad;
    public Sprite bearHappy;
    public Sprite bearSad;
    public Sprite raccoonHappy;
    public Sprite raccoonSad;

    public GameObject Portrait;

    public Sprite blueBox;
    public GameObject msgBox;

    // public Color myGreen = new Color(0.94f, 0.118f, 0.97f);

    void Awake(){
        color1 = button1.colors;
        color2 = button2.colors;
        color3 = button3.colors;
        numMail = 3;
    }

    // Start is called before the first frame update
    void Start()
    {   

        button1.onClick.AddListener(Msg1);
        button2.onClick.AddListener(Msg2);
        button3.onClick.AddListener(Msg3);

        mailbox.text = "INBOX (" + numMail + ")";

        if (scoreTracker.dogWin)
        {
            status1.text = "Success!";
            time1.text = "Delivered in " + scoreTracker.dogTime + " seconds!";

            button1txt.text = "Thank You!";
        } else {
            status1.text = "Failure...";
            Destroy(time1);

            button1txt.text = "Slobber Covered Mail";
        }

        if (scoreTracker.sprinklerWin)
        {
            status2.text = "Success!";
            time2.text = "Delivered in " + scoreTracker.sprinklerTime + " seconds!";

            button2txt.text = "You're The Best!";
        } else {
            status2.text = "Failure...";
            Destroy(time2);

            button2txt.text = "Wet Mail...?";
        }

        if (scoreTracker.mailbagWin)
        {
            status3.text = "Success!";
            time3.text = "Delivered in " + scoreTracker.mailbagTime + " seconds!";

            button3txt.text = "Tetris God!";
        } else {
            status3.text = "Failure...";
            Destroy(time3);

            button3txt.text = "Damaged Packages ";
        }


    }

    void Msg1(){
        if (mail1 == 0){
            mail1 = 1;
            numMail --;
            mailbox.text = "INBOX (" + numMail + ")";
        }
        color1.highlightedColor = Color.grey;
        color1.normalColor = Color.white;
        color1.pressedColor = Color.green;
        color1.selectedColor = Color.green;
 
        button1.colors = color1;

        if (scoreTracker.dogWin)
        {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "Thank you for my mail! You delivered it mail in " + scoreTracker.dogTime + " seconds!";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = bearHappy;
        } else {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "Hey Parcy... \n Why is there slobber all over my mail...? What did you do...";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = bearSad;
        }
    }

    void Msg2(){
        if (mail2 == 0){
            mail2 = 1;
            numMail --;
            mailbox.text = "INBOX (" + numMail + ")";
        }
        color2.highlightedColor = Color.grey;
        color2.normalColor = Color.white;
        color2.pressedColor = Color.green;
        color2.selectedColor = Color.green;
 
        button2.colors = color2;

        if (scoreTracker.sprinklerWin)
        {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "Thank you for my mail! You delivered the mail in " + scoreTracker.sprinklerTime + " seconds!";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = frogHappy;

        } else {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "G'day Mate... \n My mail is all wet. I am upset.";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = frogSad;
        }
    }

    void Msg3(){
        if (mail3 == 0){
            mail3 = 1;
            numMail --;
            mailbox.text = "INBOX (" + numMail + ")";
        }
        color3.highlightedColor = Color.grey;
        color3.normalColor = Color.white;
        color3.pressedColor = Color.green;
        color3.selectedColor = Color.green;
 
        button3.colors = color3;

        if (scoreTracker.mailbagWin)
        {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "Thank you for neatly organizing my mail in " + scoreTracker.mailbagTime + " seconds!";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = raccoonHappy;
        } else {
            msgBox.gameObject.GetComponent<SpriteRenderer>().sprite = blueBox;
            Message.text = "Hey... \n I didn't get all my packages in my mailbox and the ones that I did get seemed to be crammed in the mailbox. 0/10 stars ):";
            Portrait.gameObject.GetComponent<SpriteRenderer>().sprite = raccoonSad;
        }
    }
}
