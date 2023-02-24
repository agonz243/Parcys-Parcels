using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCustom : MonoBehaviour
{
    //widdle paw :3
    public Texture2D paw;
    //whatever settings blah blah
    public CursorMode cursorMode = CursorMode.Auto;
    public bool autoCenterHotSpot = false;
    public Vector2 hotSpotCustom = Vector2.zero;
    private Vector2 hotSpotAuto;

    private void Start()
    {
        Vector2 hotSpot;
        if (autoCenterHotSpot)
        {
            hotSpotAuto = new Vector2(paw.width * 0.5f, paw.height * 0.5f);
            hotSpot = hotSpotAuto;
        }
        else
        {
            hotSpot = hotSpotCustom;
        }
        Cursor.SetCursor(paw, hotSpot, cursorMode);
    }

    //want to add on click here, so do that u bum 
}
