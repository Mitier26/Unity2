using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "Game Start";
        GameManager.instance.message += UpdateMessage;
    }

    void UpdateMessage(Player player)
    {
        myText.text = GameManager.instance.hasGameFinished ? "GAME OVER" : player.ToString() + " 'S TURN";
    }


}
