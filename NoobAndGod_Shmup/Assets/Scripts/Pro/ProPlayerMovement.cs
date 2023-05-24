using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProPlayerMovement : MonoBehaviour
{
    private Camera m_Camera;

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Vector2 direction;

    public float cameraSpeed;

    private void Awake()
    {
        m_Camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(InputX, InputY).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(transform.position.x, transform.position.y, -10);

        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}