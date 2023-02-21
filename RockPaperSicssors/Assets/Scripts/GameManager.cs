using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Choices { Rock =0, Sicssors, Paper, None}
public class GameManager : MonoBehaviour
{
    const string win = "WIN!";
    const string draw = "Draw";

    public static GameManager Instance;

    Choices playerChoice = Choices.None;
    Choices opponentChoice = Choices.None;

    bool isPlayerSelected, isOpponentSelected, isGameFinished, isOpponentAI;

    string playerName, opponentName;

    [SerializeField]
    TextMeshProUGUI resultText;
    [SerializeField]
    GameObject settingsPanel;

    [SerializeField]
    TextMeshProUGUI playerNameInput, opponentNameInput;

    [SerializeField]
    Toggle toggle_isAI;

    [SerializeField]
    Image playerSelectedImange, oppoentSelectedImage;

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Animator playerChoiceAnim, opponentChoiceAnim, playerSelectedAnim, opponentSelectedAnim;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        playerName = GameData.data.playerName;
        opponentName = GameData.data.opponentName;
        isOpponentAI = GameData.data.isOpponentAI;
        settingsPanel.SetActive(false);
    }

    public void Select(Choices choices, bool isPlayer)
    {
        if (isGameFinished) return;
        if(isPlayer)
        {
            playerChoice = choices;
            isPlayerSelected = true;
            if(isOpponentAI)
            {
                Select((Choices)Random.Range(0, 3), false);
                isOpponentSelected = true;
            }
        }
        else
        {
            opponentChoice = choices;
            isOpponentSelected = true;
        }

        if(isPlayerSelected && isOpponentSelected)
        {
            isGameFinished = true;
            ResultWinner();
        }
    }

    public void ResultWinner()
    {
        if(playerChoice == opponentChoice)
        {
            resultText.text = draw;
        }
        else if(playerChoice == Choices.Paper && opponentChoice == Choices.Rock)
        {
            resultText.text = playerName + win;
        }
        else if (playerChoice == Choices.Rock && opponentChoice == Choices.Sicssors)
        {
            resultText.text = playerName + win;
        }
        else if (playerChoice == Choices.Sicssors && opponentChoice == Choices.Paper)
        {
            resultText.text = playerName + win;
        }
        else if (playerChoice == Choices.Paper && opponentChoice == Choices.Sicssors)
        {
            resultText.text = opponentName + win;
        }
        else if (playerChoice == Choices.Rock && opponentChoice == Choices.Paper)
        {
            resultText.text = opponentName + win;
        }
        else if (playerChoice == Choices.Sicssors && opponentChoice == Choices.Rock)
        {
            resultText.text = opponentName + win;
        }

        SetImage();
        SetAnimation();
    }


    public void SetImage()
    {
        Debug.Log((int)playerChoice);
        Debug.Log((int)opponentChoice);
        Debug.Log(playerChoice);
        Debug.Log(opponentChoice);
        Debug.Log(playerChoice.ToString());
        Debug.Log(opponentChoice.ToString());
        playerSelectedImange.sprite = sprites[(int)playerChoice];
        oppoentSelectedImage.sprite = sprites[(int)opponentChoice];
    }

    public void SetAnimation()
    {
        playerChoiceAnim.Play("anim_PlayerChoiceMove");
        opponentChoiceAnim.Play("anim_OpponentChoiceMove");
        playerSelectedAnim.Play("anim_PlayerSelectedMove");
        opponentSelectedAnim.Play("anim_OpponentSelectedMove");
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        toggle_isAI.isOn = isOpponentAI;
        playerNameInput.text = playerName;
        opponentNameInput.text = opponentName;
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ChangeAI(bool isAI)
    {
        GameData.data.isOpponentAI = isAI;
        isOpponentAI = isAI;
    }

    public void SetPlayerName(string inputName)
    {
        playerName = inputName;
        GameData.data.playerName = playerName;
    }

    public void SetOpponentName(string inputName)
    {
        opponentName = inputName;
        GameData.data.opponentName = inputName;
    }
}
