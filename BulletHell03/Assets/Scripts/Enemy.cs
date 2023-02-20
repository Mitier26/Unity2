using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;         // 적의 공격력
    [SerializeField]
    private int scorePoint = 100;   // 적 처치시 획득하는 점수
    private PlayerController playerController;  // 플레이어 점수에 접근한기 위한 것

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private GameObject[] itemPrefabs;   // 아이템 배열, 랜덤 등장을 위해

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // 중돌하면 플레이어의 체력 감소
            collision.GetComponent<PlayerHP>().TakeDamage(damage);

            OnDie();
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        SpawnItem();

        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        int spawnItem = Random.Range(0, 100);

        if(spawnItem < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if(spawnItem < 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 30)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }

    }
}