using System.Collections;
using UnityEngine;
using UnityEngine.Pool;     // 유니티에서 제공하는 오브젝트 풀

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
        pool = new ObjectPool<ProObject>(CreateObject, GetObject, ReleaseObject, DestroyObject, maxSize: 20);
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
        // 오브젝트를 생성하고 부모를 지정하낟.
        ProObject obj = Instantiate(spawnPrefab, transform);
        // 오브젝트를 끈다.
        obj.gameObject.SetActive(false);
        // 오브젝트에게 어디에 소속되어 있는 풀인지 알린다.
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
