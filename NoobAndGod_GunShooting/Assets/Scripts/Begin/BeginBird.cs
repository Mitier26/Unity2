using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBird : MonoBehaviour
{
    // 새의 행동을 만든다.
    // 새는 아래로 이동하지 않는다.
    // 새는 계속 이동하며 방향만 변경한다.
    // 좌우 이동에 따라 scale 을 변경한다.
    // 생성되면 알아서 행동한다.
    // 처음 부터 좌우로 나가는 것을 방지 해야 한다.
    // aim
    // 바닥에 있을 때 조준 금지 해야 한다.
    // -6 ~ 6 생성

    // 이동 속도는 레벨에 따라 다르게 만들 수도 있다.
    public float moveSpeed;             // 새의 이동 속도, 레벨에 영향을 받아야 한다.
    public float normalSpeed;
    public float dashSpeed;

    public float changeTime;            // 방향을 바꾸는 시간
    public float currentTime = 0;       // 현제 작동 시간

    private Vector3 direction;          // 새의 방향

    private void Start()
    {
        MovingSetting();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > changeTime)
        {
            currentTime = 0;
            MovingSetting();
        }

        Move();
    }

    private void MovingSetting()
    {
        ChangeTime();
        SetDirection();
    }

    // 새가 방향을 바꾸는 시간을 변경한다.
    private void ChangeTime()
    {
        changeTime = Random.Range(1f, 2.5f);

        moveSpeed = normalSpeed;
    }

    // 이동
    private void Move()
    {
        // 새가 화면 위로 나가 거나 화면의 좌우로 나가면 안보이게 한다.
        if(transform.position.y >= 5.5f || transform.position.x < -9.5 || transform.position.x > 9.5)
        {
            gameObject.SetActive(false);
        }

        transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // 새의 이동 방향을 정한다.
    private void SetDirection()
    {
        // 랜덤 방향으로 이동하지만 아래로는 이동하지 않게 한다.
        float x = Random.Range(-1f, 1f);
        float y = Random.Range( 0.2f, 0.8f);

        direction = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 조준 되면 랜덤 확률로 빠르게 이동한다.
        if(collision.CompareTag("Player"))
        {
            int rate = Random.Range(0, 10);

            if(rate < 2)
            {
                SetDirection();
                moveSpeed = dashSpeed;
            }
        }
    }
}
