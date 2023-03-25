using System.Collections;
using UnityEngine;
using UnityEngine.Pool;     // ����Ƽ���� �����ϴ� ������Ʈ Ǯ

public abstract class ProSpawner : MonoBehaviour
{
    [Header("--------Parent--------")]
    [SerializeField]
    protected ProObject spawnPrefab;             // ��ȯ�� ������Ʈ
    [SerializeField]
    protected Vector2 spawnDirection;             // ��ȯ�� ����, ������ ������Ʈ�� ������ �ش�.
    [SerializeField]
    protected float spawnPower;                   // ��ȯ�� ���� �Ŀ�, ������ ���� �̵� �ӵ��� ����
    [SerializeField]
    protected float spawnDelay;                   // ��ȯ ����
    [SerializeField]
    protected Transform[] spawnRanges;            // ��ȯ�� ����, �������� ������ ��, ���� ����, �ڽ��� �������� �Ѵ�.

    protected IObjectPool<ProObject> pool;       // ������Ʈ Ǯ, ����Ƽ ����

    private void Awake()
    {
        pool = new ObjectPool<ProObject>(CreateObject, GetObject, ReleaseObject, DestroyObject, maxSize: 20);
    }

    private void Update()
    {
        // �׽�Ʈ��
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
        // ������Ʈ�� �����ϰ� �θ� �����ϳ�.
        ProObject obj = Instantiate(spawnPrefab, transform);
        // ������Ʈ�� ����.
        obj.gameObject.SetActive(false);
        // ������Ʈ���� ��� �ҼӵǾ� �ִ� Ǯ���� �˸���.
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
