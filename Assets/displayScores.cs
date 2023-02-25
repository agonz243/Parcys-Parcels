using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayScores : MonoBehaviour
{
    public TextMeshProUGUI status1;
    public TextMeshProUGUI status2;
    public TextMeshProUGUI status3;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreTracker.dogWin)
        {
            status1.text = "Success!";
        } else {
            status1.text = "Failure...";
        }

        if (scoreTracker.sprinklerWin)
        {
            status2.text = "Success!";
        } else {
            status2.text = "Failure...";
        }

        if (scoreTracker.mailbagWin)
        {
            status3.text = "Success!";
        } else {
            status3.text = "Failure...";
        }


    }
}
