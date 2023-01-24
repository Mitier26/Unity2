using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject itemEffect;

    public void Exit()
    {
        Instantiate(itemEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
