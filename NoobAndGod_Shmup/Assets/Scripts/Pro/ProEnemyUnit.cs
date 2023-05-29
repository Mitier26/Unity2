using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { Idle, Move, Attack, RunAway }

public class ProEnemyUnit : MonoBehaviour, IPoolObject
{
    public PoolManager poolManager;

    public STATE state = STATE.Move;

    public System.Action action;       // Unit�� ���°��� ������ �� �̵��ϴ� �Ϳ� ����� �����ϱ� ����
    public System.Action stop;

    public string idName;

    public GameObject target;
    public GameObject shooter;

    public float stayDelay;

    public float maxHp;
    public float hp;

    private void Awake()
    {
        shooter.SetActive(false);
        poolManager = ProGameManager.instance.poolManager;
        hp = maxHp;
    }


    // ���±� �̵� �϶� 
    private void SetMove()
    {
        action?.Invoke();
    }

    private void StopMove()
    {
        stop?.Invoke();
    }

    public void SetAttack(GameObject target)
    {
        this.target = target;
        shooter.SetActive(true);
        state = STATE.Attack;
        StopMove();
        StopCoroutine(Idle());
    }

    public void SetIdle()
    {
        state = STATE.Idle;
        shooter.SetActive(false);
        target = null;
        // ������ ����� �� Idle �� ����ȴ�.
        // �׷��� ������Ʈ�� ���µ� �ڷ�Ƽ�� �����Ѵٰ� ������ �����.
        if(gameObject.activeSelf)
            StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        float timer = Random.Range(0, 5f);

        yield return new WaitForSeconds(timer);

        SetMove();

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            ProProjectile proBullet = collision.GetComponent<ProProjectile>();

            poolManager.TakeToPool<ProProjectile>(proBullet.idName, proBullet);

            hp -= proBullet.power;

            if(hp <= 0)
            {
                StopCoroutine(Idle());
                poolManager.TakeToPool<ProEnemyUnit>(idName, this);
            }
        }
    }

    void IPoolObject.OnCreatedInPool()
    {
    }

    void IPoolObject.OnGettingFromPool()
    {
    }
}
