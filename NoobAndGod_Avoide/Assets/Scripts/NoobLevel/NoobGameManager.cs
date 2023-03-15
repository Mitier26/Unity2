using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoobGameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private TextMeshProUGUI resultScoreText;

    [SerializeField]
    private NoobObstacle[] noobObstacles;

    [SerializeField]
    private GameObject gameoverPanel;

    [SerializeField]
    private Color[] colors;

    private int score = 1;
    private float scoreUpCount;
    private float scoreDelay = 1;
    private int activeCount = 0;

    private float bgCount;
    private float bgInterval = 30;
    private float bgLastCount;

    public bool isLive = true;

    private void Start()
    {
        Time.timeScale = 1.0f;
        noobObstacles[activeCount].gameObject.SetActive(true);
        ChangeBackgroundColor();

        if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void Update()
    {
        scoreUpCount += Time.deltaTime;
        bgCount += Time.deltaTime;

        if(scoreUpCount > scoreDelay)
        {
            AddScore(1);
            scoreUpCount = 0;
        }

        if(score % 15 == 0 && score <= 45)
        {
            score++;
            activeCount++;
            noobObstacles[activeCount].gameObject.SetActive(true);
        }

        if(score % 23 == 0)
        {
            score++;
            AddSpeed();
        }

        if(bgCount - bgLastCount >= bgInterval)
        {
            bgLastCount = bgCount;
            ChangeBackgroundColor();
        }
    }

    private void ChangeBackgroundColor()
    {
        Camera.main.backgroundColor = colors[Random.Range(0, colors.Length)];
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    private void AddSpeed()
    {
        scoreDelay *= 0.9f;

        for(int i = 0; i < noobObstacles.Length; i++)
        {
            noobObstacles[i].GetComponent<NoobObstacle>().moveDelay *= 0.9f;
        }
    }

    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }

        resultScoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();

        isLive = false;
        Time.timeScale = 0;
        gameoverPanel.SetActive(true);
        for (int i = 0; i < noobObstacles.Length; i++)
        {
            noobObstacles[i].gameObject.SetActive(false);
        }
    }
}
