using UnityEngine;
using UnityEngine.Pool;

public class ProObject : MonoBehaviour
{
    private IObjectPool<ProObject> managedPool;

    public void SetManager(IObjectPool<ProObject> pool)
    {
        // 자신의 소속되어 있는 pool
        managedPool = pool;
    }
    private void Update()
    {
        // 일정 범위를 벗어만 pool로 되돌린다.
        if(transform.position.x <= ProConstants.min.x || transform.position.x >= ProConstants.max.x
            || transform.position.y <= ProConstants.min.y || transform.position.y >= ProConstants.max.y)
        {
            CancelInvoke("DestroyObject");
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        // pool로 복귀
        managedPool.Release(this);
    }
}
