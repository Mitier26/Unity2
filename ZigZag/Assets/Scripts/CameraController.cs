using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float distance;

    private void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position);
        // 카메라와 대상 사이의 거리를 저장
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + transform.rotation * new Vector3(0,0,-distance);
        // 카메라의 위치값
        // rotation 을 해야한다. 카메라의 회전값을 기본으로 이동을 시켜야한다.
    }
}
