using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mouseCustom : MonoBehaviour
{
    //widdle paw :3
    public Texture2D paw;
    public Texture2D grab;

    //whatever settings blah blah
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public bool autoCenterHotSpot = false;
    public Vector2 hotSpotCustom;
    private Vector2 hotSpotAuto;
    Vector2 hotSpot;

    // Countdown at the beginning of the scene
    public float countdownTimeLeft;
    public int beginGame;
    [SerializeField]private TextMeshProUGUI countdownTxt;
    public GameObject transRec;
    public float alpha = 0.1f;//half transparency
    private Material currentMat;
    public GameObject stopTime;
    public float seconds;

    private void Start()
    {
            hotSpotCustom = new Vector2(60.0f, 45.0f);
            hotSpot = hotSpotCustom;
        
        Cursor.SetCursor(paw, hotSpot, CursorMode.ForceSoftware);

        // Don't destroy paw script for whole game
        DontDestroyOnLoad(gameObject);

        // Bool for Countdown
        beginGame = 0;
    }

    //want to add on click here, so do that u bum 
    private void Update()
    {
        if (beginGame == 0){
            countdownTimeLeft -= Time.deltaTime;
            alpha -= 0.001f;

            transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, alpha);

            // if (seconds != Mathf.FloorToInt(countdownTimeLeft % 60)){
            //     squeakSource.Play();
            // }

            seconds = Mathf.FloorToInt(countdownTimeLeft % 60);

            // Debug.Log(alpha);

            if (countdownTimeLeft < 1){
                countdownTxt.outlineColor = new Color (0.12f, 0.48f, 0.16f, 1f); // dark green outline
                countdownTxt.faceColor = new Color (0f, 1f, 0f, 1f); // green font
                countdownTxt.text = string.Format("GO!");
            } else if (countdownTimeLeft < 2){
                countdownTxt.outlineColor = new Color (1f, 0.67f, 0.04f, 1f); // dark yellow outline
                countdownTxt.faceColor = new Color (1f, 0.78f, 0.31f, 1f); // yellow font
                countdownTxt.text = string.Format("{0}", seconds);
            } else if (countdownTimeLeft < 3) {
                countdownTxt.outlineColor = new Color (0.91f, 0.45f, 0.30f, 1f); // dark orange outline
                countdownTxt.faceColor = new Color(1.0f, 0.64f, 0.0f, 1f); // orange font
                countdownTxt.text = string.Format("{0}", seconds);
            } else if (countdownTimeLeft < 4) {
                // countdownTxt.outlineColor = new Color (0.51f, 0.20f, 0.25f, 1f); // dark red outline
                countdownTxt.outlineColor = new Color (0.47f, 0.09f, 0.15f, 1f); // dark red outline
                countdownTxt.faceColor = new Color (0.66f, 0.14f, 0.21f, 1f); // red font
                countdownTxt.text = string.Format("{0}", seconds);
            }

            if(countdownTimeLeft <= 0) {
                countdownTimeLeft = 0;
                // countdownTxt.text = string.Format("GO!");
                countdownTxt.faceColor = new Color32(0, 0, 0, 0);
                countdownTxt.outlineWidth = 0.0f;
                countdownTxt.outlineColor = new Color32(0, 0, 0, 0);
                transRec.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, 0f);
                beginGame = 1;
                stopTime.SetActive(true);
            }

        } else {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(grab, hotSpotCustom, CursorMode.ForceSoftware);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(paw, hotSpotCustom, CursorMode.ForceSoftware);
            }
        }
    }
}
