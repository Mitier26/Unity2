using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GodObstacle : MonoBehaviour
{
    private IObjectPool<GodObstacle> poolManager;

    public void SetManager(IObjectPool<GodObstacle> pool)
    {
        // �ڽ��� �ҼӵǾ� �ִ� pool
        poolManager = pool;
    }
}
