using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseCanvas;

    void Awake()
    {
        // Retain pause screen canvas and pause controller
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseCanvas);
    }

    // On enable, subscribe to event listener that tracks scene loads
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // On scene load, find main camera and assign it to canvas
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        // Get scene's main camera
        Camera sceneCam;
        sceneCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Assign main camera to pause canvas
        Canvas pCanvas;
        pCanvas = pauseCanvas.GetComponent<Canvas>();
        pCanvas.worldCamera = sceneCam;
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
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Changes scene to menu and performs appropriate resets
    public void GoToMenu()
    {
        // Destroy pause canvas and game object holding script
        Destroy(gameObject);
        Destroy(pauseCanvas);

        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
        Music.instance.GetComponent<AudioSource>().Play();
        scoreTracker.reset();
    }

    // Toggles the active state of the pause menu canvas
    public void togglePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);
    }
}
