using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private GameObject astroidPrefab;
    [SerializeField] private GameObject indicator;

    [SerializeField] private int maxLevel = 40;
    [SerializeField] private int enemyCount = 1;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float elapsedTime;
    
    [SerializeField] private List<int> spawnIndex;

    [Range(1, 40)]
    public int testLevel = 1;

    [SerializeField] private float levelupInterval = 6f;

    private void Start()
    {
        // Level �� ���� �����ϴ� ���� ���� �ٸ��� �Ѵ�.
        // Level �� ���� �����ϴ� ���� �ٸ��� �Ѵ�.
        // ���� ���� �����ϴ� ���� �ٸ� ��ũ��Ʈ���� �Ѵ�. BeginManager
        spawnIndex = new List<int>();

        StartCoroutine(LevelUpTimer());
        StartCoroutine(SpawnEnemys());
    }

    private IEnumerator LevelUpTimer()
    {
        while (true)
        {
            if (enemyCount < 5)
            {
                if (testLevel % 3 == 0) enemyCount++;
            }
            spawnDelay = Mathf.Lerp(4, 1f, (float)testLevel / maxLevel);
            enemySpeed = Mathf.Lerp(1, 4f, (float)testLevel / maxLevel);
            testLevel = Mathf.Min(testLevel+1, maxLevel);
            yield return new WaitForSeconds(levelupInterval);
        }
    }

    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            SelectRandomPositions();

            // ��ȯ�Ǵ� ���� ���� ���� ���� ��ȯ�Ѵ�.
            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(spawnPrefabs[0], spawnPoints[spawnIndex[i]].position, Quaternion.identity);
            }

            // ��� ����ϰ� ���� ��ȯ�� �Ѵ�.
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // �� ������ �ƴ� �� ������ ��ġ�� �����ִ� ���� �ʿ��ϴ�.
    // ������ ���� �ƴϰ� ��ȯ���� ��ġ
    private void SelectRandomPositions()
    {
        // ����Ʈ�� �ʱ�ȭ �Ѵ�.
        spawnIndex.Clear();
        int currentNumber = Random.Range(0, spawnPoints.Length);

        for(int i = 0; i < spawnPoints.Length - 2;)
        {
            if(spawnIndex.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, spawnPoints.Length);
            }
            else
            {
                spawnIndex.Add(currentNumber);
                i++;
            }
        }
    }
}
