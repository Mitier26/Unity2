using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProManager : MonoBehaviour
{
    public static ProManager instance;

    [Header("--------State--------")]
    public bool isString;

    [Header("--------Score--------")]
    [SerializeField]
    private TMP_Text scoreText;                     // 게임 실행중의 점수

    [Header("--------Result--------")]
    [SerializeField]
    private GameObject resultPanel;             // 결과창
    [SerializeField]
    private TextMeshProUGUI resultScoreText;        // 결과창 점수
    [SerializeField]
    private TextMeshProUGUI resultHighScoreText;    // 결과창 최고 점수

    public GameObject joystick;
    public GameObject jumpButton;

    [SerializeField]
    private Image fadeImage;
    float playTime = 0;
    float percent = 0;

    [SerializeField]
    private ProSpawnManager spawnManager;
    private int levelupInterval = 11;

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;

            if (score >= levelupInterval * spawnManager.level)
            {
                spawnManager.AddLevel();
            }

            scoreText.text = score.ToString();
            resultScoreText.text = score.ToString();
        }
    }

    private int highScore;
    public int HighScore
    {
        get { return PlayerPrefs.GetInt(Constants.ProfessionalSaveString); }
        set
        {
            highScore = value;
            FireBaseManager3.instance.SaveData(Constants.ProfessionalSaveString, highScore);
            resultHighScoreText.text = highScore.ToString();
        }
    }


    private void Awake()
    {
        if (instance == null) instance = this;

        if (!PlayerPrefs.HasKey(Constants.ProfessionalSaveString)) PlayerPrefs.SetInt(Constants.ProfessionalSaveString, 0);
        Score = 0;
        isString = false;
        Time.timeScale = 1f;
        Camera.main.orthographicSize = 1f;
    }

    private IEnumerator Start()
    {
        Time.timeScale = 1.0f;

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(Fade());

        yield return StartCoroutine(CameraMove());

        GameStart();
        yield break;
    }

    private IEnumerator Fade()
    {
        playTime = 0;
        percent = 0;

        while (playTime < 1f)
        {
            playTime += Time.deltaTime;
            percent = playTime / 1;
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1f, 0, percent);
            fadeImage.color = color;

            yield return null;
        }
        yield break;
    }

    private IEnumerator CameraMove()
    {
        joystick.SetActive(true);
        jumpButton.SetActive(true);

        playTime = 0;
        percent = 0;

        while (playTime < 2f)
        {
            playTime += Time.deltaTime;
            percent = playTime / 1;
            Camera.main.orthographicSize = Mathf.Lerp(1f, 7f, percent);

            yield return null;
        }
        Camera.main.orthographicSize = 7f;
        yield break;
    }

    private void GameStart()
    {
        spawnManager.gameObject.SetActive(true);
        isString = true;
        scoreText.gameObject.SetActive(true);
        StartCoroutine(AddScore());
    }

    private IEnumerator AddScore()
    {
        while (isString)
        {
            // 점수 증가 간격 마다 1점 증가
            Score++;

            yield return new WaitForSeconds(0.6f);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    public void Gameover()
    {
        isString = false;
        Time.timeScale = 0f;
        resultPanel.SetActive(true);
        if(Score > HighScore) HighScore = Score;

        joystick.SetActive(false);
        jumpButton.SetActive(false);
    }
}
