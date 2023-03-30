using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform rayCastOrigin;         // ������ �߻� ��ġ
    public Transform playerFeet;            // �÷��̾� ���� ��ġ
    public LayerMask layerMask;             // �������� �˻� �� ���̾�
    private RaycastHit2D hit2D;             // �������� �浹�� ���� ����

    private void Update()
    {
        GroundCheckMethhod();
    }
    private void GroundCheckMethhod()
    {
        // ������ �߻� ��ġ���� �Ʒ� �������� 100 �Ÿ� ��ū �߻��Ѵ�.
        hit2D = Physics2D.Raycast(rayCastOrigin.position, Vector2.down, 100f, layerMask);

        // �浹�� ���� �ִٸ�
        if(hit2D)
        {
            // ���� ���븦 �ӽ� �����Ѵ�.
            // �ӽ� ������ ��ġ�� �浹�� ���� ��ġ�� �Ѵ�.
            // ���� ��ġ�� ������ �ӽ� ���� ��ġ�� �Ѵ�.
            Vector2 temp = playerFeet.position;
            temp.y = hit2D.point.y;
            playerFeet.position = temp;
        }
    }
}
