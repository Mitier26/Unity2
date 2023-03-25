using UnityEngine;
using UnityEngine.Pool;

public class ProObject : MonoBehaviour
{
    private IObjectPool<ProObject> managedPool;

    public void SetManager(IObjectPool<ProObject> pool)
    {
        // �ڽ��� �ҼӵǾ� �ִ� pool
        managedPool = pool;
    }
    private void Update()
    {
        // ���� ������ ��� pool�� �ǵ�����.
        if(transform.position.x <= ProConstants.min.x || transform.position.x >= ProConstants.max.x
            || transform.position.y <= ProConstants.min.y || transform.position.y >= ProConstants.max.y)
        {
            CancelInvoke("DestroyObject");
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        // pool�� ����
        managedPool.Release(this);
    }
}
