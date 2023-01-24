using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float xMoveSpeed = 2f;         // �̵� �ӵ�
    [SerializeField] private float xDelta = 2f;             // �¿� �̵� ���� ��
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
        // X �� �̵� �� ��� ( �⺻�� + ������)
        float x = xStartPosition + xDelta * Mathf.Sin(Time.time * xMoveSpeed);
        // Time.tiem : ������ ���۵� �� ��� �����Ǵ� �ð�
        // Mathf.Sin : Sin � �׷���
        // Mathf.Sin(Time.tiem) : 0 ~ 1 ~ 0 ~ -1 ~ 0 : -1 ~ 1 ������ ��
        // Mathf.Sin(Time.time * xMoveSpeed) : xMoveSpeed �� ŭ ������ -1 ~ 1 �� ��ȭ
        // xDelta * Mathf.Sin(Time.time * xMoveSpeed)
        // -1 ~ 1 * ��ȭ�� : -��ȭ�� ~ ��ȭ�� ���� ��ȭ
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void MoveToY()
    {
        rigid2D.AddForce(transform.up * yMoveSpeed, ForceMode2D.Impulse);
    }
}
