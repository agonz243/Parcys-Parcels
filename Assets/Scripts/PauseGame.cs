using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseCanvas;

    void Awake()
    {
        // Retain pause screen canvas and pause controller
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseCanvas);
    }

    void Update() 
    {
        // MUST ADD: Add setting time scale to 0 to actually pause game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);  
        }
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
