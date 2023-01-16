using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private TMP_Text bestScoreText;

    // ���� ��¿� ����
    //
    private void Awake()
    {
        // �ְ� ������ ǥ���Ѵ�. ����� ���ٸ� ����Ʈ ���� 0 �̿��� 0���� ǥ�õȴ�.
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

    // �ڷ�ƾ���� ������.
    private IEnumerator ShowScore()
    {
        int currentScore = GameManager.Instance.CurrentScore;

        // �Ʒ� �ݺ����� ���� �������.
        // ���ڸ� ǥ���ϱ� ���� �����ؾ��ϴ� ���� �ִ�.
        // �ְ� ������ ���� ������ ���Ͽ� ���� ������ �ְ� ���� ���� ������
        // newBest ���ڸ� ����� �־�� �Ѵ�.
        int highScore = GameManager.Instance.HighScore;

        // ���� ������ �ְ������� ��
        if(highScore < currentScore)
        {
            newBestText.gameObject.SetActive(true);
            // ����Ǿ� �ִ� �ְ� ���� ���� ���� ������ �ְ� ������ ������ �־���Ѵ�.
            GameManager.Instance.HighScore = currentScore;
        }
        else
        {
            newBestText.gameObject.SetActive(false);
        }

        // �ְ� ������ ���� ������ �ְ������� ǥ�����ش�.
        bestScoreText.text = GameManager.Instance.HighScore.ToString();

        int tempScore;
        float speed = 1f / animationTime;
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            // ������ ���� ���� ���Ͽ� ���ڰ� �����ϴ� �� ó�� ���̰� �Ѵ�.
            // ���ϸ��̼� Ŀ��� 0 ~ 1 ������ ���̴�.
            tempScore = (int)(animationSpeed.Evaluate(timeElapsed) * currentScore);
            scoreText.text = tempScore.ToString();
            yield return null;
        }

        // �ݺ��� ������ ���� ������ ������ �־�� �Ѵ�.
        scoreText.text = currentScore.ToString();



    }


    // ��ư Ŭ���� ���� ����
    [SerializeField] private AudioClip clickClip;
    public void ClickPlay()
    {
        GameManager.Instance.GoToGamePlay();
        SoundManager.Instance.PlaySound(clickClip);
    }
}
