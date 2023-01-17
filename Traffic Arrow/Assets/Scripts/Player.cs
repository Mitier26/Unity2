using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   // �÷��̾��� �ӵ�

    private Vector2 moveDirection;              // �÷��̾��� �̵� ����

    private void Awake()
    {
        // �ʱ�ȭ
        // �÷���� �̵� ������ ������ ������ ���Ѵ�.
        moveDirection = (Random.Range(-1f, 1f) * Vector2.up) + (Random.Range(-1f, 1f) * Vector2.right);
        // �밢�� �̵����� �����ϱ� ���� ��
        moveDirection = moveDirection.normalized;
    }

    private void Update()
    {
        // ���콺 �Է��� �޴´�.
        if(Input.GetMouseButton(0))
        {
            // ���콺 Ŭ���� ��ġ�� ���ӳ��� ��ǥ�� ����
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ���� ���� 3D ��ǥ�� 2D ��ǥ�� �����Ѵ�.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // ���� ��ġ���� ���콺Ŭ�� ��ġ�� ���� ������ ���Ѵ�.
            moveDirection= (mousePos2D - (Vector2)transform.position).normalized;

            // ���Ⱚ�� �̿��� ���� ���ϱ�
            float cosAngle = Mathf.Acos(moveDirection.x) * Mathf.Rad2Deg;

            // ���� ���� ������ �÷��̾� ȸ�� �Ѵ�.
            transform.rotation = Quaternion.Euler(0, 0, cosAngle * (moveDirection.y > 0f ? 1f : -1f));
        }
    }

    private void FixedUpdate()
    {
        // �÷��̾� �̵�
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
