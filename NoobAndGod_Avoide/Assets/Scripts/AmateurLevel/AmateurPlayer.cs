using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurPlayer : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;            // Rigidbody�� ����ϱ� ����
    private Animator animator;                  // Amimation ��� �ϱ� ����

    [SerializeField]
    private float movespeed;                    // �̵� �ӵ�
    private float moveX;                        // �̵� ����
    [SerializeField]
    private float border = 2.5f;                // �׵θ�

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!AmateurManager.instance.isPlay) return;

        moveX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("moveX", Mathf.Abs(moveX));
    }


    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector3(moveX * movespeed, 0, 0);
    }

    private void LateUpdate()
    {
        if (moveX > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (moveX < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);

        if (transform.position.x < -border) transform.position = new Vector3(-border, transform.position.y, 0);
        else if (transform.position.x > border) transform.position = new Vector3(border, transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            AmateurManager.instance.AddScore(7);
            AudioManager.instance.PlaySoundEffect(AudioManager.SFX.Coin);
        }
        if(collision.CompareTag("Obstacle"))
        {
            AmateurManager.instance.GameOver();
            AudioManager.instance.PlaySoundEffect(AudioManager.SFX.GameOver);
            gameObject.SetActive(false);
        }

        collision.gameObject.SetActive(false);
    }
}
