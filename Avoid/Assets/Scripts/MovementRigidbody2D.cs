using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementRigidbody2D : MonoBehaviour
{
    [Header("Move Horizontal")]
    [SerializeField]
    private float moveSpeed = 8f;

    [Header("Move Vertical")]
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float lowGravity = 2f;
    [SerializeField]
    private float highGravity = 3f;

    [SerializeField]
    private int maxJumpCount = 2;
    private int currentJumpCount;


    [Header("Collision")]
    [SerializeField]
    private LayerMask groundLayer;              // �ٴ� �浹 üũ�� ���� ���̾�

    private bool isGounded;                     // �ٴ� üũ
    private Vector2 footPosition;               // �ٴ� üũ�� ���� ���� ��ġ
    private Vector2 footArea;                   // �ٴ� üũ�� ���� �� �ν� ����

    private Rigidbody2D rigid2D;
    private new Collider2D collider2D;

    public bool IsLongJump { get; set; } = false;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        // �÷��̾� ������Ʈ�� Collider2D ����
        // Collider �� Collsion ���� ����
        // min ( ���� �Ʒ� ), center ( �߽� ), max ( ������ �� ) 
        Bounds bounds = collider2D.bounds;
        // ���� ��ġ ����
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // �÷��̾� �� ���� ��ġ ����
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        // �÷��̾� �� ���� ���� ����
        // ������ �ٴڰ� ��������� IsGround = true;
        isGounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);
        // OverlapBox�� ����� �ٴڰ� �˻� �Ѵ�.
        // ��ġ, ����, ����, ���� ���̾�

        // �ٴڿ� ��� �ְ� �ӵ��� 0 ���� �̸� 
        // velocity.y �� ( + ) �̸� �������δ�.
        // �÷��̾ �ϰ� �ϰ� ���� �� velocity.y �� ( - ) �� �ȴ�.
        if(isGounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        if(IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravity;
        }
        else
        {
            rigid2D.gravityScale = highGravity;
        }
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(transform.position.x, Constants.min.x, Constants.max.x);
        // Clamp positon.x ���� min.x ���� ������ min.x, max.x ���� ũ�� max.x ���� ���̸� position.x
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    public bool JumpTo()
    {
        if(currentJumpCount > 0)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
            currentJumpCount--;

            return true;
        }

        return false;
    }
}
