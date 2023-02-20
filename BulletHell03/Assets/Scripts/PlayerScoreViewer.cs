using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerController PlayerController;
    private TMP_Text textScore;

    private void Awake()
    {
        textScore = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textScore.text = "Score " + PlayerController.Score;
    }
}
