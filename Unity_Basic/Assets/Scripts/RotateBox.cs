using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{
    public Vector3 direction;
    public float rotateSpeed;

    private float currentAngle;


    private void FixedUpdate()
    {
        currentAngle += rotateSpeed * Time.fixedDeltaTime;

        //transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        //transform.Rotate(direction);

        transform.Rotate(direction * Time.fixedDeltaTime * rotateSpeed);
    }
}
