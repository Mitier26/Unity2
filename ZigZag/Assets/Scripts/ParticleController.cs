using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particle.Play();
    }

    private void Update()
    {
        if(particle.isPlaying == false)
        {
            gameObject.SetActive(false);
        }
    }
}
