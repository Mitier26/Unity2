using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03, }
public class Boss : MonoBehaviour
{
    [SerializeField]
    StageData stageData;
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;

    private BossWeapon bossWeapon;

    private BossHP bossHP;          // ü�¿� ���� ������ �ൿ�� ���ϱ�  ������ ü�� ������ �ʿ��ϴ�.

    [SerializeField]
    private GameObject bossExplosionPrefab;


    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void ChangeState(BossState newState)
    {
        // ������ ����.ToString() : �������� ���� �� string
        // �ڷ�ƾ�� �����Ͽ� �ش� �ڷ�ƾ�� ����ǵ��� �����.

        StopCoroutine(bossState.ToString());

        bossState = newState;

        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);
                // �̵��� �Ϸ�Ǹ� ���¸� �����Ѵ�.
                ChangeState(BossState.Phase01);
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);

        while(true)
        {
            // ü���� �ְ� ü���� 70���ϰ� �Ǹ�
            if(bossHP.CurrentHp <= bossHP.MaxHp * 0.7)
            {
                // ������ �����
                bossWeapon.StopFiring(AttackType.CircleFire);

                ChangeState(BossState.Phase02);
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        // �ܹ� ������ ����
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // ���������� �̵�
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while(true)
        {
            // ȭ�� ���̷� ���� ���� ��ȯ
            if(transform.position.x <= stageData.LimitMin.x ||
                transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }

            if(bossHP.CurrentHp <= bossHP.MaxHp * 0.3f)
            {
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);

                ChangeState(BossState.Phase03);
            }

            yield return null;
        }

    }

    private IEnumerator Phase03()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // ���������� �̵�
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // ȭ�� ���̷� ���� ���� ��ȯ
            if (transform.position.x <= stageData.LimitMin.x ||
                transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }
            yield return null;
        }
    }

    public void OnDie()
    {
        GameObject clone = Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);

        clone.GetComponent<BossExplosion>().SetUp(playerController, nextSceneName);

        Destroy(gameObject);
    }
}
