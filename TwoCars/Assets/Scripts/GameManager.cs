using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject startButton, InputButton, endPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText, highScoreText, highScoreEndText;

    public GameObject[] blueGo, orangeGo;
    public float startWait, spawnWait;

    private float[] xPosition = new float[2] { 1.27f, 4f };
    private bool gameover;

    int score, highScore;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);

        startButton.SetActive(true);
        InputButton.SetActive(false);
        endPanel.SetActive(false);

        Time.timeScale = 1;
        gameover = false;
        score = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        scoreText.text = score.ToString();
        highScoreText.text = "HighScore : " + highScore;
    }

    public void StartButton()
    {
        startButton.SetActive(false);
        InputButton.SetActive(true);
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameover)
        {
            float xpos = xPosition[Random.Range(0, xPosition.Length)];
            Vector3 tempPos = new Vector3(xpos, 10f, 0);
            GameObject tempObj = orangeGo[Random.Range(0, orangeGo.Length)];
            Instantiate(tempObj, tempPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);

            xpos = -xPosition[Random.Range(0, xPosition.Length)];
            tempPos = new Vector3(xpos, 10f, 0);
            tempObj = blueGo[Random.Range(0, blueGo.Length)];
            Instantiate(tempObj, tempPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
#endif
        Application.Quit();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        gameover = true;
        InputButton.SetActive(false);
        endPanel.SetActive(true);
        Time.timeScale = 0;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreEndText.text = "HighScore : " + highScore;
    }
}
