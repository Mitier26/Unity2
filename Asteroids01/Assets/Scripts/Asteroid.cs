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
}
