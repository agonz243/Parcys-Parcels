using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterOutputScene : MonoBehaviour
{
    public TextMeshProUGUI letterDisplay;

    private void Awake() {
        letterDisplay.text = LetterInputScene.letterInputScene.letterSaved;
    }
}
