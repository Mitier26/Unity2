using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer2 : MonoBehaviour
{
    private Rigidbody2D rb;         // 물리 이동을 위해
    private RaycastHit2D hit;       // 바닥에 있는 것 확인 용
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public FixedJoystick joystick;

    public float inputX;           // 입력 받는 X 값
    [SerializeField]
    private float moveSpeed;        // 이동 속도
    [SerializeField]
    private float jumpForce;        // 점프 힘

    private float angle;            // 회전 각도
    public Vector2 jumpDirection;  // 점프 방향

    public bool isGround;          // 바닥에 있는지 확인용
    public bool isJump;            // 점프 중인지 확인용

    [SerializeField]
    private float checkDistance;    // 레이저의 길이, 검색 범위
    [SerializeField]
    private LayerMask checkLayer;

    [SerializeField]
    private GameObject arrow;       // 점프 방향 표시용
    public Vector2 arrowDirection; // 점프 방향
    public float arrowAngle;       // 점프 각도
    public float rotationSpeed = 5f;

    public Transform rayPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        // x값을 입력 받는다.
        if (!isJump)
        {
            inputX = joystick.Horizontal;
            //inputX = Input.GetAxisRaw("Horizontal");
        }


        RayHit();           // 레이저
        ArrowDirection();   // 점프 방향

        // 에니메니션 변경
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("Ground", hit);

        // 기울기
        animator.SetBool("Slope", Mathf.Abs(angle) > 25f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 그림의 좌우 반전
        if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (isJump)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);
            angle = transform.rotation.eulerAngles.z;

        }
        else
        {
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }

        float clampX = Mathf.Clamp(transform.position.x, -22f, 22f);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
    }

    private void RayHit()
    {
        // 아래 방향으로 레이저를 발사한다.
        hit = Physics2D.Raycast(rayPoint.position, Vector2.down, checkDistance, checkLayer);

        if (hit)
        {
            Vector2 normal = hit.normal;
            angle = Mathf.Atan2(normal.x, normal.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * rotationSpeed);
        }
        else
        {
            isGround = false;
        }
    }

    private void ArrowDirection()
    {
        jumpDirection = Quaternion.Euler(0, 0, -angle) * Vector2.up;

        arrowDirection = new Vector2(inputX, jumpDirection.y).normalized;
        arrowAngle = Mathf.Atan2(arrowDirection.x, arrowDirection.y) * Mathf.Rad2Deg;

        float arrowResult = transform.rotation.eulerAngles.z - arrowAngle;

        arrow.transform.localRotation = Quaternion.Euler(0, 0, arrowResult);

    }


    public void bJump()
    {
        if (isGround)
        {
            isJump = true;
            animator.SetTrigger("Jump");
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            GodAudioManager.Instance.PlaySoundEffect(GodAudioManager.SFX.Jump);
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                isJump = true;
                animator.SetTrigger("Jump");
                rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GodGameManager.Instance.GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 바닥에 있으면 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = true;
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        isJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPoint.position, Vector2.down * checkDistance);
    }

}
