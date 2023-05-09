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

    // Function to be passed to sceneLoaded delegate
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
            pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);  
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
