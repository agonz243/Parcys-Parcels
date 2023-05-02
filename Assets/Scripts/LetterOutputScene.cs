using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterOutputScene : MonoBehaviour
{
    public TextMeshProUGUI letterDisplay;

    private void Awake() {
        Debug.Log("Output: " + LetterInputScene.letterInputScene.letterSaved);
        letterDisplay.text = LetterInputScene.letterInputScene.letterSaved;
    }
}
