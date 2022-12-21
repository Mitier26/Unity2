using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    public float power;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power);
        }
    }
}
