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
        StartCoroutine(CheckCommand());
        hp = maxHp;
    }

    private IEnumerator CheckCommand()
    {
        yield return new WaitForSeconds(1f);

        if (isCenter && ProGameManager.instance.enemyWorkerCount < ProGameManager.instance.enemyWorkerMaxCount)
        {
            yield return StartCoroutine(ProductUnit());
        }
        else if(!isCenter && ProGameManager.instance.enemyCount < ProGameManager.instance.enemyMaxCount)
        {
            yield return StartCoroutine(ProductUnit());
        }
        else
        {
            yield return StartCoroutine(CheckCommand());
        }
       
    }

    private IEnumerator ProductUnit()
    {
        elapsedTime = 0;
        int randUnit = Random.Range(0, produceObject.Length);

        if (ProGameManager.instance.enemyGold >= produceGold[randUnit])
        {
            slider.gameObject.SetActive(true);

            ProGameManager.instance.enemyGold -= produceGold[randUnit];

            while (elapsedTime <= produceTime[randUnit])
            {
                elapsedTime += Time.deltaTime;
                slider.value = (float)elapsedTime / produceTime[randUnit];
                yield return null;
            }

            CreateUnit(randUnit);
        }
        else
        {
            yield return StartCoroutine(CheckCommand());
        }

        slider.gameObject.SetActive(false);
        yield return StartCoroutine(CheckCommand());
    }


    private void CreateUnit(int index)
    {
        if(!isCenter)
        {
            ProEnemyUnit unit;
            // 모든 적들의 건물은 생성는 같고 무엇을 생성할 것인가만 다르다.
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
                    Debug.Log("파괴");
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
