using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCrossHair : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isIn = false;

    private GameObject target;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        // ���콺�� ��ġ�� ũ�ν���� ���� ����
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        // ������ �ϴ� ���� �߻� �� �� ���� ������ �Ѵ�.
        if (Input.GetMouseButtonDown(0) && !BeginGameManager.instance.isReloading)
        {
            BeginGameManager.instance.Shooting(isIn, mousePos, target);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            // Ÿ���̸� ���� ���Ѵ�.
            spriteRenderer.color = Color.red;
            target = collision.gameObject;
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;
        target = null;
        isIn = false;
    }
}
