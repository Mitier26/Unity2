using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float timeForHalfCycls;

    [SerializeField] private Vector3 startPos, endPos;

    private float timeElapsed, speed, totalHeight;

    private void Awake()
    {
        totalHeight = endPos.y - startPos.y;
        timeElapsed = (transform.localPosition.y - startPos.y) / totalHeight;
        speed = 1 / timeForHalfCycls;
    }


    private void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime * speed;
        Vector3 temp = startPos;
        temp.y += timeElapsed * totalHeight;
        transform.localPosition = temp;

        if(timeElapsed <0f || timeElapsed > 1f)
        {
            speed *= -1f;
        }
    }
}
