using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private GameObject pattern01;

    private readonly float scoreScale = 20f;

    public float CurrentScore { get; private set; }

    public bool IsGamePlay { get; private set; } = false;

    public void GameStart()
    {
        uiController.GameStart();
        pattern01.SetActive(true);
        IsGamePlay = true;
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void GameOver()
    {
        uiController.GameOver();

        pattern01.SetActive(false);

        IsGamePlay = false;
    }

    private void Update()
    {
        if (IsGamePlay == false) return;

        CurrentScore += Time.deltaTime * scoreScale;
    }
}
