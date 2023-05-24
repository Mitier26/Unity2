using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProEnemyBuilding : MonoBehaviour
{
    public PoolManager poolManager;

    public Slider slider;

    public float[] produceTime = { 20, 30 };
    public int[] produceGold = { 20, 45 };
    public ProObject[] produceObject = { ProObject.RedUnit_1, ProObject.RedUnit_2 };
    public float elapsedTime;

    private void Start()
    {
        StartCoroutine(ProduceUnit());
    }

    private IEnumerator ProduceUnit()
    {
        elapsedTime = 0;
        int randUnit = Random.Range(0, produceTime.Length);

        if (ProGameManager.instance.enemyGold >= produceGold[randUnit])
        {
            while (elapsedTime <= produceTime[randUnit])
            {
                elapsedTime += Time.deltaTime;
                slider.value = (float)elapsedTime / produceTime[randUnit];
                yield return null;
            }

            CreateUnit(randUnit);
        }

        yield return StartCoroutine(ProduceUnit());
    }


    private void CreateUnit(int index)
    {
        ProEnemyUnit unit;
        // 모든 적들의 건물은 생성는 같고 무엇을 생성할 것인가만 다르다.
        unit = poolManager.GetFromPool<ProEnemyUnit>((int)produceObject[index]);

        unit.transform.position = transform.position;
    }
}
