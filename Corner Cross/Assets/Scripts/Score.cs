using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentId;

    [SerializeField] private Color activeColor, inActiveColor;
    [SerializeField] private SpriteRenderer sr, sr1, sr2;

    private Color currentColor;

    private void OnEnable()
    {
        GameManager.UpdateScoreColor += OnTargetSet;
    }

    private void OnDisable()
    {
        GameManager.UpdateScoreColor -= OnTargetSet;
    }


    private void OnTargetSet(int targetId)
    {
        currentColor = targetId == currentId ? activeColor : inActiveColor;
        sr.color = sr1.color = sr2.color = currentColor;
    }
}
