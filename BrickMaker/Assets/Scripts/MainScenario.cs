using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScenario : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textHighScore;

    private void Awake()
    {
        textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void BtnClickGameStart()
    {
        SceneManager.LoadScene("02Game");
    }

    public void BtnClickGameExit()
    {
        #if UNITY_EDITER
        UnityEditor.EditorApplication.isPlayint = false;
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
