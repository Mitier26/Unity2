using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      // ��ǲ �ý��� ���

public class PlayerMovement : MonoBehaviour
{
    private Vector2 playerInputValue;       // �Է��� �漧
    public Ghost ghost;                     //  �ܻ� ȿ��

    void Update()
    {
        MoveLogic();
    }

    // �Է��� �޴� ���̴�.
    // ����Ƽ �ν����Ϳ� �ִ� inputSystem �� ������ �ִ�.
    private void OnMove(InputValue value)
    {
        // �Է� ���� ���� ������ �´�.
        playerInputValue = value.Get<Vector2>();

        // �Է¹��� ���� 0 �� �ƴϸ� �ܻ��� �����.
        if(playerInputValue.magnitude != 0)
        {
            ghost.makeGhost = true;
        }
        else
            ghost.makeGhost = false;

    }

    private void MoveLogic()
    {
        // ������Ʈ�� ���ԵǾ� �ִ� ��
        // �÷��̾ �̵��Ѵ�.
        Vector2 temp = new Vector2(playerInputValue.x * Time.deltaTime * 5, 0);
        transform.Translate(temp);
    }
}
