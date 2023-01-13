using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private string _PlaySceneName = "PlayGame";

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestScoreText;
    [SerializeField] private TMP_Text _bestScoreText;

    private void Start()
    {
        _bestScoreText.text = GameManager.Instance.LoadBest().ToString("F0");
        _scoreText.text = GameManager.Instance._score.ToString("F0");

        if(GameManager.Instance._score > GameManager.Instance.LoadBest())
        {
            _newBestScoreText.text = GameManager.Instance._score.ToString("F0");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(_PlaySceneName);
        GameManager.Instance._score = 0;
        GameManager.Instance._isPlay= true;
    }
}
