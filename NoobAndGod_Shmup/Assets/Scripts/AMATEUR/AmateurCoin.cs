using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurCoin : MonoBehaviour
{
    [SerializeField] private float timer = 2f;

    private void OnEnable()
    {
        Invoke(nameof(DisableCoin), timer);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void DisableCoin()
    {
        gameObject.SetActive(false);
    }
}
