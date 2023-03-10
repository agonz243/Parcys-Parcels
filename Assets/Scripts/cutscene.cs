using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
 
public class cutscene : MonoBehaviour
{
 
     VideoPlayer video;
 
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += ChangeScene;
    }
 
 
     void ChangeScene(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("SprinklerInstructions");
    }
}