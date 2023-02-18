using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject meteoritePrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;

    private void Awake()
    {
        StartCoroutine("SpawnMeteorite");
    }

    private IEnumerator SpawnMeteorite()
    {
        while (true)
        {
            // ������ ��ġ ����
            float spawnX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            float spawnY = stageData.LimitMax.y + 1.0f;

            // ��� ����
            GameObject alertObject = Instantiate(alertLinePrefab, new Vector3(spawnX, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(1);
            // 1���� ��� ����
            Destroy(alertObject);

            // � ����
            Instantiate(meteoritePrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);

            // ���� �ð� �� �ٽ� ����
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
