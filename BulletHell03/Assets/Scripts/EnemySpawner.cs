using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;            // 利狼 积己 林扁

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // 利捞 积己瞪 困摹 汲沥
            float spawnX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            float spawnY = stageData.LimitMax.y + 1.0f;

            // 利狼 积己
            Instantiate(enemyPrefab, new Vector3(spawnX,spawnY, 0), Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
