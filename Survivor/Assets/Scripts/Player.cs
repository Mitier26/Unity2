using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float moveSpeed = 4f;

    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Scanner scanner;
    public Hand[] hands;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void Update()
    {
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void OnMove(InputValue value)
    {
        if (!GameManager.Instance.isLive) return;
        inputVec = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        // ���� �̵� ���

        // 1. ���� ���ϴ� ���
        //rigid.AddForce(inputVector);

        // 2. �ӵ� ����
        //rigid.velocity = inputVector;
        //rigid.velocity += inputVector * moveSpeed * Time.deltaTime;
        // �ӵ��� �ѹ� ������ �������� ��� �����δ�.
        // ���������� �ִ°� ����. �Ѿ� �����Ϳ� ����ϸ� ���� ���δ�.

        // 3. ��ġ �̵�
        //Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        Vector2 nextVec = inputVec * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        animator.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x !=0) spriteRenderer.flipX = inputVec.x < 0;
    }
}
