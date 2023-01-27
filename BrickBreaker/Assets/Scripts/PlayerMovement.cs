using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float moveOffset = 7f;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        if ((moveX > 0 && transform.position.x < moveOffset) || (moveX < 0 && transform.position.x > -moveOffset))
        {
            transform.position += Vector3.right * moveX * moveSpeed * Time.deltaTime;
        }

    }
}
