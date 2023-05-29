using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProEnemyShooter : MonoBehaviour
{
    public PoolManager poolManager;
    public ProEnemyUnit unit;
    public GameObject target;

    public ProObject proObject;

    public float power;
    public float speed;

    public float delay;
    public float elapsedTime;


    private void Awake()
    {
        poolManager = ProGameManager.instance.poolManager;
        unit = transform.parent.GetComponent<ProEnemyUnit>();
        
    }

    private Coroutine shootCoroutine;

    private void OnEnable()
    {
        // 그냥 OnEnable 로 하면 범위에 나갔다 들어 오면 2중으로 작동한다.
        // 이렇게 하면 나갔다 들어 왔을 대 작동이 안될 수 도 있다.
        if (shootCoroutine == null) // Shoot 코루틴이 실행 중이지 않은 경우에만 실행
        {
            shootCoroutine = StartCoroutine(Shoot());
            elapsedTime = delay;
        }
        
    }

    private void OnDisable()
    {
        if (shootCoroutine != null) // Shoot 코루틴이 실행 중인 경우에만 중지
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    // 조준이 되면 ( 플레이어가 범위안에 들어오면 작동 )
    // OnEnable = 범위 안에 플레이어가 들어와 작동이 된것이다.

    private IEnumerator Shoot()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= delay)
            {
                target = unit.target;
                Vector3 direction = target.transform.position - transform.position;
                direction.z = 0;
                direction.Normalize();

                ProBullet bullet = poolManager.GetFromPool<ProBullet>((int)proObject);
                bullet.poolManager = poolManager;
                bullet.transform.position = transform.position;
                bullet.power = power;
                bullet.tag = "EnemyBullet";
                bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;

                elapsedTime = 0;
            }

            yield return null;
        }

    }
}
