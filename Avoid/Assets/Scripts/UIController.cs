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

        // ���� ���� ���� ũ�⸦ ����
        effectGameOver.Play(500, 200);

        effectResultScore.Play(0, currentScore, effectResultGrade.FadeIn);
        // ī���� ���ϸ��̼� ���� �� FadeIn �۵� �ǰ� �����.
        // Unity Action �� �̿��Ͽ���.
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToYoutube()
    {
        Application.OpenURL("�ּ�");
    }

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
        // �Ҽ��� �ڸ����� ���� �Ǽ��� ǥ��
    }

    private void CalculateGradeAndTalk(int score)
    {
        if(score < 2000)
        {
            textResultGrade.text = "F";
            textResultTalk.text = "�� �� \n �������";
        }
        else if (score < 3000)
        {
            textResultGrade.text = "D";
            textResultTalk.text = "���� \n ���߽��ϴ�.";
        }
        else if (score < 4000)
        {
            textResultGrade.text = "C";
            textResultTalk.text = "���� \n ���߽��ϴ�.";
        }
        else if (score < 5000)
        {
            textResultGrade.text = "B";
            textResultTalk.text = "A �� ���� \n �ʾҴ�";
        }
        else
        {
            textResultGrade.text = "A";
            textResultTalk.text = "����Ƽ��\n�������ϴ�\n�׳�����!!";
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
