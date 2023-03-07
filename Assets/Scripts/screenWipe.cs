using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenWipe : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float wipeSpeed = 1f;

    public GameObject image;

    private bool isDone;

    private float wipeProgress;

     private void Awake()
    {
        isDone = true;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            WipeScreen();
        } else {
            wipeProgress = 0;
        }
    }


    private void ToggleWipe() {
        isDone = false;
    }

    private void WipeScreen()
    {
        wipeProgress += Time.deltaTime * (1f / wipeSpeed);
        image.transform.position += new Vector3(0.3f, 0, 0);
        if (wipeProgress >= 5f)
        {
            isDone = true;
        }
    }

    [ContextMenu("Wipe")]
    private void Wipe() { ToggleWipe(); }
}
