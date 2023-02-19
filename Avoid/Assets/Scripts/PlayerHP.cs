using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private GameObject[] imageHP;
    private int currentHP;

    [SerializeField]
    private float invincibilityDuration;            // 무적 시간
    private bool isInvincibility = false;           // 무적 상태

    private SoundController soundController;
    private SpriteRenderer spriteRenderer;          // 무적 상태 표시

    private Color originColor;                      // 기본 색상

    private void Awake()
    {
        soundController = GetComponentInChildren<SoundController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = imageHP.Length;

        originColor = spriteRenderer.color;
    }

    public bool TakeDamage()
    {
        // TakeDamage는 바로 bool값을 반환하면서 playerController에서 플레이어가 죽었는지 판정한다.
        if (isInvincibility == true) return false;
        // 무적이면 아래 코드를 실행하지 않고 죽지 않았다는 것을 반환한다.

        // 체력이 1 보다 많은면 살 수 있다는 것이다.
        if(currentHP > 1)
        {
            soundController.PlaySound(0);
            StartCoroutine(nameof(OnInvincibility));
            currentHP--;
            imageHP[currentHP].SetActive(false);
        }
        else
        {
            return true;
        }

        return false;
    }

    private IEnumerator OnInvincibility()
    {
        isInvincibility = true;
        float current = 0;
        float percent = 0;
        float colorSpeed = 10f;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / invincibilityDuration;

            spriteRenderer.color = Color.Lerp(originColor, Color.red , Mathf.PingPong(Time.time * colorSpeed, 1));
            // Mathf.PingPong(float t, float length)
            // t 값에 따라 length 사이의 값을 반환 Sin 같은 것
            // t 값이 계속 증가 함에 따라 0 ~ 1 ~ 0 을 왕복한다.

            yield return null;
        }

        // 코루틴이 끝나면 기본 색상으로 돌아간다.
        spriteRenderer.color = originColor;
        // 무적을 종료
        isInvincibility = false;
    }

    public void RecoveryHP()
    {
        if(currentHP < imageHP.Length)
        {
            soundController.PlaySound(1);
            imageHP[currentHP].SetActive(true);
            currentHP++;
        }
    }
}
