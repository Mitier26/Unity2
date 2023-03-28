using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCoin : ProBox
{
    [SerializeField]
    private float coinLifeTime = 8f;


    private void OnEnable()
    {
        StartCoroutine(DestoryCoin());
    }

    private IEnumerator DestoryCoin()
    {
        yield return new WaitForSeconds(coinLifeTime);
        base.DestroyObject();
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ProAudioManager.instance.PlaySound(ProAudioManager.PROSFX.Coin, transform.position);
            ProParticleManager.instance.PlayParticle(ProParticleManager.PARTICLE.Coin, collision.contacts[0].point);
        }
    }
}
