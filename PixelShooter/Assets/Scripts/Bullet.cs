using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public bool isRotate;

    private void Update()
    {
        if(isRotate)
        {
            transform.Rotate(Vector3.forward * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BorderBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
