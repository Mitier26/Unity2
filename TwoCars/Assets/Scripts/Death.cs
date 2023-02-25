using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Circle"))
        {
            GameManager.instance.GameOver();
        }
        if(collision.CompareTag("Square"))
        {
            Destroy(collision.gameObject);
        }
    }
}
