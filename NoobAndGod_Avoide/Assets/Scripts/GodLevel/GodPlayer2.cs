using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer2 : MonoBehaviour
{
    private Rigidbody2D rb;         // ���� �̵��� ����
    private RaycastHit2D hit;       // �ٴڿ� �ִ� �� Ȯ�� ��

    private float inputX;           // �Է� �޴� X ��
    [SerializeField]
    private float moveSpeed;        // �̵� �ӵ�
    [SerializeField]
    private float jumpForce;        // ���� ��

    private float angle;            // ȸ�� ����
    public Vector2 jumpDirection;  // ���� ����

    private bool isGround;          // �ٴڿ� �ִ��� Ȯ�ο�
    private bool isJump;            // ���� ������ Ȯ�ο�

    [SerializeField]
    private float checkDistance;    // �������� ����, �˻� ����
    [SerializeField]
    private LayerMask checkLayer;

    [SerializeField]
    private GameObject arrow;       // ���� ���� ǥ�ÿ�
    private Vector2 arrowDirection; // ���� ����
    private float arrowAngle;       // ���� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (!isGround) return;
        // x���� �Է� �޴´�.
        inputX = Input.GetAxis("Horizontal");

        // �Ʒ� �������� �������� �߻��Ѵ�.
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

        // �����Ѵ�.
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
        // �ٴڿ� ������ 
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
