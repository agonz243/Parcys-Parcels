using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        if (scoreTracker.dogWin)
        {
            status1.text = "Success!";
            time1.text = "Delivered in " + scoreTracker.dogTime + " seconds!";
        } else {
            status1.text = "Failure...";
            Destroy(time1);
        }

        if (scoreTracker.sprinklerWin)
        {
            status2.text = "Success!";
            time2.text = "Delivered in " + scoreTracker.sprinklerTime + " seconds!";
        } else {
            status2.text = "Failure...";
            Destroy(time2);
        }

        if (scoreTracker.mailbagWin)
        {
            status3.text = "Success!";
            time3.text = "Delivered in " + scoreTracker.mailbagTime + " seconds!";
        } else {
            status3.text = "Failure...";
            Destroy(time3);
        }


    }
}
