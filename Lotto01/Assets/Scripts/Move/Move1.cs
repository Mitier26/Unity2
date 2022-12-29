using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform target;
    public Rigidbody2D rb;
    public Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement =  target.position - transform.position;
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        // normalized 가 있는 것과 없는 것의 차이를 알 수 있다.
    }
    private void FixedUpdate()
    {
        MoveTranslate(movement);
    }
    public void MoveTranslate(Vector2 direction)
    {
        transform.Translate( direction * speed * Time.deltaTime);
    }

    public void MoveAddForce(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }

    public void MoveVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    public void MoveMovePositon(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
