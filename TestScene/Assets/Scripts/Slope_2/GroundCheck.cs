using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform rayCastOrigin;         // 레이저 발사 위치
    public Transform playerFeet;            // 플레이어 발의 위치
    public LayerMask layerMask;             // 레이저가 검사 할 레이어
    private RaycastHit2D hit2D;             // 레이저와 충돌한 것의 정보

    private void Update()
    {
        GroundCheckMethhod();
    }
    private void GroundCheckMethhod()
    {
        // 레이저 발사 위치에서 아래 방향으로 100 거리 만큰 발사한다.
        hit2D = Physics2D.Raycast(rayCastOrigin.position, Vector2.down, 100f, layerMask);

        // 충돌한 것이 있다면
        if(hit2D)
        {
            // 발의 위취를 임시 저장한다.
            // 임시 저장한 위치를 충돌한 곳의 위치로 한다.
            // 발의 위치를 변경한 임시 저장 위치로 한다.
            Vector2 temp = playerFeet.position;
            temp.y = hit2D.point.y;
            playerFeet.position = temp;
        }
    }
}
