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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRederer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
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
        if (!isLive) return;

        Vector2 direction = target.position - rigid.position;
        Vector2 nextVec = direction.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        spriteRederer.flipX = target.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            health -= collision.GetComponent<Bullet>().damage;

            if(health > 0)
            {

            }
            else
            {
                Dead();
            }
            
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
