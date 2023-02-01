using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;

    public float rotation;
    public float moveSpeed;

    public float damege;

    // ¼Ò¸ê ½Ã°£
    public float lifeTime;
    private float lifeTimer;

    private void Start()
    {
        lifeTimer = lifeTime;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;

        if(lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        lifeTimer = lifeTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }
}
