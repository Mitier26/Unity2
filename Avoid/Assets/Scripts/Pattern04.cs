using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern04 : MonoBehaviour
{
    [SerializeField]
    private MovementTransform2D boss;
    [SerializeField]
    private GameObject bossProjectile;
    [SerializeField]
    private float attackRate = 1;
    [SerializeField]
    private int maxFireCount = 5;


    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boss.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        // 보스가 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));

        // 보스 좌우 이동
        StartCoroutine(nameof(MoveLeftAndRight));

        // 보스 공격
        int count = 0;

        while (count < maxFireCount)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }

        // 보스 오브젝트 비활성화
        boss.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        // 목표위치
        float bossDestivationY = 2;

        boss.gameObject.SetActive(true);

        while(true)
        {
            if(boss.transform.position.y <= bossDestivationY)
            {
                boss.MoveTo(Vector2.zero);

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator MoveLeftAndRight()
    {
        // 보스 오브젝트가 맵 바깥까지 이동하지 않도록 하는 가중치 값
        float xWeight = 3;

        // 보스가 처음 이동하는 방향
        boss.MoveTo(Vector3.right);

        while(true)
        {
            if(boss.transform.position.x <= Constants.min.x + xWeight)
            {
                boss.MoveTo(Vector3.right);
            }
            else if(boss.transform.position.x >= Constants.max.x - xWeight)
            {
                boss.MoveTo(Vector3.left);
            }

            yield return null;
        }
    }

    private void CircleFire()
    {
        int count = 30;                         // 총알의 수
        float intervalAngel = 360 / count;      // 총알의 발사 각도 차이

        for(int i = 0; i < count; i++)
        {
            // 총알 생성
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            // 총알의 각도
            float angle = intervalAngel * i;

            // 각도를 이용한 이동방향
            float x = Mathf.Cos(angle * Mathf.PI / 180f);
            float y = Mathf.Sin(angle * Mathf.PI / 180f);

            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
