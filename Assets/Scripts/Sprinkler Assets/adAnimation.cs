using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adAnimation : MonoBehaviour
{
    public Animator parcyAnimator;

    public SpriteRenderer aRenderer;
    public SpriteRenderer dRenderer;

    public Sprite aPressed;
    public Sprite dPressed;

    public Sprite a;
    public Sprite d;

    public double frame;
    
    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frame = Math.Floor(parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length * (parcyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate);
        if (frame == 0) {
            aRenderer.sprite = aPressed;
            dRenderer.sprite = d;
        } else if (frame == 1) {
            aRenderer.sprite = a;
            dRenderer.sprite = dPressed;
        } else {
            aRenderer.sprite = a;
            dRenderer.sprite = d;
        }
    }
}
