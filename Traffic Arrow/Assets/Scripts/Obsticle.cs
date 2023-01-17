using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;      // 적의 이동 속도

    private Vector2 moveDirection;                  // 적의 이동 방향

    private void Awake()
    {
        // 적의 이동 방향을 랜덤한 방향으로 정한다.
        moveDirection = (Random.Range(-1f, 1f) * Vector2.up) + (Random.Range(-1f, 1f) * Vector2.right);
        moveDirection = moveDirection.normalized;
    }


    private void FixedUpdate()
    {
        // 적의 이동
        transform.position += (Vector3)(_moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Side"))
        {
            moveDirection.x *= -1f;
        }

        if (collision.CompareTag("Top"))
        {
            moveDirection.y *= -1f;
        }
    }
}
