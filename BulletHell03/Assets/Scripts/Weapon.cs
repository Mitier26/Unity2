using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;    // 총알 오브젝트
    [SerializeField]
    private float attackRate = 0.1f;

    public void StartFiring()
    {
        StartCoroutine("TryAttact");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttact");
    }

    private IEnumerator TryAttact()
    {
        while (true)
        {
            // 총알의 생성
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // 총알의 발사 간격
            yield return new WaitForSeconds(attackRate);
        }
    }
}
