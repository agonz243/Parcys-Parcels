using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //declaring the two diff tile colors based on logic in gridManager
    [SerializeField] private Color baseColor, offsetColor;

    //need to use sprite renderer for colors
    [SerializeField] private SpriteRenderer renderah;

    [SerializeField] private GameObject highlights;

    [SerializeField] public Collider2D bonk;

    [SerializeField] public bool isSolved = false;

     public void Init(bool isOffset)
    {
        renderah.color = isOffset ? offsetColor : baseColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Packages")
        {
            highlights.SetActive(true);
        }
        highlights.SetActive(true);
    }

    void OnMouseExit()
    {
        highlights.SetActive(false);
    }
}
