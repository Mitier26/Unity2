using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProObject : MonoBehaviour
{
    private IObjectPool<GameObject> managedPool;

    public bool isCoin;
    [SerializeField]
    private float coinLifeTime = 8f;

    private void OnEnable()
    {
        if(isCoin)
        {
            Invoke("DestroyObject", coinLifeTime);
        }
    }

    public void SetManager(IObjectPool<GameObject> pool)
    {
        managedPool = pool;
    }

    private void Update()
    {
        if(transform.position.x <= ProConstants.min.x || transform.position.x >= ProConstants.max.x
            || transform.position.y <= ProConstants.min.y || transform.position.y >= ProConstants.max.y)
        {
            CancelInvoke("DestroyObject");
            DestroyObject();
        }

    }

    public void DestroyObject()
    {
        managedPool.Release(gameObject);
    }
}
