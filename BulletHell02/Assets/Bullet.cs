using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BULLET_SPEED = 1f;
    private const float MAX_X = 10f;
    private const float MAX_Y = 5f;

    private Vector3? mDirection = null;


    private void Update()
    {
        if(mDirection != null)
        {
            Vector3 direction = (Vector3)mDirection;
            transform.position = transform.position + direction * BULLET_SPEED * Time.deltaTime;

            if(transform.position.x > MAX_X || transform.position.x < -MAX_X || transform.position.y > MAX_Y || transform.position.y < -MAX_Y)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection( Vector3 direction)
    {
        mDirection = direction;
    }
}
