using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginManager : MonoBehaviour
{

    [SerializeField] BeginEnemySpawner enemySpawner;

    [SerializeField] private int maxLevel = 80;
    [Range(1, 80)]
    public int level = 1;

    [SerializeField] private float levelupInterval = 6f;

    private int count = 0;

    private void Start()
    {
        StartCoroutine(LevelUp());
    }

    private IEnumerator LevelUp()
    {
        while (true)
        {
            if (enemySpawner.EnemyCount < 5)
            {
                if (level % 3 == 0) enemySpawner.EnemyCount++;
            }
            //enemySpawner.EnemyHp = Mathf.Lerp(3, 15, (float)level / maxLevel);
            enemySpawner.SpawnDelay = Mathf.Lerp(4, 0.5f, (float)level / maxLevel);
            enemySpawner.EnemySpeed = Mathf.Lerp(1, 5f, (float)level / maxLevel);
            
            // 레벨의 증가
            level = Mathf.Min(level + 1, maxLevel);
            yield return new WaitForSeconds(levelupInterval);

            // 테스트

            if (level % 20 == 0)
                count = 0;
            
            count++;
        }
    }

   
    public int SelectEnemy()
    {
        int index;

        float a = Mathf.Lerp(0, 1, (float)count / 20);
        float ran = Random.value;

        if (level < 20)
            index = (ran <= a) ? 1 : 0;
        else if (level < 40)
            index = (ran <= a) ? 2 : 1;
        else if (level < 60)
            index = (ran <= a) ? 3 : 2;
        else
            index = 3;

        return index;
    }
    
}
