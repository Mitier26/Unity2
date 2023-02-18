using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;                   // 배경이미지가 서로 타겟
    [SerializeField]
    private float scrollRange = 9.9f;           // 스크롤 할 범위
    [SerializeField]
    private float moveSpeed = 3.0f;             // 스크롤 속도
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        // 배경을 moveDirection 방향으로 이동 시킨다.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 배경의 y 위치기 작거나 같으면
        if (transform.position.y <= -scrollRange)
        {
            // 타겟의 위치에서 스크롤 범위를 더한다.
            // 스트롤범위 * 2 도 같은 듯
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
