using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideToSide : MonoBehaviour
{
    float speed, range;

    private void Start()
    {
        speed = Random.Range(0, 2) == 0 ? speed = 1 : -1;
        range = Random.Range(1.25f, 3.25f);

        speed *= Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {

        if (transform.position.x > range || transform.position.x < -range)
        {
            speed = -speed;
        }
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
    }
}
