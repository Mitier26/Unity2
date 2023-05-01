using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }
}
