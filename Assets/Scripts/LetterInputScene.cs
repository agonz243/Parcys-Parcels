using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LetterInputScene : MonoBehaviour
{
    public static LetterInputScene letterInputScene;
    public TMP_InputField input;
    public TextMeshProUGUI intro;
    public string letterSaved;
    public VideoPlayer vidPlayer;
    public PageSwitch pageTurn;

    public sceneChanger sceneChanger;

    public AudioSource scribble;
    public bool scribblePlayed;
    public bool typing;
    public float pauseDelay;
    
    private void Awake() {
        // if (letterInputScene == null) {
        letterInputScene = this;
        //     DontDestroyOnLoad(gameObject);
        // } else {
        //     Destroy(gameObject);
        // }
    }

    void Start()
    {
        //Fetch the Input Field component from the GameObject
        // input = GetComponent<TMP_InputField>();

        scribblePlayed = false;
        typing = false;
    }

    void Update()
    {
        if (typing){
            pauseDelay -= Time.deltaTime;
        }
        
        //Check if the Input Field is in focus and able to alter
        if (input.isFocused && Input.anyKeyDown) {
            if (!scribble.isPlaying & !scribblePlayed){
                scribble.Play();
                scribblePlayed = true;
                typing = true;
                Debug.Log("scribble");
            } else if (!scribble.isPlaying){
                scribble.Play();
                typing = true;
                Debug.Log("unpause scribble");
                
            }
        } else {
            if(pauseDelay <= 0) {
                pauseDelay = 1;
                typing = false;
                scribble.Pause();
                // Debug.Log("nothing");
            }
    
        }

        
    }

    public void saveLetter() {
        letterSaved = intro.text + input.text;
        // SceneManager.LoadScene("LetterOutput");
        // sceneChanger.LoadNextScene();
        pageTurn.NextPage();
        vidPlayer.GetComponent<VidLoader>().Next();
        // SceneManager.LoadScene("DogInstructions");
    }
}
