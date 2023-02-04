using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogGameButton : MonoBehaviour
{
    public void LoadGame() {
        SceneManager.LoadScene(0);
        //Scene dogGameScene = SceneManager.GetSceneByName("Dog Chase Demo");
        //SceneManager.SetActiveScene(dogGameScene);
        SceneManager.UnloadSceneAsync("Title Screen");
    }
}
