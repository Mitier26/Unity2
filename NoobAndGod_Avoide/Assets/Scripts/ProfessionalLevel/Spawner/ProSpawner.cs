using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ProSpawner : MonoBehaviour
{
    [Header("--------Parent--------")]
    [SerializeField]
    protected ProObject spawnPrefab;             // 소환할 오브젝트
    [SerializeField]
    protected Vector2 spawnDirection;             // 소환할 방향, 생성한 오브젝트에 영향을 준다.
    [SerializeField]
    protected float spawnPower;                   // 소환할 때의 파워, 생성한 것의 이동 속도에 영향
    [SerializeField]
    protected float spawnDelay;                   // 소환 간격
    [SerializeField]
    protected Transform[] spawnRanges;            // 소환할 범위, 랜덤으로 생성될 때, 생성 범위, 자신을 기준으로 한다.

    protected IObjectPool<ProObject> pool;       // 오브젝트 풀, 유니티 제공

    private void Awake()
    {
        pool = new ObjectPool<ProObject>(CreateObject, GetObject, ReleaseObject, DestroyObject, maxSize: 5);
    }

    private void Update()
    {
        // 테스트용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObject());
    }


    protected abstract IEnumerator SpawnObject();

    private ProObject CreateObject()
    {
        ProObject obj = Instantiate(spawnPrefab, transform);

        obj.gameObject.SetActive(false);

        obj.GetComponent<ProObject>().SetManager(pool);

        return obj;
    }

    private void GetObject(ProObject obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(true);
    }

    private void ReleaseObject(ProObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void DestroyObject(ProObject obj)
    {
        Destroy(obj);
    }
}
