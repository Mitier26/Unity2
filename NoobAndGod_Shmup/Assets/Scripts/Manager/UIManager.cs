using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("GAMING")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Gradient gradient;
    [Header("PAUSE")]
    [SerializeField] private GameObject pausePanel;
    [Header("GAMEOVER")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI resultScoreText, resultHighScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        SetScore(0);        // 점수를 초기화 한다.
        SetSlider(1);       // 슬라이더를 초기화 한다.
    }

    public void SetGamePanel(bool active)
    {
        gamePanel.SetActive(active);
    }

    // 화면에 점수를 표시한다.
    // 점수는 GameManager 에서 가지고 온다.
    public void SetScore(float score)
    {
        scoreText.text = score.ToString();
    }

    public void SetSlider(float value)
    {
        // 슬라이더의 색을 값에 따라 변경한다.
        hpSlider.fillRect.GetComponent<Image>().color = gradient.Evaluate(value);
        // 슬라이더 바의 량을 변경한다.
        hpSlider.value = value;
    }

    // 화면의 있는 일시정지 버튼을 눌렀을 때 실행한다.
    // 게임을 일시정지하고 일시정지 패널을 화면에 띄운다.
    public void PauseButton()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    // 계속하기 버튼을 눌렀을 때 실행한다.
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void GameOver()
    {
        //resultScoreText.text = GameManager.instance.
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        GameManager.instance.isPlay = false;

        SetResult(GameManager.instance.GetScore(), GameManager.instance.GetHighScore());
    }

    public void SetResult(float currentScore, float highScore)
    {
        resultScoreText.text = currentScore.ToString();
        resultHighScoreText.text = highScore.ToString();
    }


    // 스테이지 버튼을 눌렀을 때 실행한다.
    // 씬메니져를 사용하는데...
    public void StageButton()
    {
        ResumeButton();
        SceneManager.LoadScene(SceneName.STAGE.ToString());
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
