using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScoreViewer : MonoBehaviour
{
    private TMP_Text textResultScore;

    private void Awake()
    {
        textResultScore = GetComponent<TMP_Text>();
        int score = PlayerPrefs.GetInt("Score");
        textResultScore.text = "Result Score : " + score; ;
    }
}
