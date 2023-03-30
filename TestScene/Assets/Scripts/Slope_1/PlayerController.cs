using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rig;                    // 물리 효과
    float inputX;                       // 입력 방향
    public int moveSpeed;               // 이동 속도
    public LayerMask groundMask;        // 충돌 감지 레이어

    public Transform chkPos;            // 바닥을 확인 하는 부분
    public bool isGround;               // 바닥이가
    public float checkRadius;           // 바닥 검사 범위

    public float jumpPower = 1;         // 점프 힘
    bool isJump;                        // 점프 중인가

    public float distance;              // 레이저의 거리
    public float angle;                 // 노말과 위 방향과의 각도
    public Vector2 perp;                // 레이저가 충돌한 것의 수직 방향
    public bool isSlope;                // 경사로 인가

    public Transform frontchk;          // 이동 방향의 앞을 확인

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        GroundChk();
        Flip();

        if (inputX == 0)
        {
            // 입력된 것이 없으면 Rigidbody에 있는 Freeze 를 켜는 것이다.
            // 비트 연산을 이용했다.
            // 이럿게 하지 않으면 작동하지 않는다.
            rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
            rig.constraints = RigidbodyConstraints2D.FreezeRotation;

        // 레이져를 발사해 충돌이 있는지 확인을 한다.
        RaycastHit2D hit = Physics2D.Raycast(chkPos.position, Vector2.down, distance, groundMask);

        RaycastHit2D fronthit = Physics2D.Raycast(frontchk.position, transform.right, 0.1f, groundMask);

        if (hit || fronthit)
        {
            if (fronthit)
                SlopeChk(fronthit);
            else if (hit)
                SlopeChk(hit);


            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
            Debug.DrawLine(hit.point, hit.point + perp, Color.red);
        }

        // 입력된 것이 있다면
        if(inputX != 0)
        {
            // 경사, 바닥, 점프아님, 각도 좋음
            if (isSlope && isGround && !isJump && angle < maxangle)
            {
                rig.velocity = Vector2.zero;

                // 경사면에 있을 때 경사면의 방향으로 힘들주어 경사로를 이동하는 것 처럼 보이게 한다.
                if(inputX > 0)
                    transform.Translate(new Vector2(perp.x * moveSpeed * -inputX * Time.deltaTime, perp.y * moveSpeed * -inputX * Time.deltaTime));
                else if(inputX < 0)
                    transform.Translate(new Vector2(perp.x * moveSpeed * inputX * Time.deltaTime, perp.y * moveSpeed * -inputX * Time.deltaTime));

            }
            // 평지에 있을 때
            else if (!isSlope && isGround && !isJump)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * Mathf.Abs(inputX));
            else if (!isGround)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * Mathf.Abs(inputX));
        }

        //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * Mathf.Abs(inputX));
    }

    public float maxangle;
    private void FixedUpdate()
    {
        //if (isSlope && isGround & !isJump)
        //    rig.velocity = perp * moveSpeed * inputX * -1f;
        //else if (!isSlope && isGround && !isJump)
        //    rig.velocity = new Vector2(inputX * moveSpeed, 0);
        //else if (!isGround)
        //    rig.velocity = new Vector2(inputX * moveSpeed, rig.velocity.y);

        Jump();
    }

    public float maxAngle;
    void GroundChk()
    {
        // 바닥 확인
        isGround = Physics2D.OverlapCircle(chkPos.position, checkRadius, groundMask);
    }

    void Flip()
    {
        // 입력 값에 따라 회전 , 좌, 우
        if (inputX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void SlopeChk(RaycastHit2D hit)
    {
        // 경사로 인지 확인을 한다.
        // 지면과 평행인 방향
        perp = Vector2.Perpendicular(hit.normal).normalized;

        // 각도를 구하는 것이다.
        angle = Vector2.Angle(hit.normal, Vector2.up);

        if (angle != 0)
            isSlope = true;
        else
            isSlope = false;
    }

    private void Jump()
    {
        if (rig.velocity.y <= 0)
            isJump = false;

        if (isGround)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                isJump = true;
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }
}
