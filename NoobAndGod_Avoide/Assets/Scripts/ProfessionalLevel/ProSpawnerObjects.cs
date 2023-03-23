using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProSpawnerObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPrefab;             // 소환할 오브젝트
    [SerializeField]
    private Vector2 spawnDirection;             // 소환할 방향, 생성한 오브젝트에 영향을 준다.
    [SerializeField]
    private float spawnPower;                   // 소환할 때의 파워, 생성한 것의 이동 속도에 영향
    [SerializeField]
    private float spawnRange;                   // 소환할 범위, 랜덤으로 생성될 때, 생성 범위, 자신을 기준으로 한다.
    [SerializeField]
    private float spawnDelay;                   // 소환 간격

    private IObjectPool<GameObject> pool;       // 오브젝트 풀, 유니티 제공

    [Header("Coin")]
    [SerializeField]
    private bool isCoin = false;    // 코인일 때 소환 방식이 다르고, 태그도 달라야한다.
    [SerializeField]
    private Vector2 coinDirectionX;
    [SerializeField] 
    private Vector2 coinDirectionY;
    [SerializeField]
    private float coinPower;

    [Header("Fish")]
    [SerializeField]
    private bool isFish = false;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateObject, GetObject, ReleaseObject, DestroyObject, maxSize:5);
    }

    private void Update()
    {
        // 테스트용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while(true)
        {
            GameObject obj = pool.Get();

            if (isCoin)
            {
                spawnDirection = new Vector2(Random.Range(coinDirectionX.x, coinDirectionX.y), Random.Range(coinDirectionY.x, coinDirectionY.y));
                spawnPower = Random.Range(5f, coinPower);
            }

            obj.transform.position = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0);

            if(!isFish) obj.GetComponent<Rigidbody2D>().velocity = spawnDirection * spawnPower;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(spawnPrefab, transform);

        // 생성하는 오브젝트의 태그 설정
        if (isCoin)
        {
            obj.tag = "Coin";
            obj.GetComponent<ProObject>().isCoin = this.isCoin;
        }
        else obj.tag = "Obstacle";
        
        obj.SetActive(false);

        if (isFish)
        {
            obj.GetComponent<ProFish>().SetManager(pool);
        }
        else obj.GetComponent<ProObject>().SetManager(pool);

        return obj;
    }

    private void GetObject(GameObject obj)
    {
        obj.transform.position = transform.position;
        obj.SetActive(true);
    }

    private void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
