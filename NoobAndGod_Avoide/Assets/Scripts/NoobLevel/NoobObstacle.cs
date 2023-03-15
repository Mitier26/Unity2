using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobObstacle : MonoBehaviour
{
    public float moveDelay = 1f;
    private float moveCount = 0;

    private void OnEnable()
    {
        SetPosition();

    }
    private void Update()
    {
        if(moveCount > moveDelay)
        {
            transform.position += Vector3.down;
            moveCount = 0f;
        }

        if(transform.position.y < -4.5)
        {
            SetPosition();
        }

        moveCount += Time.deltaTime;
    }

    private void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-2, 3), 5.5f, 0);
    }
}
