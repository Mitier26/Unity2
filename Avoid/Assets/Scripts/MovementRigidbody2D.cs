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
    private LayerMask groundLayer;              // 바닥 충돌 체크를 위한 레이어

    private bool isGounded;                     // 바닥 체크
    private Vector2 footPosition;               // 바닥 체크를 위한 발의 위치
    private Vector2 footArea;                   // 바닥 체크를 위한 발 인식 범위

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
        // 플레이어 오브젝트의 Collider2D 정보
        // Collider 와 Collsion 차이 조심
        // min ( 왼쪽 아래 ), center ( 중심 ), max ( 오른쪽 위 ) 
        Bounds bounds = collider2D.bounds;
        // 발의 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어 발 영역 위치 설정
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        // 플레이어 발 영역 범위 설정
        // 영역이 바닥과 닿아있으면 IsGround = true;
        isGounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);
        // OverlapBox를 만들고 바닥과 검사 한다.
        // 위치, 영역, 각도, 판정 레이어

        // 바닥에 닿아 있고 속도가 0 이하 이면 
        // velocity.y 가 ( + ) 이면 점프중인다.
        // 플레이어가 하강 하고 있을 때 velocity.y 가 ( - ) 가 된다.
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
        // Clamp positon.x 값이 min.x 보다 작으면 min.x, max.x 보다 크면 max.x 사이 값이면 position.x
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
