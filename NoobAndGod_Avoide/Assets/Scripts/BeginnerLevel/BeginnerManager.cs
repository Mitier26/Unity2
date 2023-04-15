using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeginnerManager : MonoBehaviour
{
    public static BeginnerManager instance;

    private float score;
    private float levelUpScore = 32;

    public bool isLive;
    [SerializeField]
    Transform spawners;
    [SerializeField]
    private UnityEngine.GameObject gameoverPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI resultScoreText;
    [SerializeField]
    private TextMeshProUGUI resultHighScoreText;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        score = 0;
        isLive = true;
        gameoverPanel.SetActive(false);
        if (!PlayerPrefs.HasKey(Constants.BginnerSaveString)) PlayerPrefs.SetInt(Constants.BginnerSaveString, 0);
    }

    private void Update()
    {
        if (!isLive) return;

        score += 1.7f * Time.deltaTime;
        scoreText.text = score.ToString("F0");

        if(score > levelUpScore)
        {
            levelUpScore += score;
            spawners.BroadcastMessage("LevelUp");
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = score.ToString("F0");
    }

    public void Death()
    {
        isLive = false;
        gameoverPanel.SetActive(true);
        resultScoreText.text = score.ToString("F0");

        int highScore = PlayerPrefs.GetInt(Constants.BginnerSaveString);
        if(score > highScore)
        {
            highScore = (int)score;

            FireBaseManager3.instance.SaveData(Constants.BginnerSaveString, highScore);
        }
        resultHighScoreText.text = highScore.ToString();

        BeginnerSpawner[] childs = spawners.GetComponentsInChildren<BeginnerSpawner>();
        foreach (var child in childs)
        {
            child.gameObject.SetActive(false);
        }
    }

}
