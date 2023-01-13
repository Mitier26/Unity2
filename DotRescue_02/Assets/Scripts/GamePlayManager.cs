using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private bool hasGameFinished;

    [SerializeField] private TMP_Text _scoreText;

    private float score;
    private float scoreSpeed;
    private int currentLevel;
    
    // 레벨업에 필요한 점수
    [SerializeField] private List<int> _levelSpeed, _levelMax;

    private void Awake()
    {
        // 게임이 한번이라도 플레이되면
        GameManager.Instance.IsInitialized = true;

        score = 0;
        currentLevel = 0;
        _scoreText.text = ((int)score).ToString();

        scoreSpeed = _levelSpeed[currentLevel];
    }

    private void Update()
    {
        if (hasGameFinished) return;

        score += scoreSpeed * Time.deltaTime;

        _scoreText.text = ((int)score).ToString();

        // 점수가 지금 레벨에 있는 레벨업 점수보다 크면
        if (score > _levelMax[Mathf.Clamp(currentLevel, 0, _levelMax.Count - 1)])
        {
            // 레벨을 증가
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, _levelMax.Count - 1);
            scoreSpeed = _levelSpeed[currentLevel];
        }
    }

    // Player 에서 충돌이 발생하면 작동할 것
    public void GameEnded()
    {
        hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)score;

        StartCoroutine(GameOver());
    }

    private  IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GotoMainMenu();
    }
}
