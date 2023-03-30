using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      // 인풋 시스템 사용

public class PlayerMovement : MonoBehaviour
{
    private Vector2 playerInputValue;       // 입력의 방샹
    public Ghost ghost;                     //  잔상 효과

    void Update()
    {
        MoveLogic();
    }

    // 입력을 받는 것이다.
    // 유니티 인스펙터에 있는 inputSystem 과 관련이 있다.
    private void OnMove(InputValue value)
    {
        // 입력 받은 값을 가지고 온다.
        playerInputValue = value.Get<Vector2>();

        // 입력받은 값이 0 이 아니면 잔상을 만든다.
        if(playerInputValue.magnitude != 0)
        {
            ghost.makeGhost = true;
        }
        else
            ghost.makeGhost = false;

    }

    private void MoveLogic()
    {
        // 업데이트에 포함되어 있는 것
        // 플레이어를 이동한다.
        Vector2 temp = new Vector2(playerInputValue.x * Time.deltaTime * 5, 0);
        transform.Translate(temp);
    }
}
