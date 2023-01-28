using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;

    [Header("InGame")]
    [SerializeField]
    private TMP_Text textCurrentScore;
    [SerializeField]
    private TMP_Text textHighScore;

    [SerializeField]
    private UIPausePanelAnimation pausePanel;

    [Header("GameOver")]
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private Screenshot screenshot;
    [SerializeField]
    private Image imageScreenshot;
    [SerializeField]
    private TMP_Text textResultScore;

    private void Update()
    {
        textCurrentScore.text = stageController.CurrentScore.ToString();
        textHighScore.text = stageController.HighScore.ToString();
    }

    public void BtnClickPause()
    {
        pausePanel.OnAppear();
    }

    public void BtnClickHome()
    {
        SceneManager.LoadScene("01Main");
    }

    public void BtnClickRestart()
    {
        SceneManager.LoadScene("02Game");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BtnClickPlay()
    {
        pausePanel.OnDisappear();
    }

    public void GameOver()
    {
        imageScreenshot.sprite = screenshot.ScreenshotToSprite();
        textResultScore.text = stageController.CurrentScore.ToString();

        panelGameOver.SetActive(true);
    }
}
