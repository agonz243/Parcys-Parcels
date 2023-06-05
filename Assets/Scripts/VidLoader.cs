using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
 
public class VidLoader : MonoBehaviour
{
    private Scene currentScene;
    VideoPlayer video;

    public sceneChanger sceneChanger;
    private VideoClip clip;
    public VideoClip clipToParcy;
    public VideoClip clipToPlayer;
 
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        video = GetComponent<VideoPlayer>();
        clip = video.clip;
        if (currentScene.name == "LoseDogGame" || currentScene.name == "WinDogGame" || currentScene.name == "Win" || currentScene.name == "Lose" || currentScene.name == "LosePuzzleGame" || currentScene.name == "WinPuzzleGame") {
            if(MinigameRandomizer.minigameRandomizer.getMinigameCount() == 1) {
                clip = clipToParcy;
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 2) {
                clip = clipToPlayer;
            } else if (MinigameRandomizer.minigameRandomizer.getMinigameCount() == 3) {
                clip = clipToParcy;
            } else {
                Debug.Log("exception minigame count");
            }  
        } else {
            clipToParcy = clip;
            clipToPlayer = clip;
        }
        video.clip = clip;
    }

    public void Next() {
        video.Play();
        video.loopPointReached += ChangeScene;
    }

    public void PlayVid() {
        video.Play();
    }
 
    void ChangeScene(UnityEngine.Video.VideoPlayer vp)
    {
        sceneChanger.LoadNextScene();
    }
}