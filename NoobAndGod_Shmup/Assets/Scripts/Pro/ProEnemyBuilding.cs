using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProEnemyBuilding : MonoBehaviour
{
    public PoolManager poolManager;

    public Slider slider;

    public float maxHp;
    public float hp;
    public float[] produceTime = { 20, 30 };
    public int[] produceGold = { 20, 45 };
    public ProObject[] produceObject = { ProObject.RedUnit_1, ProObject.RedUnit_2 };
    public float elapsedTime;

    public bool isCenter;

    private void Start()
    {
        slider.gameObject.SetActive(false);
        StartCoroutine(ProduceUnit());
        hp = maxHp;
    }

    private IEnumerator ProduceUnit()
    {
        elapsedTime = 0;
        int randUnit = Random.Range(0, produceTime.Length);

        if (isCenter && ProGameManager.instance.enemyWorkerCount >= ProGameManager.instance.enemyWorkerMaxCount)
        {
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(ProduceUnit());
        }
           

        if(!isCenter && ProGameManager.instance.enemyCount >= ProGameManager.instance.enemyMaxCount)
        {
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(ProduceUnit());
        }

        slider.gameObject.SetActive(true);

        if (ProGameManager.instance.enemyGold >= produceGold[randUnit])
        {
            ProGameManager.instance.enemyGold -= produceGold[randUnit];

            while (elapsedTime <= produceTime[randUnit])
            {
                elapsedTime += Time.deltaTime;
                slider.value = (float)elapsedTime / produceTime[randUnit];
                yield return null;
            }

            CreateUnit(randUnit);
        }

        slider.gameObject.SetActive(false);
        yield return StartCoroutine(ProduceUnit());
    }


    private void CreateUnit(int index)
    {
        if(!isCenter)
        {
            ProEnemyUnit unit;
            // ��� ������ �ǹ��� ������ ���� ������ ������ ���ΰ��� �ٸ���.
            unit = poolManager.GetFromPool<ProEnemyUnit>((int)produceObject[index]);
            unit.transform.position = transform.position;
            ProGameManager.instance.enemyCount++;
        }
        else if(isCenter)
        {
            ProEnemyWorker unit;
            unit = poolManager.GetFromPool<ProEnemyWorker>((int)produceObject[index]);
            unit.transform.position = transform.position;
            ProGameManager.instance.enemyWorkerCount++;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            if(collision.GetComponent<ProProjectile>() != null)
            {
                ProProjectile proBullet = collision.GetComponent<ProProjectile>();

                poolManager.TakeToPool<ProProjectile>(proBullet.idName, proBullet);

                hp -= proBullet.power;

                StartCoroutine(Repair());

                if (hp <= 0)
                {
                    Debug.Log("�ı�");
                }
            }
        }
    }

    private IEnumerator Repair()
    {
       while(hp != maxHp)
        {
            hp += 1;
            yield return new WaitForSeconds(3f);
        }

        yield break;
    }
}
