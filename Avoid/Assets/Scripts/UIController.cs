using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [Header("Main UI")]
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private TMP_Text textMainGrade;

    [Header("Game UI")]
    [SerializeField] 
    private GameObject gamePanel;

    [SerializeField]
    private TMP_Text textScore;

    [Header("Result UI")]
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private TMP_Text textResultScore;
    [SerializeField]
    private TMP_Text textResultGrade;
    [SerializeField]
    private TMP_Text textResultTalk;
    [SerializeField]
    private TMP_Text textResultHighScore;


    [Header("Result UI Animation")]
    [SerializeField]
    private ScaleEffect effectGameOver;
    [SerializeField]
    private CountingEffect effectResultScore;
    [SerializeField]
    private FadeEffect effectResultGrade;

    private void Awake()
    {
        textMainGrade.text = PlayerPrefs.GetString("HighGrade");    
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void GameOver()
    {
        int currentScore = (int)gameController.CurrentScore;

        CalculateGradeAndTalk(currentScore);
        CalculateHighScore(currentScore);

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);

        // 게임 오버 글자 크기를 변경
        effectGameOver.Play(500, 200);

        effectResultScore.Play(0, currentScore, effectResultGrade.FadeIn);
        // 카운팅 에니메이션 종료 후 FadeIn 작동 되게 만든다.
        // Unity Action 을 이용하였다.
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToYoutube()
    {
        Application.OpenURL("주소");
    }

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
        // 소수점 자리수가 없는 실수로 표현
    }

    private void CalculateGradeAndTalk(int score)
    {
        if(score < 2000)
        {
            textResultGrade.text = "F";
            textResultTalk.text = "좀 더 \n 노력하자";
        }
        else if (score < 3000)
        {
            textResultGrade.text = "D";
            textResultTalk.text = "조금 \n 잘했습니다.";
        }
        else if (score < 4000)
        {
            textResultGrade.text = "C";
            textResultTalk.text = "많이 \n 잘했습니다.";
        }
        else if (score < 5000)
        {
            textResultGrade.text = "B";
            textResultTalk.text = "A 가 멀지 \n 않았다";
        }
        else
        {
            textResultGrade.text = "A";
            textResultTalk.text = "유니티를\n마스터하는\n그날까지!!";
        }
    }

    private void CalculateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);

            PlayerPrefs.SetString("HighGrade", textResultGrade.text);

            textResultHighScore.text = score.ToString();
        }
        else
        {
            textResultHighScore.text = highScore.ToString();
        }
    }
}
