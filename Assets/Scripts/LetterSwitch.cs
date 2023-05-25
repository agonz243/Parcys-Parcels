using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterSwitch : MonoBehaviour
{

    public TextMeshProUGUI[] textboxes;
    private int index;

    public sceneChanger sceneChanger;

    // // Start is called before the first frame update
    void Start()
    {
        index = 0;

        for (int x = 1; x < textboxes.Length; x++) {
            textboxes[x].enabled = false;
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void NextPage() {
        if(index == textboxes.Length - 1) {
            sceneChanger.LoadNextScene();
        } else {
            textboxes[index].enabled = false;
            index++;
            textboxes[index].enabled = true;
        }
    }
}
