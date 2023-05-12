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
        // currentScene = SceneManager.GetActiveScene();
        // if (currentScene.name == "TitleScreen") {
        //     scoreTracker.reset();
        //     canPause = true;
        //     SceneManager.LoadScene("Beginning_1");
        // } else if (currentScene.name == "Beginning_1") {
        //     // Pause music during Dog Chase Minigame
        //     SceneManager.LoadScene("Beginning_2");
        //     // Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "Beginning_2") {
        //     // picks random minigame and removes it from array
        //     Debug.Log(MinigameRandomizer.minigameRandomizer.randomizeMinigame());
        //     // Pause music during Dog Chase Minigame
        //     SceneManager.LoadScene("DogInstructions");
        //     // Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "DogInstructions") {
        //     // Pause music during Dog Chase Minigame
        //     canPause = false;
        //     SceneManager.LoadScene("DogChase");
        //     Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
        //     // Play music during instructions
        //     canPause = true;
        //     SceneManager.LoadScene("Middle_1");
        //     // Music.instance.GetComponent<AudioSource>().Play();
        // } else if (currentScene.name == "Middle_1") {
        //     // Pause music during Sprinkler Minigame
        //     SceneManager.LoadScene("Middle_2");
        // } else if (currentScene.name == "Middle_2") {
        //     Debug.Log(MinigameRandomizer.minigameRandomizer.randomizeMinigame());
        //     // Pause music during Sprinkler Minigame
        //     SceneManager.LoadScene("SprinklerInstructions");
        //     Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "SprinklerInstructions") {
        //     // Pause music during Sprinkler Minigame
        //     canPause = false;
        //     SceneManager.LoadScene("Sprinkler-2");
        //     Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
        //     canPause = true;
        //     // Play music during instructions
        //     SceneManager.LoadScene("End1_1");
        //     Music.instance.GetComponent<AudioSource>().Play();
        // } else if (currentScene.name == "End1_1") {
        //     SceneManager.LoadScene("End1_2");
        // } else if (currentScene.name == "End1_2") {
        //     Debug.Log(MinigameRandomizer.minigameRandomizer.randomizeMinigame());
        //     SceneManager.LoadScene("MailbagInstructions");
        // } else if (currentScene.name == "MailbagInstructions") {
        //     canPause = false;
        //     // Pause music during Mailbag Minigame
        //     SceneManager.LoadScene("Mailbag");
        //     Music.instance.GetComponent<AudioSource>().Pause();
        // } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
        //     // Play music in title screen
        //     SceneManager.LoadScene("ScoreScreen");
        //     Music.instance.GetComponent<AudioSource>().Play();
        // } else if (currentScene.name == "ScoreScreen") {
        //     SceneManager.LoadScene("End2_1");
        // } else if (currentScene.name == "End2_1") {
        //     SceneManager.LoadScene("End2_2");
        // } else if (currentScene.name == "End2_2") {
        //     SceneManager.LoadScene("TitleScreen");
        // } else if (currentScene.name == "CreditScreen") {
        //     SceneManager.LoadScene("TitleScreen");
        // } else { 
        //     Debug.Log("Loading nothing :(");
        // }

        if (MinigameRandomizer.minigameRandomizer) {
            MinigameRandomizer.setInitiated(true);
        }
            
        if (MinigameRandomizer.getInitiated()){
            randomizedFlow();
        } else {
            normalFlow();
        }
        
    }

    public void randomizedFlow() {
        Debug.Log("reach");
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScreen") {
            scoreTracker.reset();
            canPause = true;
            SceneManager.LoadScene("Beginning_1");
        } else if (currentScene.name == "Beginning_1") {
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene("Beginning_2");
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "Beginning_2") {
            // picks random minigame and removes it from array
            MinigameRandomizer.minigameRandomizer.randomizeMinigame();
            Debug.Log(MinigameRandomizer.minigameRandomizer.getCurrentMinigame());
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "DogInstructions") {
            // Pause music during Dog Chase Minigame
            canPause = false;
            SceneManager.LoadScene("DogChase");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame" || currentScene.name == "Win" || currentScene.name == "Lose" || currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            if(MinigameRandomizer.minigameRandomizer.getMinigameCount() == 1) {
                // Play music during instructions
                canPause = true;
                SceneManager.LoadScene("Middle_1");
                // Music.instance.GetComponent<AudioSource>().Play();
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 2) {
                // Play music during instructions
                SceneManager.LoadScene("End1_1");
                Music.instance.GetComponent<AudioSource>().Play();
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 3) {
                // Play music in title screen
                SceneManager.LoadScene("ScoreScreen");
                Music.instance.GetComponent<AudioSource>().Play();
            } else {
                Debug.Log("exception minigame count");
            }     
        } else if (currentScene.name == "Middle_1") {
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene("Middle_2");
        } else if (currentScene.name == "Middle_2") {
            Debug.Log(MinigameRandomizer.minigameRandomizer.randomizeMinigame());
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "SprinklerInstructions") {
            // Pause music during Sprinkler Minigame
            canPause = false;
            SceneManager.LoadScene("Sprinkler-2");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "End1_1") {
            SceneManager.LoadScene("End1_2");
        } else if (currentScene.name == "End1_2") {
            Debug.Log(MinigameRandomizer.minigameRandomizer.randomizeMinigame());
            SceneManager.LoadScene(MinigameRandomizer.minigameRandomizer.getCurrentMinigame() + "Instructions");
        } else if (currentScene.name == "MailbagInstructions") {
            canPause = false;
            // Pause music during Mailbag Minigame
            SceneManager.LoadScene("Mailbag");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "ScoreScreen") {
            SceneManager.LoadScene("End2_1");
        } else if (currentScene.name == "End2_1") {
            SceneManager.LoadScene("End2_2");
        } else if (currentScene.name == "End2_2") {
            SceneManager.LoadScene("TitleScreen");
        } else if (currentScene.name == "CreditScreen") {
            SceneManager.LoadScene("TitleScreen");
        } else { 
            Debug.Log("Loading nothing :(");
        }
    }

    public void normalFlow() {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScreen") {
            scoreTracker.reset();
            canPause = true;
            SceneManager.LoadScene("Beginning_1");
        } else if (currentScene.name == "Beginning_1") {
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene("Beginning_2");
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "Beginning_2") {
            // Pause music during Dog Chase Minigame
            SceneManager.LoadScene("DogInstructions");
            // Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "DogInstructions") {
            // Pause music during Dog Chase Minigame
            canPause = false;
            SceneManager.LoadScene("DogChase");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
            // Play music during instructions
            canPause = true;
            SceneManager.LoadScene("Middle_1");
            // Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "Middle_1") {
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene("Middle_2");
        } else if (currentScene.name == "Middle_2") {
            // Pause music during Sprinkler Minigame
            SceneManager.LoadScene("SprinklerInstructions");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "SprinklerInstructions") {
            // Pause music during Sprinkler Minigame
            canPause = false;
            SceneManager.LoadScene("Sprinkler-2");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
            canPause = true;
            // Play music during instructions
            SceneManager.LoadScene("End1_1");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "End1_1") {
            SceneManager.LoadScene("End1_2");
        } else if (currentScene.name == "End1_2") {
            SceneManager.LoadScene("MailbagInstructions");
        } else if (currentScene.name == "MailbagInstructions") {
            canPause = false;
            // Pause music during Mailbag Minigame
            SceneManager.LoadScene("Mailbag");
            Music.instance.GetComponent<AudioSource>().Pause();
        } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            // Play music in title screen
            SceneManager.LoadScene("ScoreScreen");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "ScoreScreen") {
            SceneManager.LoadScene("End2_1");
        } else if (currentScene.name == "End2_1") {
            SceneManager.LoadScene("End2_2");
        } else if (currentScene.name == "End2_2") {
            SceneManager.LoadScene("TitleScreen");
        } else if (currentScene.name == "CreditScreen") {
            SceneManager.LoadScene("TitleScreen");
        } else { 
            Debug.Log("Loading nothing :(");
        }
    }
}