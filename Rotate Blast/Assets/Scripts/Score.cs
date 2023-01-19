using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentId;

    [SerializeField] private Color activeColor, inActiveColor;
    [SerializeField] private SpriteRenderer sr;

    private void OnEnable()
    {
        GameManager.UpdateScoreColor += OnTargetSet;
    }

    private void OnDisable()
    {
        GameManager.UpdateScoreColor -= OnTargetSet;
    }


    private void OnTargetSet(int moveId)
    {
        sr.color = currentId == moveId ? activeColor : inActiveColor;
    }
}
