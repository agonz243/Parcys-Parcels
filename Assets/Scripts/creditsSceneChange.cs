using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsSceneChange : MonoBehaviour
{

    // Initialize variables for scene
	Scene currentScene;
	string sceneName;

    // Initialize variables to have a delay when a button is pressed -- so click sound can play
    public float delay = 0.0f;
    public float timer;
    public bool startTimer = false;
    
    // Set current scene variable
    private void Start() {
    	currentScene = SceneManager.GetActiveScene();
    }

    // Wait for timer before loading next scene
    void Update(){
        if (startTimer == true){
            timer += Time.deltaTime;
            if (timer > 1f){
                LoadNextScene();
                timer = 0;
            }
        }
    }

    // Function to start timer
    public void Wait(){
        startTimer = true;
    }

    // Load scenes
    public void LoadNextScene() {
        if (currentScene.name == "TitleScreen") {
            SceneManager.LoadScene("CreditScreen");
        } else { 
            Debug.Log("Loading nothing :(");
        }
        
    }
}