using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

    // Initialize variables for scene
	private Scene currentScene;
	string sceneName;

    // Initialize variables to have a delay when a button is pressed -- so click sound can play
    public float delay = 0.0f;
    public float timer;
    public bool startTimer = false;
    public static bool canPause = false;

    
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
        if (MinigameRandomizer.minigameRandomizer) {
            MinigameRandomizer.setInitiated(true);
        }
            
        if (MinigameRandomizer.getInitiated()){
            randomizedFlow();
        } else {
            if (currentScene.name == "CreditScreen") {
                SceneManager.LoadScene("TitleScreen");
                // Music.instance.GetComponent<AudioSource>().Pause();
            }
            Debug.Log("scene changer: minigame randomizer not initialized, start from title");
        }
        
    }

    public void randomizedFlow() {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScreen") {
            scoreTracker.reset();
            canPause = true;
            SceneManager.LoadScene("Beginning");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "Beginning") {
            // picks random minigame and removes it from array
            MinigameRandomizer.minigameRandomizer.randomizeMinigame();
            scoreTracker.firstMinigame = MinigameRandomizer.minigameRandomizer.getCurrentMinigame();
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "DogInstructions") {
            // Pause music during Dog Chase Minigame
            canPause = false;
            SceneManager.LoadScene("DogChase");
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame" || currentScene.name == "Win" || currentScene.name == "Lose" || currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            if(MinigameRandomizer.minigameRandomizer.getMinigameCount() == 1) {
                // Play music during instructions
                canPause = true;
                SceneManager.LoadScene("Middle");
                Music.instance.GetComponent<AudioSource>().Play();
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 2) {
                // Play music during instructions
                canPause = true;
                SceneManager.LoadScene("End1");
                Music.instance.GetComponent<AudioSource>().Play();
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 3) {
                // Play music in title screen
                SceneManager.LoadScene("End2");
                Music.instance.GetComponent<AudioSource>().Play();
            } else {
                Debug.Log("exception minigame count");
            }     
        } else if (currentScene.name == "Middle") {
            MinigameRandomizer.minigameRandomizer.randomizeMinigame();
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "SprinklerInstructions") {
            // Choose randomly between sprinkler game layouts
            string[] layouts = {"Sprinkler-2", "Sprinkler-3"};

            // Pause music during Sprinkler Minigame
            canPause = false;
            SceneManager.LoadScene(layouts[Random.Range(0,layouts.Length)]);
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "End1") {
            MinigameRandomizer.minigameRandomizer.randomizeMinigame();
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "MailbagInstructions") {
            // Array of possible layouts
            string[] layouts = {"Mailbag", "MailbagDupe"};
            canPause = false;
            // Pause music during Mailbag Minigame
            SceneManager.LoadScene(layouts[Random.Range(0,layouts.Length)]);
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "End2") {
            SceneManager.LoadScene("TitleScreen");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "CreditScreen") {
            SceneManager.LoadScene("TitleScreen");
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else { 
            Debug.Log("Loading nothing :(");
        }
    }
}