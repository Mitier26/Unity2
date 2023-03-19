using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerObject : MonoBehaviour
{
    public float moveSpeed;

    private void Start()
    {
        moveSpeed = 2f;
    }
    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "BeginnerDestroyer")
        {
            Destroy(gameObject);
        }
    }
}
