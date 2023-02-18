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
    private float spawnTime;            // ���� ���� �ֱ�

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // ���� ������ ��ġ ����
            float spawnX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            float spawnY = stageData.LimitMax.y + 1.0f;

            // ���� ����
            Instantiate(enemyPrefab, new Vector3(spawnX,spawnY, 0), Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
