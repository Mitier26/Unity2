using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobStageManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyGroup;
    [SerializeField] private int enemyCount;
    public float level;

    // 기본 간격 = (최대값 - 최소값) / (단계수 -1)
    // 각 수치에서 기본 간격을 뺀다.
    private void Start()
    {
        enemyCount = enemyGroup.transform.childCount;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;

        if (enemyCount % 4 == 0) level++;

        if(enemyCount <= 0)
        {
            UIManager.instance.GameOver();
        }
        
    }
}
