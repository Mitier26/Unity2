using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayManager : MonoBehaviour
{
    #region START

    private bool hasGameFinished;

    public static GamePlayManager Instance;

    public List<Color> colors;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            hasGameFinished = false;
            GameManager.Instance.IsInitialized = true;

            score = 0;
            scoreText.text = ((int)score).ToString();
            StartCoroutine(SpawnScore());
        }
    }

    #endregion

    #region GAME_LOGIC

    [SerializeField] private ScoreEffect scoreEffect;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !hasGameFinished)
        {
            if(currentScore == null)
            {
                GameEnded();
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(!hit.collider || !hit.collider.gameObject.CompareTag("Block"))
            {
                GameEnded();
                return;
            }

            int currentScoreId = currentScore.colorId;
            int clickedScoreId = hit.collider.gameObject.GetComponent<Player>().colorId;

            if(currentScoreId != clickedScoreId)
            {
                GameEnded();
                return;
            }

            var t = Instantiate(scoreEffect, currentScore.gameObject.transform.position, Quaternion.identity);
            t.Init(colors[currentScoreId]);

            var tempScore = currentScore;
            if(currentScore.NextScore != null)
            {
                currentScore = currentScore.NextScore;
            }
            Destroy(tempScore.gameObject);

            UpdateScore();
        }
    }

    #endregion

    #region SCORE

    private float score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip pointClip;

    private void UpdateScore()
    {
        score++;
        SoundManager.Instance.PlaySound(pointClip);
        scoreText.text = ((int)score).ToString();
    }

    [SerializeField] private float spawnTime;
    [SerializeField] private Score scorePrefab;
    private Score currentScore;

    private IEnumerator SpawnScore()
    {
        Score prevScore = null;

        while(!hasGameFinished)
        {
            var tempScore = Instantiate(scorePrefab);

            if(prevScore == null)
            {
                prevScore = tempScore;
                currentScore = prevScore;
            }
            else
            {
                prevScore.NextScore = tempScore;
                prevScore = tempScore;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    #endregion

    #region GAME_OVER

    [SerializeField] private AudioClip loseClip;
    public UnityAction GameEnd;

    public void GameEnded()
    {
        hasGameFinished = true;
        GameEnd?.Invoke();
        SoundManager.Instance.PlaySound(loseClip);
        GameManager.Instance.CurrentScore = (int)score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainMenu();
    }

    #endregion
}
