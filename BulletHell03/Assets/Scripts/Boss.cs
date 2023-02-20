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

    private BossHP bossHP;          // 체력에 따라 보스의 행동이 변하기  때문에 체력 정보가 필요하다.

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
        // 열거형 변수.ToString() : 열겨형에 정의 된 string
        // 코루틴에 연결하여 해당 코루틴이 실행되도록 만든다.

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
                // 이동이 완료되면 상태를 변경한다.
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
            // 체력이 최고 체력의 70이하가 되면
            if(bossHP.CurrentHp <= bossHP.MaxHp * 0.7)
            {
                // 공격을 멈춘다
                bossWeapon.StopFiring(AttackType.CircleFire);

                ChangeState(BossState.Phase02);
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        // 단발 공격의 시작
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // 오른쪽으로 이동
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while(true)
        {
            // 화면 끝이로 가면 방향 전환
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

        // 오른쪽으로 이동
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // 화면 끝이로 가면 방향 전환
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
