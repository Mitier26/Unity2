using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset;

    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.left;

        if(transform.position.x < maxOffset)
        {
            Destroy(gameObject);
        }
    }
}
