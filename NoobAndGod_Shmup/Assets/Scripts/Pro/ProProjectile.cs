using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProProjectile : MonoBehaviour, IPoolObject
{
    public PoolManager poolManager;
    public Rigidbody2D rb;

    public string idName;
    public float power;
    public float activeTime = 2;

    void IPoolObject.OnCreatedInPool()
    {
    }

    void IPoolObject.OnGettingFromPool()
    {
        StartCoroutine(TakePool());
    }

    private IEnumerator TakePool()
    {
        yield return new WaitForSeconds(activeTime);
        poolManager.TakeToPool<ProProjectile>(idName, this);
        yield break;
    }
}
