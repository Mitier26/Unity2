using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotate : MonoBehaviour
{
    public int speed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
