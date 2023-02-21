using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprinklerParticle : MonoBehaviour
{
    private ParticleSystem part;

    public List<ParticleCollisionEvent> colEvents;

    void Start(){
        part = GetComponent<ParticleSystem>();
        colEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, colEvents);
        // Debug.Log("Hit");

        // for(int i = 0; i < numCollisionEvents; i++){

        // }
        // if(other.TryGetComponent(out Path pl)){
        //     pl.playerHit();
        // }
        if(other.TryGetComponent(out sprinklerGame pl)){
            pl.playerHit();
        }
    }
}
