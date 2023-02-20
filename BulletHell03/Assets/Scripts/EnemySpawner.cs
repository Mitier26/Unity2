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
    private float spawnTime;            // 적의 생성 주기

    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;  // UI를 표시하는 캔버스

    [SerializeField]
    private int MaxEnemyCount = 100;    // 등장하는 적의 수

    [SerializeField]
    private BGMController bgmController;

    [SerializeField]
    private GameObject textBossWarning; // 보스 등장 전에 출력되는 글
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject panelBossHP;     // 보스의 체력 표시

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
            // 적이 생성될 위치 설정
            float spawnX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            float spawnY = stageData.LimitMax.y + 1.0f;

            // 적의 생성
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(spawnX,spawnY, 0), Quaternion.identity);

            // 적 체력 UI 생성
            SpawnEnemyHPSlider(enemyClone);

            // 적의 생성 수 증가
            currentEnmeyCount++;

            if(currentEnmeyCount >= MaxEnemyCount)
            {
                // 보스 소환
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
        // 슬라이더 바의 위치 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss);

        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        textBossWarning.SetActive(false);

        // 보스를 보이게한다.
        boss.SetActive(true);
        panelBossHP.SetActive(true);
        // 보스의 첫번째 상태를 지정한다.
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
