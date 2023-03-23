using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;          // 바닥 확인을 위한 크기 사용

    [SerializeField]
    private float moveSpeed = 5f;                       // 이동 속도
    [SerializeField]
    private float jumpForce = 14f;                      // 점프 파워

    private float horizontalInput;                      // 좌우 입력 값
    private Vector3 moveDirection = Vector3.zero;       // 이동 방향

    [SerializeField]
    private bool isGround;                              // 바닥에 있는지 확인
    [SerializeField]
    private LayerMask groundLayer;                      // 바닥 확인을 위한

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        HorizontalMovement();

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider.size.y, groundLayer);
        if (hit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));   
    }

    private void HorizontalMovement()
    {
        moveDirection = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        transform.Translate(moveDirection * Time.deltaTime);
    }
}
