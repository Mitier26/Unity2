using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;                   // 회전 속도
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;     // 회전 각도

    private void Update()
    {
        // Rotate 방향 * 속도 * 델타 : 오브젝트 회전
        transform.Rotate(rotateSpeed * rotateAngle * Time.deltaTime);
    }
}
