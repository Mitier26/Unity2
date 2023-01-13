using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤으로 만든다.
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // PlayerPrefs에 저장할 때 사용하는 키를 고정시킨다.
    // 오타에의한 에러를 방지할 수 있는 좋은 방법이다.
    private string highScoreKey = "HighScore";

    // 프로퍼티로 만들었다.
    // HighScore를 변수처럼 사용할 수 있다.
    // 장점은 Set 값을 입력할 때 알아서 PlayerPrefs에 저장이 된다.
    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(highScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(highScoreKey, value);
        }
    }

    // set 앞에 private를 붙이면 다른 곳에서 수정할 수 없게 된다.
    public int CurrentScore { get; set; }
    public bool IsInitialized { get; set; }

    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    // 씬 이동을 위한 키
    private const string MainMenu = "MainMenu";
    private const string GamePlay = "GamePlay";

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void GotoGamePlay()
    {
        SceneManager.LoadScene(GamePlay);
    }
}
