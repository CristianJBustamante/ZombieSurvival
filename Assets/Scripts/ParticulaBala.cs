using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaBala : MonoBehaviour
{
    public ParticleSystem particleSystem;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            particleSystem.Play();
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        int events = particleSystem.GetCollisionEvents(other, colEvents);
        Debug.Log("HIT");
    }
}
