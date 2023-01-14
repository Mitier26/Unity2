using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform rotateTransform;

    private void FixedUpdate()
    {
        rotateTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
