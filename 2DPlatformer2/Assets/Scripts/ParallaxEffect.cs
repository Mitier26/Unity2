using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // 배경의 시작 위치
    private Vector2 startingPosition;

    // 배경의 z 값 멀리 있을 수록 작다
    private float startingZ;

    // 카메라와 시작지점 사이의 거리
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // 플레이어와 배경과의 Z 거리
    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // 만약 오브젝트가 앞에 있으면 near ( 0.3 ) 멀리 있으면 far (1000)
    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // 멀리 있을 수록 더 빠르다.
    // 상하 좌우전부 표현하기 위해 이렇게 만듬
    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;

    private void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
