using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    public Vector3 direction;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 temp = transform.position;

        if(temp.x > 10 || temp.z > 10 || temp.x < -10 || temp.z < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            Destroy(other.gameObject);
            GameManager.instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}
