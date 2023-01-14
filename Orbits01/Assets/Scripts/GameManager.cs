using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private readonly string highScoreKey = "HighScore";
    public int HighScore
    {
        get { return PlayerPrefs.GetInt(highScoreKey, 0); }
        set { PlayerPrefs.SetInt(highScoreKey, value); }
    }

    public void Init()
    {
        CurrentScore = 0;
        IsInitialzed = false;
    }

    public int CurrentScore { get; set; }
    public bool IsInitialzed { get; set; }

    private const string Mainmenu = "Mainmenu";
    private const string GamePlay = "Gameplay";

    public void GoToGamePlay()
    {
        SceneManager.LoadScene(GamePlay);
    }

    public void GoToMainmenu()
    {
        SceneManager.LoadScene(Mainmenu);
    }

}
