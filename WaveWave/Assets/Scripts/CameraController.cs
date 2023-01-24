using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;              // 카메라의 추격 대상
    [SerializeField] private float yOffset = 8f;            // 카메라 y 위치
    [SerializeField] private float smoothTime = 0.3f;       // 부드럽게 이동하는 시간
    private Vector3 velocity = Vector3.zero;                // 값의 변화량

    private Camera mainCamera;    // 배경색 변경용

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0,yOffset,-10));
        
        // Vector3 worldPosition = Transform.TransformPosint(Vector3 localPosition)
        // 로컬 좌표를 월드 좌표로 변환한다.
        // target ( Player ) 의 좌표에서 Y 축으로 yOffset 만큼, z -10 만 큰 떨어진 장소를 저장한다.
        
        targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);
        // Player 위치가 아니고 카메라가 따라갈 대상이다.

        // 해당 위치 까지 부드럽게 이동
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        // 현재 위치, 목표 위치, 현재 속도, 목표 도달 시간
    }

    public void ChangeBackgroundColor()
    {
        float colorHue = Random.Range(0, 10);
        colorHue *= 0.1f;
        mainCamera.backgroundColor = Color.HSVToRGB(colorHue, 0.6f, 0.8f);
        // HSVToRGB
        // 색상(Hue), 채도(Saturation), 명도(value) 을 RGB 색상으로 변환
    }
}
