using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool currentMouseMode;
    private static PauseGame pauseSingleton = null;

    void Awake()
    {
        if (pauseSingleton == null) {
            pauseSingleton = this;
            // Retain pause screen canvas and pause controller
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(pauseCanvas);
        } else {
            Destroy(gameObject);
            Destroy(pauseCanvas);
        }

    }

    // On enable, subscribe to event listener that tracks scene loads
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    // On scene load, find main camera and assign it to canvas
    // and grab cursor visibility
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Get scene's main camera
        Camera sceneCam;
        sceneCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Assign main camera to pause canvas
        Canvas pCanvas;
        pCanvas = pauseCanvas.GetComponent<Canvas>();
        pCanvas.worldCamera = sceneCam;

        // Grab cursor visibility
        currentMouseMode = Cursor.visible;
    }

    void Update() 
    {
        // MUST ADD: Add setting time scale to 0 to actually pause game
        if (Input.GetKeyDown(KeyCode.Escape) && sceneChanger.canPause) {
            togglePause();  
        }
    }

    void OnDisable()
    {
        // Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Changes scene to menu and performs appropriate resets
    public void GoToMenu()
    {
        // Cleanup
        currentMouseMode = true;
        togglePause(); // Unpause game
        sceneChanger.canPause = false; // Make sure pause is still disabled on title
        Time.timeScale = 1; // Reset time scale 

        SceneManager.LoadScene("TitleScreen");
        Music.instance.GetComponent<AudioSource>().Play();
        scoreTracker.reset(); // Reset tracked scores
    }

    // Toggles the active state of the pause menu canvas
    public void togglePause()
    {
        // Make cursor visible on toggle.
        // This ensures that the mouse is always visible on the
        // pause screen
        Cursor.visible = true;
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);
    }
}
