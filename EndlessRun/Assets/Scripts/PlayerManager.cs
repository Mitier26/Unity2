using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameover;
    public GameObject gameOverPanel;

    private void Start()
    {
        gameover = false;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if(gameover)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
    }
}
