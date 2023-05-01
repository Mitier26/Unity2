using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginEnemySpawner : MonoBehaviour
{
    [SerializeField] private BeginManager manager;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private GameObject astroidPrefab;
    [SerializeField] private GameObject indicator;

    [SerializeField] private int enemyCount = 1;
    //[SerializeField] private float enemyHp = 3f;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float elapsedTime;
    
    [SerializeField] private List<int> spawnIndex;

    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    //public float EnemyHp
    //{
    //    get { return enemyHp; }
    //    set { enemyHp = value; }
    //}

    public float EnemySpeed
    {
        get { return enemySpeed; }
        set { enemySpeed = value; }
    }

    public float SpawnDelay
    {
        get { return spawnDelay; }
        set { spawnDelay = value; }
    }


    private void Start()
    {
        // Level �� ���� �����ϴ� ���� ���� �ٸ��� �Ѵ�.
        // Level �� ���� �����ϴ� ���� �ٸ��� �Ѵ�.
        // ���� ���� �����ϴ� ���� �ٸ� ��ũ��Ʈ���� �Ѵ�. BeginManager
        spawnIndex = new List<int>();

        StartCoroutine(SpawnEnemys());
    }


    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            SelectRandomPositions();

            // ��ȯ�Ǵ� ���� ���� ���� ���� ��ȯ�Ѵ�.
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemyObject = Instantiate(spawnPrefabs[manager.SelectEnemy()], spawnPoints[spawnIndex[i]].position, Quaternion.identity);

                enemyObject.GetComponent<BeginEnemyMovement>().SetMoveSpeed(enemySpeed);
                //enemyObject.GetComponent<BeginEnemy>().HP = enemyHp;
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
