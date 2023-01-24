using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float xMoveSpeed = 2f;         // 이동 속도
    [SerializeField] private float xDelta = 2f;             // 좌우 이동 변동 값
    private float xStartPosition;

    [Header("Vetical Movement")]
    [SerializeField] private float yMoveSpeed = 0.5f;
    private Rigidbody2D rigid2D;


    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        xStartPosition = transform.position.x;
    }

    public void MoveToX()
    {
        // X 축 이동 값 계산 ( 기본값 + 변동값)
        float x = xStartPosition + xDelta * Mathf.Sin(Time.time * xMoveSpeed);
        // Time.tiem : 게임이 시작된 후 계속 증가되는 시간
        // Mathf.Sin : Sin 곡선 그래프
        // Mathf.Sin(Time.tiem) : 0 ~ 1 ~ 0 ~ -1 ~ 0 : -1 ~ 1 사이의 값
        // Mathf.Sin(Time.time * xMoveSpeed) : xMoveSpeed 만 큼 빠르게 -1 ~ 1 로 변화
        // xDelta * Mathf.Sin(Time.time * xMoveSpeed)
        // -1 ~ 1 * 변화량 : -변화량 ~ 변화량 으로 변화
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void MoveToY()
    {
        rigid2D.AddForce(transform.up * yMoveSpeed, ForceMode2D.Impulse);
    }
}
