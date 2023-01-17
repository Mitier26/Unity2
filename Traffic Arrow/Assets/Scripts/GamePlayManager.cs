using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GamePlayManager : MonoBehaviour
{
    #region START

    private bool hasGameFinished;               // 게임이 끝났는지 확인

    public static GamePlayManager Instance;     // 싱글톤

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            hasGameFinished = false;
            GameManager.Instance.IsInitialized = true;

            score = 0;
            currentLevel = 0;
            scoreText.text = "0";

            scoreSpeed = levelSpeed[currentLevel];

            for(int i = 0; i < 8; i++)
            {
                SpawnObsticle();
            }
        }
    }

    #endregion

    #region SCORE

    private float score;
    private float scoreSpeed;
    private int currentLevel;
    [SerializeField] private List<int> levelSpeed, levelMax;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnX, spawnY;

    private void Update()
    {
        // 게임이 끝나면 실행하지 않는다.
        if (hasGameFinished) return;

        // 경과시간에 따라 점수 증가
        score += scoreSpeed * Time.deltaTime;

        scoreText.text = ((int)score).ToString();

        // 점수가 레벨업 점수 보다 높으면
        if (score > levelMax[Mathf.Clamp(currentLevel, 0, levelMax.Count - 1)])
        {
            // 적의 소환
            SpawnObsticle();
            // 레벨의 증가
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, levelMax.Count - 1);
            scoreSpeed = levelSpeed[currentLevel];
        }
    }

    public void SpawnObsticle()
    {
        // 생성 위치에 다른 것이 있으면 생성되지 않도록 하는 것
        Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(spawnY, -spawnY), 0);
        RaycastHit2D hit = Physics2D.CircleCast(spawnPos, 1f, Vector2.zero);
        bool canSpawn = hit;

        // RayCast 를 사용했다.
        // 특정 지점을 정하고 RayCast로 정한 지점 근처를 탐색한다.
        // 근처에 아무 것도 없다면 적을 소환한다.
        // 적이 겹쳐서 소환 되는 것을 막는 것이다.

        // 주변을 검색하는 것에 RayCast를 사용한 이유는 무었인가?
        // 소환되는 지점이 GameObject가 아니다.
        // 소환되는 지점이 GameObject 이면 해당 기능을 작동하라고 명령을 보내야한다.


        while(canSpawn)
        {
            spawnPos = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(spawnY, -spawnY), 0);
            hit = Physics2D.CircleCast(spawnPos, 1f, Vector2.zero);
            canSpawn = hit;
        }

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    #endregion


    #region GAME_OVER

    [SerializeField] private AudioClip loseClip;
    public void GameEnded()
    {
        SoundManager.Instance.PlaySound(loseClip);
        hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainMenu();
    }

    #endregion
}
