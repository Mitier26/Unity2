using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if(particle.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
