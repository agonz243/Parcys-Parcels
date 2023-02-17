using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{

    Scene currentScene;
	string sceneName;

    private void Start() {
    	currentScene = SceneManager.GetActiveScene();
    }

    void Awake()
    {
        // Find audiosource with this tag: "BGMusic"
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGMusic");

        // We destroy the GameObject if there's more than one music object
        if (musicObj.Length > 1){
            Destroy(this.gameObject);
        } else if (currentScene.name == "DogChaseDemo") {
            Debug.Log("please");
            Destroy(this.gameObject);
            Destroy(GameObject.Find("BackgroundMusic"));
        } else {
            DontDestroyOnLoad(this.gameObject);
        }
        // else if (currentScene.name == "SprinklerInstructions") {
        //     SceneManager.LoadScene("SprinklerV2");
        // } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
        //     SceneManager.LoadScene("MailbagInstructions");
        // } else if (currentScene.name == "MailbagInstructions") {
        //     SceneManager.LoadScene("Mailbag");
        // } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
        //     SceneManager.LoadScene("TitleScreen");
        // } else { 
        //     Debug.Log("Loading nothing :(");
        // }

        // Otherwise, we don't destroy it
        
    }
}
