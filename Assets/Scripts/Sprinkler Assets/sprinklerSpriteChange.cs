using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinklerSpriteChange : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteArray;

    public void ChangeSprite(string sprite){
        switch(sprite)
        {
            case "u1":
                spriteRenderer.sprite = spriteArray[0];
                break;
            case "u2":
                spriteRenderer.sprite = spriteArray[1];
                break;
            case "d1":
                spriteRenderer.sprite = spriteArray[2];
                break;
            case "d2":
                spriteRenderer.sprite = spriteArray[3];
                break;
            case "r1":
                spriteRenderer.sprite = spriteArray[4];
                break;
            case "r2":
                spriteRenderer.sprite = spriteArray[5];
                break;
            case "l1":
                spriteRenderer.sprite = spriteArray[6];
                break;
            case "l2":
                spriteRenderer.sprite = spriteArray[7];
                break;
        }
    }
}
