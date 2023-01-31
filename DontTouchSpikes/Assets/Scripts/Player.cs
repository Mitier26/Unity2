using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject playerDieEffect;         // ����Ʈ

    [SerializeField]
    private float jumpForce = 15f;              // ���� ����

    [SerializeField]
    private float moveSpeed = 5f;               // �̵� �ӵ�

    private Rigidbody2D rb2D;                   // ��ü

    private AudioSource audioSource;

    [SerializeField]
    private PlayerTrailSpawner playerTrailSpawner;


    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();

        rb2D = GetComponent<Rigidbody2D>();

        rb2D.isKinematic = true;            // ó������ �߷¿� ������ ���� �ʰ� �Ѵ�.

        // ���۰� ���ÿ� �÷��̾ ���������� �����Ѵ�.
        //rb2D.velocity = new Vector2(moveSpeed, jumpForce);

        //StartCoroutine(nameof(UpdateInput));
    }

    private IEnumerator Start()
    {
        float originY = transform.position.y;
        float deltaY = 0.5f;
        float moveSpeedY = 2f;

        while(true)
        {
            float y = originY + deltaY * Mathf.Sin(Time.time * moveSpeedY);
            transform.position = new Vector2( transform.position.x, y );

            yield return null;
        }
    }

    public void GameStart()
    {
        rb2D.isKinematic = false;
        rb2D.velocity = new Vector2(moveSpeed, jumpForce);

        StopCoroutine(nameof(Start));
        StartCoroutine(nameof(UpdateInput));
    }

    private IEnumerator UpdateInput()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                JumpTo();

                // ����Ʈ ��ȯ
                playerTrailSpawner.OnSpawns();
            }

            yield return null;
        }
    }

    private void JumpTo()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }

    private void ReverseXDir()
    {
        float x = -Mathf.Sign(rb2D.velocity.x);
        // sign : ��ȣ�� ��ȯ�Ѵ�. ���������� �̵��ϸ� +1 �ε�
        // ���⼭�� �տ� ( - ) �� �־� �������̸� -1�� ��ȯ�Ѵ�.
        // ���� �浹���� �� �̰��� �����ϸ� �ݴ������� �̵��Ѵ�.
        rb2D.velocity = new Vector2(x * moveSpeed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            ReverseXDir();

            // ���̶� �浹�ϸ� ������ ������ �ٲٴ� ��
            gameController.CollisionWithWall();

            audioSource.Play();
        }
        if(collision.CompareTag("Spike"))
        {
            // ����Ʈ ���
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);

            // ���� ����
            gameController.GameOver();

            // �÷��̾� �Ⱥ��̰�
            gameObject.SetActive(false);
        }
    }
}
