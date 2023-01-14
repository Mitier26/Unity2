using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainmenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text newBestText;
    [SerializeField] TMP_Text bestScoreText;

    private void Awake()
    {
        bestScoreText.text = GameManager.Instance.HighScore.ToString();

        if(!GameManager.Instance.IsInitialzed)
        {
            scoreText.gameObject.SetActive(false);
            newBestText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowScore());
        }
    }

    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float animationTime; 

    private IEnumerator ShowScore()
    {
        // 숫자가 증가하는 것 처럼 보이게 만들어야 한다.
        // 0 에서 획득한 점수 까지 설정한 시간동안
        // 숫자가 증가하는 효과

        int tempScore = 0;
        // 처음 0을 보여주기위해 텍스트를 입력해 준다.
        scoreText.text = tempScore.ToString();

        // 최고 점수와 비교하기 위한 것
        int currentScore = GameManager.Instance.CurrentScore;
        int bestScore = GameManager.Instance.HighScore;

        if(currentScore > bestScore)
        {
            GameManager.Instance.HighScore = currentScore;
            bestScoreText.gameObject.SetActive(true);
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
        }


        // 숫자 증가 에니메이션
        float timeElapsed = 0;
        float speed = 1 / animationTime;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            tempScore = (int)speedCurve.Evaluate(timeElapsed) * currentScore;
            scoreText.text = tempScore.ToString();
            yield return null;
        }

        tempScore = currentScore;
        scoreText.text = tempScore.ToString();

    }

    [SerializeField] private AudioClip clickSFX;
    public void ClickButton()
    {
        GameManager.Instance.GoToGamePlay();
        SoundManager.Instance.PlaySound(clickSFX);
    }
}
