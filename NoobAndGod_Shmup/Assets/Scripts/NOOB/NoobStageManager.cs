using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobStageManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyGroup;
    [SerializeField] private int enemyCount;
    public float level;

    // �⺻ ���� = (�ִ밪 - �ּҰ�) / (�ܰ�� -1)
    // �� ��ġ���� �⺻ ������ ����.
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
