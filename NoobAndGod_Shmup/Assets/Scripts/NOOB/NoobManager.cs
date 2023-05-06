using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject enemyGroup;
    [SerializeField] private int enemyCount;
    public float level;
    private float decreseSecond = 0.9f;

    // �⺻ ���� = (�ִ밪 - �ּҰ�) / (�ܰ�� -1)
    // �� ��ġ���� �⺻ ������ ����.
    private void Start()
    {
        Time.timeScale = 1f;
        enemyCount = enemyGroup.transform.childCount;

        StartCoroutine(DecreaseScore());
    }

    private IEnumerator DecreaseScore()
    {
        yield return new WaitForSeconds(decreseSecond);
        GameManager.instance.SetScore(-0.9f);
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
