using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LetterInputScene : MonoBehaviour
{
    public static LetterInputScene letterInputScene;
    public TMP_InputField input;
    public string letterSaved;
    
    private void Awake() {
        if (letterInputScene == null) {
            letterInputScene = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void saveLetter() {
        letterSaved = input.text;
        SceneManager.LoadScene("LetterOutput");
    }
}
