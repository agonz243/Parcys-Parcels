using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyAnimation : MonoBehaviour
{
    public Animator parcyAnimator;

    public SpriteRenderer wRenderer;
    public SpriteRenderer aRenderer;
    public SpriteRenderer sRenderer;
    public SpriteRenderer dRenderer;

    public Sprite wPressed;
    public Sprite aPressed;
    public Sprite sPressed;
    public Sprite dPressed;

    public Sprite w;
    public Sprite a;
    public Sprite s;
    public Sprite d;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(parcyAnimator.GetAnimatorTransitionInfo(0).nameHash);

// || parcyAnimator.GetAnimatorTransitionInfo(0).IsName("RightUp")
        if(parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Up" ) {
            wRenderer.sprite = wPressed;
            aRenderer.sprite = a;
            sRenderer.sprite = s;
            dRenderer.sprite = d;
        } else if (parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Left") {
        //| parcyAnimator.GetAnimatorTransitionInfo(0).nameHash == 0) {
            wRenderer.sprite = w;
            aRenderer.sprite = aPressed;
            sRenderer.sprite = s;
            dRenderer.sprite = d;
        } else if (parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Down") {
            wRenderer.sprite = w;
            aRenderer.sprite = a;
            sRenderer.sprite = sPressed;
            dRenderer.sprite = d;
        } else if (parcyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Right") {
            wRenderer.sprite = w;
            aRenderer.sprite = a;
            sRenderer.sprite = s;
            dRenderer.sprite = dPressed;
        } else {
            wRenderer.sprite = w;
            aRenderer.sprite = a;
            sRenderer.sprite = s;
            dRenderer.sprite = d;
        }
    }
}
