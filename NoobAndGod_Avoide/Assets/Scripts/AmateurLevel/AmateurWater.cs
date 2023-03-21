using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurWater : MonoBehaviour
{
    [SerializeField]
    private GameObject waterParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.SFX.Water);
        GameObject p = Instantiate(waterParticle);
        p.transform.position = collision.transform.position;
        p.GetComponent<ParticleSystem>().Play();
    }
}
