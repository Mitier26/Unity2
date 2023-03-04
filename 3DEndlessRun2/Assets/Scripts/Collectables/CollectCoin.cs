using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX;
    private void OnTriggerEnter(Collider other)
    {
        coinFX.Play();
        gameObject.SetActive(false);
    }
}
