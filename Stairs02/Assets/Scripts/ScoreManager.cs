using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static TextMeshProUGUI scoreText;
    private static int score;
    private static int scoreModifier;

    private static TextMeshPro scoreCounterText;
    private static Vector3 scoreCounterPosition;
    private static Transform scoreCounterParent;

    private void Start()
    {
        score = 0;
        scoreModifier = 0;
        scoreText = GetComponent<TextMeshProUGUI>();

        scoreCounterText = GameObject.Find("Score Counter (TMP)").GetComponent<TextMeshPro>();
    }

    public static Vector3 ScoreCounterPosition
    {
        get
        {
            return scoreCounterPosition;
        }
        set
        {
            Vector3 counterPos = new Vector3(value.x, value.y + 0.001f, value.z);
            scoreCounterPosition = counterPos;
            scoreCounterText.transform.position = scoreCounterPosition;
            scoreCounterText.gameObject.GetComponent<Animator>().Play(0);
        }
    }

    public static Transform ScoreCounterParent
    {
        get
        {
            return scoreCounterParent;
        }
        set
        {
            scoreCounterParent = value;
            scoreCounterText.transform.SetParent(scoreCounterParent, true);
        }
    }

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value + scoreModifier;
            scoreText.text = score.ToString();
            int temp = 1 + ScoreModifier;
            scoreCounterText.text = "+ " + temp.ToString();
        }
    }

    public static int ScoreModifier
    {
        get
        {
            return scoreModifier;
        }
        set
        {
            scoreModifier = value;
        }
    }

    private void OnDestroy()
    {
        score = 0;
        scoreModifier = 0;
    }
}
