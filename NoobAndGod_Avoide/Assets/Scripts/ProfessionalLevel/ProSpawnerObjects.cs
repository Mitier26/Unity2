using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProSpawnerObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPrefab;             // ��ȯ�� ������Ʈ
    [SerializeField]
    private Vector2 spawnDirection;             // ��ȯ�� ����, ������ ������Ʈ�� ������ �ش�.
    [SerializeField]
    private float spawnPower;                   // ��ȯ�� ���� �Ŀ�, ������ ���� �̵� �ӵ��� ����
    [SerializeField]
    private float spawnRange;                   // ��ȯ�� ����, �������� ������ ��, ���� ����, �ڽ��� �������� �Ѵ�.
    [SerializeField]
    private float spawnDelay;                   // ��ȯ ����

    private IObjectPool<GameObject> pool;       // ������Ʈ Ǯ, ����Ƽ ����

    [Header("Coin")]
    [SerializeField]
    private bool isCoin = false;    // ������ �� ��ȯ ����� �ٸ���, �±׵� �޶���Ѵ�.
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
        // �׽�Ʈ��
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

        // �����ϴ� ������Ʈ�� �±� ����
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
