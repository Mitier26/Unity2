using UnityEngine;
using UnityEngine.Pool;

public class ProObject : MonoBehaviour
{
    private IObjectPool<ProObject> managedPool;

    
    public void SetManager(IObjectPool<ProObject> pool)
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
        managedPool.Release(this);
    }
}
