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
            SceneManager.LoadScene("DogChaseDemo");
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
    		SceneManager.LoadScene("SprinklerV2");
    	} else if (currentScene.name == "Win" || currentScene.name == "Lose") {
            SceneManager.LoadScene("Mailbag");
        } else { 
            Debug.Log("Loading nothing :(");
        }
    }
}