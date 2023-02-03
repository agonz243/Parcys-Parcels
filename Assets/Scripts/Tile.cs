using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //declaring the two diff tile colors based on logic in gridManager
    [SerializeField] private Color baseColor, offsetColor;

    //need to use sprite renderer for colors
    [SerializeField] private SpriteRenderer renderah;

    public void Init(bool isOffset)
    {
        renderah.color = isOffset ? offsetColor : baseColor;
    }
}
