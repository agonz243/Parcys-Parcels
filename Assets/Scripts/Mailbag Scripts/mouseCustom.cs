using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
            hotSpotCustom = new Vector2(60.0f, 45.0f);
            hotSpot = hotSpotCustom;
        
        Cursor.SetCursor(paw, hotSpot, CursorMode.ForceSoftware);
    }

    //want to add on click here, so do that u bum 
    private void Update()
    {
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
