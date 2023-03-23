using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerSpawner : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.GameObject coinPrefab, obstaclePrefab;  // ��ȯ�� ������Ʈ��
    private UnityEngine.GameObject obj;                         // ������ ������Ʈ�� ������ ����

    private int percent;                            // ������ ��������, ��ֹ��� �������� ���ϴ� ����
    private float spawnTime;                        // �ð��� ���� �������� �����ϴ� ����
    private float spawnInterval;                    // ��ȯ�� ����
    private float baseSpawnTime = 0.3f;             // �ּ� ��ȯ ����
    [Range(0f, 7f)]
    public float spawnWeight;                       // ��ȯ ����ġ
    public float objectSpeed;                       // ��ȯ�� ������Ʈ�� �ӵ�

    private void Start()
    {
        spawnTime = 0;
        baseSpawnTime = 0.3f;
        spawnWeight = 7f;
        objectSpeed = 2;
        spawnInterval = Random.Range(baseSpawnTime * spawnWeight, spawnWeight);
    }

    private void Update()
    {
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            percent = Random.Range(0, 10);

            if(percent < 2 )
            {
                obj = coinPrefab;
            }
            else
            {
                obj = obstaclePrefab;
            }

            UnityEngine.GameObject spawnedObject = Instantiate(obj, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<BeginnerObject>().moveSpeed = objectSpeed;
            spawnInterval = Random.Range(baseSpawnTime * spawnWeight, spawnWeight);
        }
        else
        {
            spawnTime += Time.deltaTime;
        }
    }

    public void LevelUp()
    {
        spawnWeight -= 0.5f;
        if(spawnWeight < baseSpawnTime)
        {
            spawnWeight = baseSpawnTime;
        }
        objectSpeed += 0.5f;
    }
}
