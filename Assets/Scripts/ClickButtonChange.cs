using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonChange : MonoBehaviour
{
    public Sprite pressedButtonImg;
    private Button button;
    private Image buttonImg;

    public void Start() {
        button = this.GetComponent<Button>();
    }

    public void OnButtonClick(){
        buttonImg = button.GetComponent<Image> ();
        buttonImg.sprite = pressedButtonImg;
    }
}
