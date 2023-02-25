using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3 direction;
    private float speed = 4;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
