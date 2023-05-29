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
        // �׳� OnEnable �� �ϸ� ������ ������ ��� ���� 2������ �۵��Ѵ�.
        // �̷��� �ϸ� ������ ��� ���� �� �۵��� �ȵ� �� �� �ִ�.
        if (shootCoroutine == null) // Shoot �ڷ�ƾ�� ���� ������ ���� ��쿡�� ����
        {
            shootCoroutine = StartCoroutine(Shoot());
            elapsedTime = delay;
        }
        
    }

    private void OnDisable()
    {
        if (shootCoroutine != null) // Shoot �ڷ�ƾ�� ���� ���� ��쿡�� ����
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    // ������ �Ǹ� ( �÷��̾ �����ȿ� ������ �۵� )
    // OnEnable = ���� �ȿ� �÷��̾ ���� �۵��� �Ȱ��̴�.

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
