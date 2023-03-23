using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnerSpawner : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.GameObject coinPrefab, obstaclePrefab;  // 소환할 오브젝트들
    private UnityEngine.GameObject obj;                         // 생성한 오브젝트를 저장할 변수

    private int percent;                            // 코인을 생성할지, 장애물을 생성할지 정하는 변수
    private float spawnTime;                        // 시간에 따라 지속적을 증가하는 변수
    private float spawnInterval;                    // 소환의 간격
    private float baseSpawnTime = 0.3f;             // 최소 소환 간격
    [Range(0f, 7f)]
    public float spawnWeight;                       // 소환 가중치
    public float objectSpeed;                       // 소환한 오브젝트의 속도

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
