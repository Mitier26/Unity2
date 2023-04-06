using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GodObstacle : MonoBehaviour
{
    private IObjectPool<GodObstacle> poolManager;       // ������Ʈ Ǯ

    public float moveSpeed;                             // ������ �̵� �ӵ�

    public float xPosition;                             // ��ȯ�� �� X ��ǥ
    public float yPosition = 10f;                       // ��ȯ�� �� Y ��ǥ

    public void SetManager(IObjectPool<GodObstacle> pool)
    {
        // �ڽ��� �ҼӵǾ� �ִ� pool
        poolManager = pool;
    }

    private void OnEnable()
    {
        xPosition = Random.Range(-24f, 24);
        transform.position = new Vector2 (xPosition, yPosition);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if(transform.position.y < -10)
        {
            poolManager.Release(this);
        }
    }
}
