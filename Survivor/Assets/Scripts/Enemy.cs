using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRederer;
    Collider2D coll;

    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRederer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriteRederer.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 direction = target.position - rigid.position;
        Vector2 nextVec = direction.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        if (!isLive) return;

        spriteRederer.flipX = target.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") && isLive)
        {
            health -= collision.GetComponent<Bullet>().damage;

            StartCoroutine(nameof(KnockBack));

            if(health > 0)
            {
                anim.SetTrigger("Hit");
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriteRederer.sortingOrder = 1;
                anim.SetBool("Dead", true);

                GameManager.Instance.kill++;
                GameManager.Instance.GetExp();
            }
            
        }
    }

    private IEnumerator KnockBack()
    {
        yield return wait;  // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        //yield return null;  // 1프레인 쉬기
        //yield return new WaitForSeconds(2f); // 2초 쉬기
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
