using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProFly : MonoBehaviour
{
    private IObjectPool<GameObject> managedPool;

    public void SetManager(IObjectPool<GameObject> pool)
    {
        managedPool = pool;
    }

    public void DestroyObject()
    {
        managedPool.Release(gameObject);
    }
}
