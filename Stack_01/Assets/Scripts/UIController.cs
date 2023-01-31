using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;

    [Header("InGame")]
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;

    [Header("GameOver")]
    [SerializeField]
    private GameObject textNewRecord;
    [SerializeField]
    private GameObject imagaCrown;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    [SerializeField]
    private GameObject textTouchToRestart;

    public void GameStart()
    {
        mainPanel.SetActive(false);

        textCurrentScore.gameObject.SetActive(true);
    }

    public void UpdateScore(int socre)
    {
        textCurrentScore.text = socre.ToString();
    }

    public void GameOver(bool isNewRecord)
    {
        if(isNewRecord == true)
        {
            textNewRecord.SetActive(true);
        }
        else
        {
            imagaCrown.SetActive(true);

            textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            textHighScore.gameObject.SetActive(true);
        }

        textTouchToRestart.SetActive(true);
    }
}
