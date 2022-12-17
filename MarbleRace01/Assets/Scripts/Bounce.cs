using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 500f;
    private bool isBouncing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            rb.AddForce(collision.contacts[0].normal * speed);
            isBouncing = true;
            Invoke("StopBounce", 0.1f);
        }
    }

    private void StopBounce()
    {
        isBouncing = false;
    }
}
