using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    RaycastHit2D hit;
    public float detectDistance = 1f;
    public float testRot = 0;
    public Vector2 normal;
    public Vector2 hitPos;
    public Vector2 rePos;
    public float angle;
    public LayerMask checkLayer;

    public float inputX;
    public float moveSpeed;
    public float jumpForce;

    public Vector2 jumpDirection;
    public Vector2 arrowDirection;
    public float arrowAngle;

    public bool isGround;
    public bool isJump = false;

    public GameObject directionArrow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        hit = Physics2D.Raycast(transform.position, Vector2.down, detectDistance, checkLayer);
        //hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.down, detectDistance, LayerMask.GetMask("Ground"));

        if (hit)
        {
            // 법선 벡터를 이용하여 플레이어 회전
            Vector2 normal = hit.normal;
            angle = Mathf.Atan2(normal.x, normal.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * 5);
        }


        // 점프의 방향
        jumpDirection = Quaternion.Euler(0, 0, -angle) * Vector2.up;

        arrowDirection = new Vector2(inputX, jumpDirection.y);
        arrowAngle = Mathf.Atan2(arrowDirection.x, arrowDirection.y) * Mathf.Rad2Deg;

        float arrowResult = transform.rotation.eulerAngles.z - arrowAngle;

        directionArrow.transform.localRotation = Quaternion.Euler(0, 0, arrowResult);
    }

    private void FixedUpdate()
    {
        if(isJump)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
            angle = transform.rotation.eulerAngles.z;
        }
        else
        {
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }

        Jump();
    }

    private void Jump()
    {
        if (isGround)
        {
            if(Input.GetAxis("Jump") != 0)
            {
                isJump = true;

                // 필수
                //jumpDistance = Quaternion.Euler(0, 0, -angle) * Vector2.up;

                rb.AddForce( jumpDirection * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * detectDistance);
    }
}
