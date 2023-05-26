using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageSwitch : MonoBehaviour
{

    // public TextMeshProUGUI[] textboxes;
    public GameObject[] objects;
    private int index;

    public sceneChanger sceneChanger;

    void Start()
    {
        index = 0;

        for (int x = 1; x < objects.Length; x++) {
            objects[x].SetActive(false);
        }
    }

    public void NextPage() {
        if(index == objects.Length - 1) {
            sceneChanger.LoadNextScene();
        } else {
            objects[index].SetActive(false);
            index++;
            objects[index].SetActive(true);
        }
    }
}
