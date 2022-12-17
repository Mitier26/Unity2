using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform newPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Marble")
        {
            collision.transform.position = newPos.position;
        }
    }
}
