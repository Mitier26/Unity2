using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static float leftSide = -3.5f;
    public static float rightSide = 3.5f;
    public float interalLeft;
    public float interalRight;

    private void Update()
    {
        interalLeft = leftSide;
        interalRight = rightSide;
    }
}
