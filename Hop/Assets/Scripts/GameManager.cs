using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject startButton, scoreButton, restartButton, player, ball, blockPrefab, shadowPrefab;

    [SerializeField]
    private TMP_Text scoreText, diamondText, highScoreText, highScoreEndText;

    [SerializeField]
    private Vector3 startPos, offset;

    private int score, diamond, highScore, combo;

    public delegate void SetComboAnimation(bool isCombo);
    public event SetComboAnimation updateComboAnimation;

    private bool isCombo;
    public bool IsCombo
    {
        get { return isCombo; }
        set
        {
            isCombo = value;
            updateComboAnimation?.Invoke(isCombo);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        startButton.SetActive(true);
        scoreButton.SetActive(false);
        restartButton.SetActive(false);

        score = 0;
        combo = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        diamond = PlayerPrefs.HasKey("Diamond") ? PlayerPrefs.GetInt("Diamond") : 0;

        scoreText.text = score.ToString();
        diamondText.text = diamond.ToString();
        highScoreText.text = "Best : " + highScore.ToString();

        for (int i = 0; i < 4; i++)
        {
            SpawnBlock();
        }

        IsCombo = false;
    }

    public void SpawnBlock()
    {
        startPos += offset;
        GameObject tempBlock = Instantiate(blockPrefab);
        float xPos = Random.Range(-8f, 8f);
        tempBlock.transform.position = startPos + new Vector3(xPos, 0, 0);
        tempBlock.GetComponent<Block>().SubscribeToMethod();
    }

    public void UpdateScore(GameObject currentBlock)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 blockPos = currentBlock.transform.position;

        if(Mathf.Abs(playerPos.x - blockPos.x) < 1f)
        {
            combo++;
            IsCombo = true;
        }
        else
        {
            combo = 1;
            IsCombo = false;
        }

        GameObject tempShadow = Instantiate(shadowPrefab);
        tempShadow.transform.position = shadowPrefab.transform.position + currentBlock.transform.position;
        Destroy(tempShadow, 3f);

        score += combo;
        scoreText.text = score.ToString();
    }

    public void UpdateDiamond()
    {
        diamond++;
        PlayerPrefs.SetInt("Diamond", diamond);
        diamondText.text = diamond.ToString();
    }

    public void GameStart()
    {
        startButton.SetActive(false);
        scoreButton.SetActive(true);

        player.GetComponent<Player>().hasGameStarted = true;
        ball.GetComponent<Ball>().hasGameStarted = true;
    }

    public void GameOver()
    {
        restartButton.SetActive(true);
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().canFollow = false;
        Camera.main.gameObject.GetComponent<CameraFollow>().canFollow = false;

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        highScoreEndText.text = "Best : " + highScore.ToString();
    }


    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
}
