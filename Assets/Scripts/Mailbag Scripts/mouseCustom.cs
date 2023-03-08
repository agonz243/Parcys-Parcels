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
    public Vector2 hotSpotCustom = Vector2.zero;
    private Vector2 hotSpotAuto;
    Vector2 hotSpot;
    private void Start()
    {
        //Vector2 hotSpot;
        if (autoCenterHotSpot)
        {
            hotSpotAuto = new Vector2(paw.width * 0.5f, paw.height * 0.4f);
            hotSpot = hotSpotAuto;
        }
        else
        {
            hotSpot = hotSpotCustom;
        }
        Cursor.SetCursor(paw, hotSpot, CursorMode.ForceSoftware);
    }

    //want to add on click here, so do that u bum 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(grab, hotSpot, CursorMode.ForceSoftware);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(paw, hotSpot, CursorMode.ForceSoftware);
        }
    }
}
