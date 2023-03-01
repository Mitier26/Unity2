using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondManager : MonoBehaviour
{
    private static TextMeshProUGUI diamondText;
    private static int diamondScore;

    private void Start()
    {
        diamondScore = 0;
        diamondText = GetComponent<TextMeshProUGUI>();
    }

    public static int DiamondScore
    {
        get
        {
            return diamondScore;
        }
        set
        {
            diamondScore = value;
            diamondText.text = diamondScore.ToString();
        }
    }

    private void OnDestroy()
    {
        diamondScore = 0;
    }
}
