using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect[] fadeGameStart;
    [SerializeField]
    private GameObject panelGameStart;
    [SerializeField]
    private TMP_Text textGameStartBestScore;

    [Header("InGame UI")]
    [SerializeField]
    private TMP_Text textInGameScore;

    private int currentScore = 0;


    [Header("GameOver UI")]
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private TMP_Text textGameOverScore;
    [SerializeField]
    private TMP_Text textGameOverBestScore;
    [SerializeField]
    private float timeStopTime;

    

    public bool IsGameStart { get; private set; } = false;
    public bool IsGameOver { get; private set; } = false;

    private IEnumerator Start()
    {
        Time.timeScale = 1;

        // 최고 점수
        int bestScore = PlayerPrefs.GetInt("BestScore");
        textGameStartBestScore.text = bestScore.ToString();

        for ( int i = 0; i < fadeGameStart.Length; ++i )
        {
            fadeGameStart[i].FadeIn();
        }

        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameStart();

                IsGameStart = true;

                yield break;
            }

            yield return null;
        }
    }

    public void GameStart()
    {
        panelGameStart.SetActive(false);

        textInGameScore.gameObject.SetActive(true);
    }

    public void IncreaseScore(int  score = 1)
    {
        currentScore += score;

        textInGameScore.text = currentScore.ToString();
    }

    public void GameOver()
    {
        IsGameOver = true;

        textGameOverScore.text = currentScore.ToString();

        textInGameScore.gameObject.SetActive(false);

        panelGameOver.SetActive(true);

        // 최고 점수 갱신?
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if(currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);

            textGameOverBestScore.text = currentScore.ToString();
        }
        else
        {
            textGameOverBestScore.text = bestScore.ToString();
        }

        StartCoroutine("SlowAndStopTime");
    }

    private IEnumerator SlowAndStopTime()
    {
        float current = 0;
        float percent = 0;

        Time.timeScale = 0.5f;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / timeStopTime;

            yield return null;
        }

        Time.timeScale = 0f;
    }
}
