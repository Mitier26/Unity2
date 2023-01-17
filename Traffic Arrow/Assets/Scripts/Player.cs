using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   // 플레이어의 속도

    private Vector2 moveDirection;              // 플레이어의 이동 방향

    private void Awake()
    {
        // 초기화
        // 플레어어 이동 방향을 랜덤한 값으로 정한다.
        moveDirection = (Random.Range(-1f, 1f) * Vector2.up) + (Random.Range(-1f, 1f) * Vector2.right);
        // 대각선 이동값을 조정하기 위한 것
        moveDirection = moveDirection.normalized;
    }

    private void Update()
    {
        // 마우스 입력을 받는다.
        if(Input.GetMouseButton(0))
        {
            // 마우스 클릭한 위치를 게임내의 좌표로 변경
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 위에 받은 3D 좌표를 2D 좌표로 변경한다.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // 현재 위치에서 마우스클릭 위치를 빼서 뱡향을 구한다.
            moveDirection= (mousePos2D - (Vector2)transform.position).normalized;

            // 방향값을 이용한 각도 구하기
            float cosAngle = Mathf.Acos(moveDirection.x) * Mathf.Rad2Deg;

            // 위에 구한 각도로 플레이어 회전 한다.
            transform.rotation = Quaternion.Euler(0, 0, cosAngle * (moveDirection.y > 0f ? 1f : -1f));
        }
    }

    private void FixedUpdate()
    {
        // 플레이어 이동
        transform.position += (Vector3)(moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Side"))
        {
            moveDirection.x *= -1;
        }

        if(collision.CompareTag("Top"))
        {
            moveDirection.y *= -1;
        }

        if(collision.CompareTag("Obstacle"))
        {
            GamePlayManager.Instance.GameEnded();
            Destroy(gameObject);
        }
    }
}
