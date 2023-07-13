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
        // 마우스의 위치로 크로스헤어 따라 가기
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        // 재장전 하는 동안 발사 할 수 없게 만들어야 한다.
        if (Input.GetMouseButtonDown(0) && !BeginGameManager.instance.isReloading)
        {
            BeginGameManager.instance.Shooting(isIn, mousePos, target);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            // 타겟이면 색이 변한다.
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
