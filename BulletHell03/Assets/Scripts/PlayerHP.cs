using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    /// <summary>
    /// 여기는 플에어의 체력만 관리하고 있다.
    /// 여러 곳에서 범용적으로 사용하기 위해서는
    /// 데이터와 UI를 분리 하는 것이 좋다.
    /// </summary>
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MaxHP => maxHP;
    
    public float CurrentHP                  // 프로퍼티로 만들어 get만 가능하게 한다.
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if( currentHP <= 0)
        {
            playerController.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
