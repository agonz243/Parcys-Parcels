using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenWipe : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float wipeSpeed = 0.5f;

    private GameObject image;

    private Vector3 startPos;

    private bool isDone;

    private bool sceneChanged;

    private float wipeProgress;

     private void Awake()
    {
        image = GameObject.Find("Transition");
        isDone = true;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(image);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            WipeScreen();
        }
    }


    public void ToggleWipe() {
        isDone = false;
    }

    private void WipeScreen()
    {
        wipeProgress += Time.deltaTime * (1f / wipeSpeed);
        image.transform.position += new Vector3(0.3f, 0, 0);
        if (wipeProgress >= 6f)
        {
            isDone = true;
            Destroy(image);
            Destroy(this.gameObject);
        } else if (wipeProgress >= 3f && !sceneChanged)
        {
            this.GetComponent<sceneChanger>().LoadNextScene();
            sceneChanged = true;
        }
    }

    [ContextMenu("Wipe")]
    private void Wipe() { ToggleWipe(); }
}
