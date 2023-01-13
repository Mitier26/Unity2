using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱������� �����.
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

    // PlayerPrefs�� ������ �� ����ϴ� Ű�� ������Ų��.
    // ��Ÿ������ ������ ������ �� �ִ� ���� ����̴�.
    private string highScoreKey = "HighScore";

    // ������Ƽ�� �������.
    // HighScore�� ����ó�� ����� �� �ִ�.
    // ������ Set ���� �Է��� �� �˾Ƽ� PlayerPrefs�� ������ �ȴ�.
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

    // set �տ� private�� ���̸� �ٸ� ������ ������ �� ���� �ȴ�.
    public int CurrentScore { get; set; }
    public bool IsInitialized { get; set; }

    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    // �� �̵��� ���� Ű
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
