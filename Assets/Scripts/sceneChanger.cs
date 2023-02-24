using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
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
            SceneManager.LoadScene("DogInstructions");
        } else if (currentScene.name == "DogInstructions") {
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene("DogChase");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
            // Play music during instructions
            SceneManager.LoadScene("SprinklerInstructions");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "SprinklerInstructions") {
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene("Sprinkler-2");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
            // Play music during instructions
            SceneManager.LoadScene("MailbagInstructions");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "MailbagInstructions") {
            // Pause music during Mailbag Minigame
            SceneManager.LoadScene("Mailbag");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            // Play music in title screen
            SceneManager.LoadScene("TitleScreen");
            Music.instance.GetComponent<AudioSource>().Play();
        } else { 
            Debug.Log("Loading nothing :(");
        }
        
    }
}