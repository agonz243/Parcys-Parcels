using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinklerSpriteChange : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteArray;

    void ChangeSprite(string sprite){
        switch(sprite)
        {
            case u1:
                spriteRenderer.sprite = spriteArray[0];
                break;
        }
    }
}
