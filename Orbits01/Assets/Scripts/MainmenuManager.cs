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
        // ���ڰ� �����ϴ� �� ó�� ���̰� ������ �Ѵ�.
        // 0 ���� ȹ���� ���� ���� ������ �ð�����
        // ���ڰ� �����ϴ� ȿ��

        int tempScore = 0;
        // ó�� 0�� �����ֱ����� �ؽ�Ʈ�� �Է��� �ش�.
        scoreText.text = tempScore.ToString();

        // �ְ� ������ ���ϱ� ���� ��
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


        // ���� ���� ���ϸ��̼�
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
