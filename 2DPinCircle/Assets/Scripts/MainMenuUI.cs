using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] StageController stageController;
    [SerializeField] private RectTransformMover menuPanel;

    [SerializeField] private TextMeshProUGUI textLevelInMenu;
    [SerializeField] private TextMeshProUGUI textLevelInGame;

    private Vector3 inactivePosition = Vector3.left * 1080f;
    private Vector3 activePosition = Vector3.zero;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInMenu.text = $"Level{(index + 1)}";
    }
    public void ButtonClickEventStart()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInGame.text = $"{(index + 1)}";

        menuPanel.MoveTo(AfterStartEvent, inactivePosition);
    }

    private void AfterStartEvent()
    {
        stageController.IsGameStart = true;
    }

    public void ButtonClickEventReset()
    {
        PlayerPrefs.SetInt("StageLevel", 0);
    }

    public void ButtonClickEventExit()
    {
        // 플레이 상황에 따라 다르게 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit()
        #endif
    }

    public void StageExit()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInMenu.text = $"Level{(index + 1)}";

        menuPanel.MoveTo(AfterStageExitEvent, activePosition);
    }

    private void AfterStageExitEvent()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        if(index == SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("StageLevel", 0);
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(index);
    }
}
