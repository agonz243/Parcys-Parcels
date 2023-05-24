using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerChange : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer sprite;
    private sprinklerGame script;

    // Start is called before the first frame update
    void Start()
    {
        script = player.GetComponent<sprinklerGame>();

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Layer: " + sprite.sortingLayerName);
        if(script.countdownTimeLeft > 0){
            sprite.sortingLayerName = "AboveBG";
        }
        else if(script.myPlayer.getPointIndex() > 10){
            sprite.sortingLayerName = "bg-top";
        } else{
            sprite.sortingLayerName = "bg-bottom";
        }
    }
}
