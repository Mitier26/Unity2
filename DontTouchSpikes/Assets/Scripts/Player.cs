using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject playerDieEffect;         // 이펙트

    [SerializeField]
    private float jumpForce = 15f;              // 점프 파위

    [SerializeField]
    private float moveSpeed = 5f;               // 이동 속도

    private Rigidbody2D rb2D;                   // 강체

    private AudioSource audioSource;

    [SerializeField]
    private PlayerTrailSpawner playerTrailSpawner;


    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();

        rb2D = GetComponent<Rigidbody2D>();

        rb2D.isKinematic = true;            // 처음에는 중력에 영향을 받지 않게 한다.

        // 시작과 동시에 플레이어가 오른쪽으로 점프한다.
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

                // 이펙트 소환
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
        // sign : 부호를 반환한다. 오른쪽으로 이동하면 +1 인데
        // 여기서는 앞에 ( - ) 가 있어 오른쪽이면 -1을 반환한다.
        // 벽에 충돌했을 때 이것을 실행하면 반대편으로 이동한다.
        rb2D.velocity = new Vector2(x * moveSpeed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            ReverseXDir();

            // 벽이랑 충돌하면 가시의 방향을 바꾸는 것
            gameController.CollisionWithWall();

            audioSource.Play();
        }
        if(collision.CompareTag("Spike"))
        {
            // 이펙트 출력
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);

            // 게임 종료
            gameController.GameOver();

            // 플레이어 안보이게
            gameObject.SetActive(false);
        }
    }
}
