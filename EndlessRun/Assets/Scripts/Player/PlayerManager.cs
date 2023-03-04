using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameover;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public TMP_Text coinsText;

    private void Start()
    {
        gameover = false;
        Time.timeScale = 1.0f;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    private void Update()
    {
        if(gameover)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins : " + numberOfCoins;
        if(SwipeManager.tap)
        {
            isGameStarted = true;
            startingText.SetActive(false);
        }
    }
}
