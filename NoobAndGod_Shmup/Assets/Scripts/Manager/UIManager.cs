using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("GAMING")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TMP_Text scoreText;
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGamePanel(bool active)
    {
        gamePanel.SetActive(active);
    }

    // ȭ�鿡 ������ ǥ���Ѵ�.
    // ������ GameManager ���� ������ �´�.
    public void SetScore(float score)
    {
        scoreText.text = score.ToString();
    }

    // ȭ���� �ִ� �Ͻ����� ��ư�� ������ �� �����Ѵ�.
    // ������ �Ͻ������ϰ� �Ͻ����� �г��� ȭ�鿡 ����.
    public void PauseButton()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    // ����ϱ� ��ư�� ������ �� �����Ѵ�.
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


    // �������� ��ư�� ������ �� �����Ѵ�.
    // ���޴����� ����ϴµ�...
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
