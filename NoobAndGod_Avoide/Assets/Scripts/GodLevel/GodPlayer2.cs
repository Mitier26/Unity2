using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer2 : MonoBehaviour
{
    private Rigidbody2D rb;         // 물리 이동을 위해
    private RaycastHit2D hit;       // 바닥에 있는 것 확인 용

    private float inputX;           // 입력 받는 X 값
    [SerializeField]
    private float moveSpeed;        // 이동 속도
    [SerializeField]
    private float jumpForce;        // 점프 힘

    private float angle;            // 회전 각도
    public Vector2 jumpDirection;  // 점프 방향

    private bool isGround;          // 바닥에 있는지 확인용
    private bool isJump;            // 점프 중인지 확인용

    [SerializeField]
    private float checkDistance;    // 레이저의 길이, 검색 범위
    [SerializeField]
    private LayerMask checkLayer;

    [SerializeField]
    private GameObject arrow;       // 점프 방향 표시용
    private Vector2 arrowDirection; // 점프 방향
    private float arrowAngle;       // 점프 각도

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (!isGround) return;
        // x값을 입력 받는다.
        inputX = Input.GetAxis("Horizontal");

        // 아래 방향으로 레이저를 발사한다.
        hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, checkLayer);

        if(hit)
        {
            Vector2 normal = hit.normal;
            angle = Mathf.Atan2(normal.x, normal.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * 5);
        }

        jumpDirection = Quaternion.Euler(0, 0, -angle) * Vector2.up;

        arrowDirection = new Vector2(inputX, jumpDirection.y);
        arrowAngle = Mathf.Atan2(arrowDirection.x, arrowDirection.y) * Mathf.Rad2Deg;

        float arrowResult = transform.rotation.eulerAngles.z - arrowAngle;

        arrow.transform.localRotation = Quaternion.Euler(0, 0, arrowResult);
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
            angle = transform.rotation.eulerAngles.z;
        }
        else
        {
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }

        // 점프한다.
        Jump();
    }

    private void Jump()
    {
        if (isGround)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                isJump = true;

                rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 바닥에 있으면 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = true;
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

}
