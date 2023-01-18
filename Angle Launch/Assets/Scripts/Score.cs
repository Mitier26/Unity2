using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int Id;

    [SerializeField] private Color activeColor, inActiveColor, targetColor;
    [SerializeField] private SpriteRenderer sr;

    private void OnEnable()
    {
        GameManager.UpdateColor += OnTargetSet;
        GameManager.UPdateMoveColor += OnMoveSet;
    }

    private void OnDisable()
    {
        GameManager.UpdateColor -= OnTargetSet;
        GameManager.UPdateMoveColor -= OnMoveSet;
    }

    private void OnTargetSet(int targetId)
    {
        sr.color = targetId == Id ? targetColor : inActiveColor;
    }

    private void OnMoveSet(int moveId)
    {
        sr.color = moveId == Id ? activeColor : sr.color;
    }
}
