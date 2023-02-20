using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 1000;
    private float currentHp;
    private SpriteRenderer spriteRenderer;
    private Boss boss;

    public float MaxHp => maxHp;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        StopCoroutine("HitColorAnimation");

        StartCoroutine("HitColorAnimation");

        if(currentHp <= 0)
        {
            boss.OnDie();   
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
