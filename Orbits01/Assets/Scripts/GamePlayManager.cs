using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject scorePrefab;

    private int score;

    private void Awake()
    {
        GameManager.Instance.IsInitialzed= true;

        score = 0;
        scoreText.text = score.ToString();

        SpawnScore();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        SpawnScore();
    }

    private void SpawnScore()
    {
        Instantiate(scorePrefab);
    }

    public void GameEnded()
    {
        GameManager.Instance.CurrentScore = score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainmenu();
    }

}
