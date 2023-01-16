using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���� �޴����� ������ �����ϰ� ���� ���� �Ѵ�.
    // ���� �޴����� �ٸ� ������ ����� �� �ֵ��� �̱������� �����.

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            // �ʱ�ȭ
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ����
    // �ְ� ����, ��������
    private readonly string HighScoreKey = "HighScore";
    public int HighScore
    {
        get { return PlayerPrefs.GetInt(HighScoreKey, 0); }
        set { PlayerPrefs.SetInt(HighScoreKey, value); }
    }

    public int CurrentScore { get; set; }

    // �ʱ�ȭ ����
    public bool IsInitialzed { get; set; }
    // ������ ����Ǿ����� Ȯ�� �ϴ� ������ MainMenuManager���� ���ȴ�.

    private void Init()
    {
        CurrentScore = 0;
        IsInitialzed = false;
    }


    // �� ����
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
