using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Vector2 durationRange = new Vector2(5, 10);
    [SerializeField] private float destroyDelay = 1f;

    private float duration = 0f;
    private float destroyTimer = 0f;

    private void Start()
    {
        duration = Random.Range(durationRange.x, durationRange.y);
        particleSystem.Play();
    }

    private void Update()
    {
        if(destroyTimer > duration)
        {
            particleSystem.Stop();

            if(destroyTimer - duration > destroyDelay)
            {
                Destroy(gameObject);
            }
        }

        destroyTimer += Time.deltaTime;
    }

}
