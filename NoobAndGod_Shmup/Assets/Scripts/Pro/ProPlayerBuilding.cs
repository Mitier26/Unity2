using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPlayerBuilding : MonoBehaviour
{
    PoolManager poolManager;

    public float maxHp;
    public float hp;

    public int maxHpLevel;

    private void Start()
    {
        poolManager = ProGameManager.instance.poolManager;
        hp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            ProProjectile proBullet = collision.GetComponent<ProProjectile>();
            poolManager.TakeToPool<ProProjectile>(proBullet.idName, proBullet);

            hp -= collision.GetComponent<ProProjectile>().power;

            if(hp <= 0 )
            {
                // 게임 오버
                ProGameManager.instance.GameOver();
            }
        }
    }

    public void AddHp()
    {
        maxHpLevel++;
        maxHp += 100;
    }
}
