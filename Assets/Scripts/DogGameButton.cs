using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogGameButton : MonoBehaviour
{
    public void LoadDogGame() {
        SceneManager.LoadScene("Dog Chase Demo");
        // Scene dogGameScene = SceneManager.GetSceneByName("Dog Chase Demo");
        // SceneManager.SetActiveScene(dogGameScene);
        SceneManager.UnloadSceneAsync("Title Screen");
    }

    public void LoadSprinklerGame(){
        SceneManager.LoadScene("SprinklerV2");
        // Scene dogGameScene = SceneManager.GetSceneByName("Dog Chase Demo");
        // SceneManager.SetActiveScene(dogGameScene);
        SceneManager.UnloadSceneAsync("Title Screen");
   }

   public void LoadMailbagGame(){
        SceneManager.LoadScene("Mailbag");
        // Scene dogGameScene = SceneManager.GetSceneByName("Dog Chase Demo");
        // SceneManager.SetActiveScene(dogGameScene);
        SceneManager.UnloadSceneAsync("Title Screen");
   }
}
