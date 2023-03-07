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
        DontDestroyOnLoad(image);
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
        if (wipeProgress >= 6f)
        {
            isDone = true;
        } else if (wipeProgress >= 3f)
        {
            this.GetComponent<sceneChanger>().LoadNextScene();
        }
    }

/*     private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "transitioner")
        {
            Debug.Log("Change Scene Now!");
            this.GetComponent<sceneChanger>().LoadNextScene();
        }
    } */

    [ContextMenu("Wipe")]
    private void Wipe() { ToggleWipe(); }
}
