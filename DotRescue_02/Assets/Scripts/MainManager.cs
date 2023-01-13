using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // 메인 메뉴에 있는 점수와 글자에 관한 것
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;

    private void Awake()
    {
        // 시작하면 GameManager에 있는 HighScore를 불러온다.
        // HighSocre는 프로퍼티로 되어 있어 get을 하면 PlayerPrefs에서 읽어 온다.
        _bestScoreText.text = GameManager.Instance.HighScore.ToString();


        if (!GameManager.Instance.IsInitialized)
        {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowScore());
        }
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;
    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        // new Best를 제어한다.
        if(highScore < currentScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }

        _bestScoreText.text = GameManager.Instance.HighScore.ToString();
        float speed = 1 / _animationTime;
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            // animationTime 동안 실행되게 만든다.
            timeElapsed += speed * Time.deltaTime;
            // animation Curve는  0 ~ 1 까지 이다 
            // 지정한 시간만큼 숫자가 증가하는 것 처럼 보이게 만든다.
            tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
            _scoreText.text = tempScore.ToString();
            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    [SerializeField] private AudioClip _clickClip;

    public void ClickedPlay()
    {
        SoundManager.Instance.PlaySound(_clickClip);
        GameManager.Instance.GotoGamePlay();
    }
}
