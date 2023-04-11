using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseButton();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseButton();
        }
    }

    public void PauseButton()
    {
        bool isPaused = pausePanel.activeSelf;
        pausePanel.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1.0f : 0.0f;
    }

    public void StageButton()
    {
        SceneManager.LoadScene("Stage");
        Time.timeScale = 1f;
        PauseButton();
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        PauseButton();
    }

    public void ResumeButton()
    {
        PauseButton();
    }
}
