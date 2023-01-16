using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private TMP_Text bestScoreText;

    // 점수 출력에 관한
    //
    private void Awake()
    {
        // 최고 점수를 표시한다. 기록이 없다면 디폴트 값이 0 이여서 0으로 표시된다.
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

    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve animationSpeed;

    // 코루틴으로 돌린다.
    private IEnumerator ShowScore()
    {
        int currentScore = GameManager.Instance.CurrentScore;

        // 아래 반복문을 먼저 만들었다.
        // 숫자를 표현하기 전에 먼저해야하는 일이 있다.
        // 최고 점수와 현재 점수를 비교하여 현재 점수가 최고 점수 보다 높으면
        // newBest 글자를 출력해 주어야 한다.
        int highScore = GameManager.Instance.HighScore;

        // 현재 점수와 최고점수의 비교
        if(highScore < currentScore)
        {
            newBestText.gameObject.SetActive(true);
            // 저장되어 있는 최고 점수 보다 높기 때문에 최고 점수를 변경해 주어야한다.
            GameManager.Instance.HighScore = currentScore;
        }
        else
        {
            newBestText.gameObject.SetActive(false);
        }

        // 최고 점수를 갱신 했으니 최고점수를 표시해준다.
        bestScoreText.text = GameManager.Instance.HighScore.ToString();

        int tempScore;
        float speed = 1f / animationTime;
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            // 위에서 나온 값을 곱하여 숫자가 증가하는 것 처럼 보이게 한다.
            // 에니메이션 커브는 0 ~ 1 사이의 값이다.
            tempScore = (int)(animationSpeed.Evaluate(timeElapsed) * currentScore);
            scoreText.text = tempScore.ToString();
            yield return null;
        }

        // 반복이 끝나면 최종 값으로 지정해 주어야 한다.
        scoreText.text = currentScore.ToString();



    }


    // 버튼 클릭에 관한 동작
    [SerializeField] private AudioClip clickClip;
    public void ClickPlay()
    {
        GameManager.Instance.GoToGamePlay();
        SoundManager.Instance.PlaySound(clickClip);
    }
}
