using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition}
public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    // 열거형의 string을 받기 때문에 열거형에 있는 이름과 동일하게 할 것
    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;                // 발사 간격
        int count = 30;                         // 발사 개수
        float intervalAngle = 360 / count;      // 발사 각도
        float weightAngle = 0;                  // 가중되는 각도
        
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                float angle = weightAngle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;

        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // 발사체의 이동 방향
            Vector3 directon = (targetPosition - transform.position).normalized;

            clone.GetComponent<Movement2D>().MoveTo(directon);

            yield return new WaitForSeconds(attackRate);

        }
    }
}
