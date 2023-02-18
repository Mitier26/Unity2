using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;    // �Ѿ� ������Ʈ
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
            // �Ѿ��� ����
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // �Ѿ��� �߻� ����
            yield return new WaitForSeconds(attackRate);
        }
    }
}
