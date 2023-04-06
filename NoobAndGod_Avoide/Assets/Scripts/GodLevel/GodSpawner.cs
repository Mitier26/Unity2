using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GodSpawner : MonoBehaviour
{
    private IObjectPool<GodObstacle> obstaclePool;

    [SerializeField]
    private GodObstacle obstaclePrefab;

    private void Awake()
    {
        obstaclePool = new ObjectPool<GodObstacle>(CreateObstacle, GetObstacle, ReleaseObstacle, DestoryObstacle, maxSize:2);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            obstaclePool.Get();
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
