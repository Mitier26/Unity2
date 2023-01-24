using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject textTitle;              // ������ Ÿ��Ʋ
    [SerializeField] private GameObject textTapToPlay;          // "Tap To Play" �ؽ�Ʈ

    [SerializeField] private GameObject buttonContinue;         // ��ư ������Ʈ

    [SerializeField] private TextMeshProUGUI textCurrentScore;  // ���� ����
    [SerializeField] private GameObject textScoreText;          // "Score" ǥ�� �ؽ�Ʈ

    [SerializeField] private TextMeshProUGUI textBestScore;     // �ְ� ���� ǥ��

    private int currentScore = 0;
    private int bestScore = 0;

    [SerializeField] private CameraController cameraController;

    public bool IsGameOver { get; private set; } = false;       // ������ ����Ǿ����� Ȯ�� �ϴ� ����

    private float gameOverDelayTime = 1f;                       // ���ӿ����� ��� �ð�

    private IEnumerator Start()
    {
        // ������ ���۵Ǹ� �ְ� ������ �ҷ� �´�.
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = $"<size=50>BEST</size>\n<size=100>{bestScore}</size>";

        // ���� ��ư�� ���� ������ �ݺ�
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

        textCurrentScore.gameObject.SetActive(true);        // ������ ���۵Ǹ� ���� ���̰�
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
        // ������ �ð� �� �� �Ʒ� ���� ����
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
