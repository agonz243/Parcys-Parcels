using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{   
    // private AudioSource _audioSource;

    // private GameObject[] other;
    // private bool NotFirst = false;
    
    // private void Awake()
    // {
    //     other = GameObject.FindGameObjectsWithTag("BGMusic");
    //     foreach (GameObject oneOther in other) {
    //         if (oneOther.scene.buildIndex == -1){
    //             NotFirst = true;
    //         }
    //     }

    //     if (NotFirst == true){
    //         Destroy(gameObject);
    //     }
    //     DontDestroyOnLoad(transform.gameObject);
    //     _audioSource = GetComponent<AudioSource>();
    // }

    // public void PlayMusic()
    // {
    //     if (_audioSource.isPlaying) return;
    //     _audioSource.Play();
    // }

    // public void StopMusic()
    // {
    //     _audioSource.Stop();
    // }

    public static Music instance;
 
    void Awake()
    {
        if (instance != null){ //!=
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
