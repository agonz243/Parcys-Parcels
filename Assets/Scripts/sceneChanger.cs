using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

	Scene currentScene;
	string sceneName;
    
    private void Start() {
    	currentScene = SceneManager.GetActiveScene();
    }

    public void LoadNextScene() {

        Debug.Log(currentScene.name);
        if (currentScene.name == "TitleScreen") {
            SceneManager.LoadScene("DogInstructions");
        } else if (currentScene.name == "DogInstructions") {
            SceneManager.LoadScene("DogChaseDemo");
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
    		SceneManager.LoadScene("SprinklerInstructions");
    	} else if (currentScene.name == "SprinklerInstructions") {
            SceneManager.LoadScene("SprinklerV2");
        } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
            SceneManager.LoadScene("MailbagInstructions");
        } else if (currentScene.name == "MailbagInstructions") {
            SceneManager.LoadScene("Mailbag");
        } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            SceneManager.LoadScene("TitleScreen");
        } else { 
            Debug.Log("Loading nothing :(");
        }
    }
}