using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer2 : MonoBehaviour
{
    private Rigidbody2D rb;         // ���� �̵��� ����
    private RaycastHit2D hit;       // �ٴڿ� �ִ� �� Ȯ�� ��
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public FixedJoystick joystick;

    public float inputX;           // �Է� �޴� X ��
    [SerializeField]
    private float moveSpeed;        // �̵� �ӵ�
    [SerializeField]
    private float jumpForce;        // ���� ��

    private float angle;            // ȸ�� ����
    public Vector2 jumpDirection;  // ���� ����

    public bool isGround;          // �ٴڿ� �ִ��� Ȯ�ο�
    public bool isJump;            // ���� ������ Ȯ�ο�

    [SerializeField]
    private float checkDistance;    // �������� ����, �˻� ����
    [SerializeField]
    private LayerMask checkLayer;

    [SerializeField]
    private GameObject arrow;       // ���� ���� ǥ�ÿ�
    public Vector2 arrowDirection; // ���� ����
    public float arrowAngle;       // ���� ����
    public float rotationSpeed = 5f;

    public Transform rayPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        // x���� �Է� �޴´�.
        if (!isJump)
        {
            inputX = joystick.Horizontal;
            //inputX = Input.GetAxisRaw("Horizontal");
        }


        RayHit();           // ������
        ArrowDirection();   // ���� ����

        // ���ϸ޴ϼ� ����
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("Ground", hit);

        // ����
        animator.SetBool("Slope", Mathf.Abs(angle) > 25f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // �׸��� �¿� ����
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
        // �Ʒ� �������� �������� �߻��Ѵ�.
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
        // �ٴڿ� ������ 
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
