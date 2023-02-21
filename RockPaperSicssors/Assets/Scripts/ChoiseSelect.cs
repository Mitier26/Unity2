using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseSelect : MonoBehaviour
{
    [SerializeField]
    private Choices buttonChoise;
    [SerializeField]
    bool isPlayer;

    public void ChoiseSelected()
    {
        GameManager.Instance.Select(buttonChoise, isPlayer);
    }
}
