using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float speed = 20.0f;
    private float maxLifeTime = 30.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetSprite();   
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if(size * 0.5 >= minSize)
            {
                SpawnSmallAsteroid();
            }

            GameManager.instance.AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void SpawnSmallAsteroid()
    {
        int randomInt = Random.Range(0, 4);

        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        for (int i = 0; i <= randomInt; i++)
        {
            Asteroid asteroid = Instantiate(this, position, transform.rotation);
            asteroid.size = size * 0.5f;
            asteroid.SetTrajectory(Random.insideUnitCircle.normalized);
        }
    }
}
