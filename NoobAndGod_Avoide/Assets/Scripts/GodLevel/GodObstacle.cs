using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GodObstacle : MonoBehaviour
{
    private IObjectPool<GodObstacle> poolManager;       // 오브젝트 풀

    public float moveSpeed;                             // 상자의 이동 속도

    public float xPosition;                             // 소환될 때 X 좌표
    public float yPosition = 10f;                       // 소환될 때 Y 좌표

    public void SetManager(IObjectPool<GodObstacle> pool)
    {
        // 자신의 소속되어 있는 pool
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
