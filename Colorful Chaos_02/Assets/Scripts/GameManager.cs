using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 메니저는 점수를 관리하고 씬을 관리 한다.
    // 게임 메니저는 다른 곳에서 사용할 수 있도록 싱글톤으로 만든다.

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            // 초기화
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 점수 관리
    // 최고 점수, 현재점수
    private readonly string HighScoreKey = "HighScore";
    public int HighScore
    {
        get { return PlayerPrefs.GetInt(HighScoreKey, 0); }
        set { PlayerPrefs.SetInt(HighScoreKey, value); }
    }

    public int CurrentScore { get; set; }

    // 초기화 관리
    public bool IsInitialzed { get; set; }
    // 게임이 실행되었는지 확인 하는 것으로 MainMenuManager에서 사용된다.

    private void Init()
    {
        CurrentScore = 0;
        IsInitialzed = false;
    }


    // 씬 관리
    private const string mainMenu = "MainMenu";
    private const string gamePlay = "GamePlay";

    public void GoToGamePlay()
    {
        SceneManager.LoadScene(gamePlay);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
