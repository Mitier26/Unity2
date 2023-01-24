using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject textTitle;              // 게임의 타이틀
    [SerializeField] private GameObject textTapToPlay;          // "Tap To Play" 텍스트

    [SerializeField] private GameObject buttonContinue;         // 버튼 오브젝트

    [SerializeField] private TextMeshProUGUI textCurrentScore;  // 현재 점수
    [SerializeField] private GameObject textScoreText;          // "Score" 표시 텍스트

    [SerializeField] private TextMeshProUGUI textBestScore;     // 최고 점수 표시

    private int currentScore = 0;
    private int bestScore = 0;

    [SerializeField] private CameraController cameraController;

    public bool IsGameOver { get; private set; } = false;       // 게임이 종료되었는지 확인 하는 변수

    private float gameOverDelayTime = 1f;                       // 게임오버시 대기 시간

    private IEnumerator Start()
    {
        // 게임이 시작되면 최고 점수를 불러 온다.
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = $"<size=50>BEST</size>\n<size=100>{bestScore}</size>";

        // 왼쪽 버튼을 누를 때까지 반복
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameStart();

                yield break;
            }

            yield return null;
        }
    }

    public void GameStart()
    {
        textTitle.SetActive(false);
        textTapToPlay.SetActive(false);

        textCurrentScore.gameObject.SetActive(true);        // 게임이 시작되면 점수 보이게
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;

        textCurrentScore.text = currentScore.ToString();

        if (currentScore < bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            textBestScore.text = $"<size=50>BEST</size>\n<size=100>{currentScore}</size>";
        }

        cameraController.ChangeBackgroundColor();
    }

    public void GameOver()
    {
        ShakeCamera.Instance.OnShakeCamera(0.5f, 0.1f);

        IsGameOver = true;

        StartCoroutine(OnGameOver());
       
    }

    private IEnumerator OnGameOver()
    {
        // 딜레이 시간 이 후 아래 동작 수행
        yield return new WaitForSeconds(gameOverDelayTime);

        //int bestScore = PlayerPrefs.GetInt("BestScore");

        if (currentScore == bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            //textBestScore.text = $"<size=50>BEST</size>\n<size=100>{currentScore}</size>";
        }

        buttonContinue.SetActive(true);
        textScoreText.SetActive(true);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
