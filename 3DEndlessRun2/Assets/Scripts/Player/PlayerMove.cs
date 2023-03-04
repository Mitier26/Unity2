using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public float sideMoveSpeed = 4;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(transform.position.x > LevelBoundary.leftSide)
                transform.Translate(Vector3.left * sideMoveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < LevelBoundary.rightSide)
                transform.Translate(Vector3.right * sideMoveSpeed * Time.deltaTime, Space.World);
        }

    }
}
