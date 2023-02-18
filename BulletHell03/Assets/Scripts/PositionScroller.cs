using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;                   // ����̹����� ���� Ÿ��
    [SerializeField]
    private float scrollRange = 9.9f;           // ��ũ�� �� ����
    [SerializeField]
    private float moveSpeed = 3.0f;             // ��ũ�� �ӵ�
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        // ����� moveDirection �������� �̵� ��Ų��.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ����� y ��ġ�� �۰ų� ������
        if (transform.position.y <= -scrollRange)
        {
            // Ÿ���� ��ġ���� ��ũ�� ������ ���Ѵ�.
            // ��Ʈ�ѹ��� * 2 �� ���� ��
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
