using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GodSpawner : MonoBehaviour
{
    GodGameManager gameManager;                         // �ݺ� ����� ���ϰ� ������ ����

    private IObjectPool<GodObstacle> obstaclePool;      // ������Ʈ Ǯ

    [SerializeField]
    private GodObstacle obstaclePrefab;                 // ������ ������Ʈ

    public float spawnInterval = 1;                     // ��ȯ ����
    public float obstacleSpeed = 5;                     // ��ȯ�� ���� �ӵ�

    private float elapsed = 0;                          // ��� �ð�

    private void Awake()
    {
        gameManager = GodGameManager.Instance;
        obstaclePool = new ObjectPool<GodObstacle>(CreateObstacle, GetObstacle, ReleaseObstacle, DestoryObstacle, maxSize: 2);
    }

    private void Update()
    {
        if (!gameManager.isStart) return;

        // ���ݰ� �ӵ� ����
        SetIntervalAndObstacleSpeed();

        // ��ȯ
        Spawning();
    }

    private void SetIntervalAndObstacleSpeed()
    {
        if (gameManager.Score == 0)
        {
            obstacleSpeed = 5f;
            spawnInterval = 1f;
        }
        else if (gameManager.Score >= 500)
        {
            obstacleSpeed = 20f;
            spawnInterval = 0.1f;
        }
        else
        {
            float t = (float)gameManager.Score / 500f;
            obstacleSpeed = Mathf.Lerp(5f, 20f, t);
            spawnInterval = Mathf.Lerp(1f, 0.1f, t);
        }
    }


    private void Spawning()
    {
        elapsed += Time.deltaTime;

        if (elapsed > spawnInterval)
        {
            GodObstacle obj = obstaclePool.Get();
            obj.moveSpeed = obstacleSpeed;
            elapsed = 0;
        }
    }

    private GodObstacle CreateObstacle()
    {
        GodObstacle obj = Instantiate(obstaclePrefab, transform);
        obj.gameObject.SetActive(false);
        obj.SetManager(obstaclePool);

        return obj;
    }

    private void GetObstacle(GodObstacle obstacle)
    {
        obstacle.gameObject.SetActive(true);
    }

    private void ReleaseObstacle(GodObstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
    }

    private void DestoryObstacle(GodObstacle obstacle)
    {
        Destroy(obstacle.gameObject);
    }
}
