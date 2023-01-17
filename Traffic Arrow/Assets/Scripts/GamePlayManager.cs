using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GamePlayManager : MonoBehaviour
{
    #region START

    private bool hasGameFinished;               // ������ �������� Ȯ��

    public static GamePlayManager Instance;     // �̱���

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
        // ������ ������ �������� �ʴ´�.
        if (hasGameFinished) return;

        // ����ð��� ���� ���� ����
        score += scoreSpeed * Time.deltaTime;

        scoreText.text = ((int)score).ToString();

        // ������ ������ ���� ���� ������
        if (score > levelMax[Mathf.Clamp(currentLevel, 0, levelMax.Count - 1)])
        {
            // ���� ��ȯ
            SpawnObsticle();
            // ������ ����
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, levelMax.Count - 1);
            scoreSpeed = levelSpeed[currentLevel];
        }
    }

    public void SpawnObsticle()
    {
        // ���� ��ġ�� �ٸ� ���� ������ �������� �ʵ��� �ϴ� ��
        Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(spawnY, -spawnY), 0);
        RaycastHit2D hit = Physics2D.CircleCast(spawnPos, 1f, Vector2.zero);
        bool canSpawn = hit;

        // RayCast �� ����ߴ�.
        // Ư�� ������ ���ϰ� RayCast�� ���� ���� ��ó�� Ž���Ѵ�.
        // ��ó�� �ƹ� �͵� ���ٸ� ���� ��ȯ�Ѵ�.
        // ���� ���ļ� ��ȯ �Ǵ� ���� ���� ���̴�.

        // �ֺ��� �˻��ϴ� �Ϳ� RayCast�� ����� ������ �����ΰ�?
        // ��ȯ�Ǵ� ������ GameObject�� �ƴϴ�.
        // ��ȯ�Ǵ� ������ GameObject �̸� �ش� ����� �۵��϶�� ����� �������Ѵ�.


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
