using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;            // ���� ���� �ֱ�

    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;  // UI�� ǥ���ϴ� ĵ����

    [SerializeField]
    private int MaxEnemyCount = 100;    // �����ϴ� ���� ��

    [SerializeField]
    private BGMController bgmController;

    [SerializeField]
    private GameObject textBossWarning; // ���� ���� ���� ��µǴ� ��
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject panelBossHP;     // ������ ü�� ǥ��

    private void Awake()
    {
        textBossWarning.SetActive(false);

        boss.SetActive(false);
        panelBossHP.SetActive(false);

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnmeyCount = 0;

        while (true)
        {
            // ���� ������ ��ġ ����
            float spawnX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            float spawnY = stageData.LimitMax.y + 1.0f;

            // ���� ����
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(spawnX,spawnY, 0), Quaternion.identity);

            // �� ü�� UI ����
            SpawnEnemyHPSlider(enemyClone);

            // ���� ���� �� ����
            currentEnmeyCount++;

            if(currentEnmeyCount >= MaxEnemyCount)
            {
                // ���� ��ȯ
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab, canvasTransform);


        //sliderClone.transform.SetParent(canvasTransform);

        sliderClone.transform.localScale = Vector3.one;
        // �����̴� ���� ��ġ ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss);

        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        textBossWarning.SetActive(false);

        // ������ ���̰��Ѵ�.
        boss.SetActive(true);
        panelBossHP.SetActive(true);
        // ������ ù��° ���¸� �����Ѵ�.
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
