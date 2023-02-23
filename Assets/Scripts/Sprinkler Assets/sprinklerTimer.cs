using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class sprinklerTimer : MonoBehaviour
{
    [SerializeField] private float gameTimer = 30;
    [SerializeField] private TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameTimer > 1){
            gameTimer -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(gameTimer % 60);
            timerText.text = string.Format("{0}", seconds);
        } else{
            SceneManager.LoadScene("Lose");
        }
    }

    public float getTimer(){
        return gameTimer;
    }
}
