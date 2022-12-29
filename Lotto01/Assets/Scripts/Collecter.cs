using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecter : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public CollecterController controller;

    private void Start()
    {
        controller = FindObjectOfType<CollecterController>();
    }
    private void OnEnable()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            boxCollider.enabled = false;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            collision.gameObject.tag = "Selected";
            collision.transform.SetParent(transform);
            collision.transform.localPosition = Vector2.zero;
            // 공이 있다는 것을 판정하면 다음 상태로 변경
            // 공이 중앙으로 이동 하면 다음 상태로 변경 ( 나중에 추가 )
            controller.SetSwitch(State.CIRCLE_MOVE);
        }
    }
}
