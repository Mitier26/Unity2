using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
        pool = new ObjectPool<ProObject>(CreateObject, GetObject, ReleaseObject, DestroyObject, maxSize: 5);
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
