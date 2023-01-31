using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;

    [Header("InGame")]
    [SerializeField]
    private GameObject inGamePanel;
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("GameOver")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI textHighScore;

    public void GameStart()
    {
        mainPanel.SetActive(false);
        inGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if(score < 10)
        {
            textScore.text = score.ToString("D2");
        }
        else
        {
            textScore.text = score.ToString();
        }

    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        textHighScore.text = $"HIGH SCORE : { PlayerPrefs.GetInt("HighScore")}";
    }
}
