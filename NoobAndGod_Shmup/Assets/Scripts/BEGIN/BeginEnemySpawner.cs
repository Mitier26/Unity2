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
        // Level 에 따라 등장하는 적의 수를 다르게 한다.
        // Level 에 따라 등장하는 적을 다르게 한다.
        // 적의 수를 관리하는 것은 다른 스크립트에서 한다. BeginManager
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

            // 소환되는 적의 수에 따라 적을 소환한다.
            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(spawnPrefabs[0], spawnPoints[spawnIndex[i]].position, Quaternion.identity);
            }

            // 잠시 대기하고 다음 소환을 한다.
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // 한 마리가 아닐 때 랜덤한 위치를 정해주는 것이 필요하다.
    // 벡터의 값이 아니고 소환기의 위치
    private void SelectRandomPositions()
    {
        // 리스트를 초기화 한다.
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
