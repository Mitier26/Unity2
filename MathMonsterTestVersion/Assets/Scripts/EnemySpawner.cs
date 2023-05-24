using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy EnemyPrefab;

    private void Start()
    {
        //StartCoroutine(SapwnEnemy());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Enemy enemy = Instantiate(EnemyPrefab);
            enemy.SetTarget(GameManager.Instance.player.gameObject);

            // 생성 위치 지정
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * 10f;
            enemy.transform.position = spawnPosition;
        }
    }

    private IEnumerator SapwnEnemy()
    {
        while (true)
        {
            Enemy enemy = Instantiate(EnemyPrefab);
            enemy.SetTarget(GameManager.Instance.player.gameObject);

            // 생성 위치 지정
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * 10f;
            enemy.transform.position = spawnPosition;

            yield return new WaitForSeconds(1);
        }
    }
}
