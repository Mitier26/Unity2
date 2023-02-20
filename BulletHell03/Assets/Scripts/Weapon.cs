using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;    // 총알 오브젝트
    [SerializeField]
    private float attackRate = 0.1f;

    [SerializeField]
    private int maxAttackLevel = 3;
    private int attackLevel = 1;        // 공격 레벨
    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    private AudioSource audioSource;

    [SerializeField]
    private GameObject boomPrefab;
    private int boomCount = 3;
    public int BoomCount
    {
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;
    }


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            AttackByLevel();

            audioSource.Play();

            // 총알의 발사 간격
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void AttackByLevel()
    {
        GameObject cloneProjectile = null;

        switch (attackLevel)
        {
            case 1:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.25f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.25f, Quaternion.identity);
                break;
            case 3:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));

                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }

    public void StartBoom()
    {
        if(boomCount > 0)
        {
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }
}
