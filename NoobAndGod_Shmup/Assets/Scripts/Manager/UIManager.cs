using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("UI")]
    public GameObject gameOverPanel, pausePanel;
    public TextMeshProUGUI currentScoreText, resultScoreText, resultHighScoreText;


    public void PauseButton()
    {
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
    }
}
