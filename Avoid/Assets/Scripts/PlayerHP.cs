using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private GameObject[] imageHP;
    private int currentHP;

    [SerializeField]
    private float invincibilityDuration;            // ���� �ð�
    private bool isInvincibility = false;           // ���� ����

    private SoundController soundController;
    private SpriteRenderer spriteRenderer;          // ���� ���� ǥ��

    private Color originColor;                      // �⺻ ����

    private void Awake()
    {
        soundController = GetComponentInChildren<SoundController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = imageHP.Length;

        originColor = spriteRenderer.color;
    }

    public bool TakeDamage()
    {
        // TakeDamage�� �ٷ� bool���� ��ȯ�ϸ鼭 playerController���� �÷��̾ �׾����� �����Ѵ�.
        if (isInvincibility == true) return false;
        // �����̸� �Ʒ� �ڵ带 �������� �ʰ� ���� �ʾҴٴ� ���� ��ȯ�Ѵ�.

        // ü���� 1 ���� ������ �� �� �ִٴ� ���̴�.
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
            // t ���� ���� length ������ ���� ��ȯ Sin ���� ��
            // t ���� ��� ���� �Կ� ���� 0 ~ 1 ~ 0 �� �պ��Ѵ�.

            yield return null;
        }

        // �ڷ�ƾ�� ������ �⺻ �������� ���ư���.
        spriteRenderer.color = originColor;
        // ������ ����
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
