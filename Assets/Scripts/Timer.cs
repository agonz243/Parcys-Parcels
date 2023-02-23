using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    //initializing timer variables for updating
    public float timeLeft;
    public Text timerTxt;

    // Update is called once per frame
    void FixedUpdate()
    {

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Game Over");
            timeLeft = 0;
            SceneManager.LoadScene("LosePuzzleGame");
        }

        updateTimer(timeLeft);
    }

    void updateTimer(float currentTime)
    {
        if(currentTime < 0)
        {
            currentTime = 0;
        }

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
