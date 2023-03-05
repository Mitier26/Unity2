using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int rotateSpeed = 300;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime ,0, Space.World );
    }
}
