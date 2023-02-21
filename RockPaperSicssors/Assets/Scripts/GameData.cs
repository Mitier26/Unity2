using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string playerName;
    public string opponentName;
    public bool isOpponentAI;

    public static GameData data;

    private void Awake()
    {
        if(data == null)
        {
            data = this;
            playerName = "Player";
            opponentName = "AI";
            isOpponentAI = true;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
