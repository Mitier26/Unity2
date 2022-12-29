using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Rotator1 : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;

    public Quaternion startQuaternion;
    public float lerpTime = 1;

    private void Start()
    {
        startQuaternion = transform.rotation;
    }

    private void Update()
    {
        //BasicRotate1();
        //BasicRotate2();
        LerpRotation();
    }

    public void BasicRotate1()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
    public void BasicRotate2()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    public void LerpRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, lerpTime * Time.deltaTime);
    }

}
