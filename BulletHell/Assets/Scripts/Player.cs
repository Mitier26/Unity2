using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int currentHp;
    [SerializeField]
    private float invincibleTime;
    [SerializeField]
    private float invincibleTimer;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EBullet") && invincibleTimer < 0)
        {
            // �� �Ѿ��� ���ݷ����� ���� �ϵ��� ����
            currentHp -= 1;

            invincibleTimer = invincibleTime;
        }
    }
}
