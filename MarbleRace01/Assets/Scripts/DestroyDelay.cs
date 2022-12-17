using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
