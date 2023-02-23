using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

	Scene currentScene;
	string sceneName;

    public float delay = 0.0f;
    public float timer;
    public bool startTimer = false;

    // public AudioClip clip;
    // public AudioSource source;
    
    private void Start() {
    	currentScene = SceneManager.GetActiveScene();
        // GameObject.FindGameObjectWithTag("BGMusic").GetComponent<Music>().PlayMusic();
    }

    void Update(){
        if (startTimer == true){
            timer += Time.deltaTime;
            if (timer > 1f){
                LoadNextScene();
                timer = 0;
            }
        }
        
    }

    // void Awake(){
        
    // }
    //     // Find audiosource with this tag: "BGMusic"
    //     GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGMusic");
    //     DontDestroyOnLoad(transform.gameObject);

    //     source = GetComponent<AudioSource>();

    //     // We destroy the GameObject if there's more than one music object
    //     // if (musicObj.Length > 1){
    //     //     Destroy(this.gameObject);
    //     // } else if (currentScene.name == "DogChaseDemo") {
    //     //     Debug.Log("please");
    //     //     Destroy(this.gameObject);
    //     //     Destroy(GameObject.Find("BackgroundMusic"));
    //     // } else {
    //     //     DontDestroyOnLoad(this.gameObject);
    //     // }
    //     // Debug.Log(currentScene.name);
    //     // else if (currentScene.name == "SprinklerInstructions") {
    //     //     SceneManager.LoadScene("SprinklerV2");
    //     // } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
    //     //     SceneManager.LoadScene("MailbagInstructions");
    //     // } else if (currentScene.name == "MailbagInstructions") {
    //     //     SceneManager.LoadScene("Mailbag");
    //     // } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
    //     //     SceneManager.LoadScene("TitleScreen");
    //     // } else { 
    //     //     Debug.Log("Loading nothing :(");
    //     // }

    //     // Otherwise, we don't destroy it
        
    // }

    // public void PlayMusic(){
    //     if (source.isPlaying) return;
    //     source.Play();
    // }

    // public void StopMusic(){
    //     source.Stop();
    // }

    public void Wait(){
        startTimer = true;
    }

    public void LoadNextScene() {

        if (currentScene.name == "TitleScreen") {
            SceneManager.LoadScene("DogInstructions");
            // Music.instance.GetComponent<AudioSource>().Pause();
            // GameObject.FindGameObjectWithTag("BGMusic").GetComponent<Music>().StopMusic();
            // GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGMusic");
            // Destroy(this.gameObject);
        } else if (currentScene.name == "DogInstructions") {
            Music.instance.GetComponent<AudioSource>().Pause();
            SceneManager.LoadScene("DogChase");
        } else if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame") {
            SceneManager.LoadScene("SprinklerInstructions");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "SprinklerInstructions") {
            SceneManager.LoadScene("Sprinkler-1");
        } else if (currentScene.name == "Win" || currentScene.name == "Lose") {
            SceneManager.LoadScene("MailbagInstructions");
            Music.instance.GetComponent<AudioSource>().Play();
        } else if (currentScene.name == "MailbagInstructions") {
            SceneManager.LoadScene("Mailbag");
        } else if (currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            SceneManager.LoadScene("TitleScreen");
        } else { 
            Debug.Log("Loading nothing :(");
        }
        
    }
}