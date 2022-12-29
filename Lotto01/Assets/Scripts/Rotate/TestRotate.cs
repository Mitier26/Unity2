using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;

    public Quaternion startQuaternion;
    public float lerpTime = 1;

    public float duration = 7f;
    public float elapsedTime;
    public float percentage;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        percentage = elapsedTime / duration;
        transform.rotation = Quaternion.Slerp(transform.rotation, startQuaternion, percentage * Time.deltaTime);
    }
}
