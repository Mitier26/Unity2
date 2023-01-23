using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    [SerializeField] private float rotateSpeed = 50f;                   // 회전 속도
    [SerializeField] private float maxRotateSpeed = 500f;               // 최대 회전 속도
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;     // 회전 각도

    public void Stop()
    {
        rotateSpeed = 0f;
    }

    public void RotateFast()
    {
        rotateSpeed = maxRotateSpeed;
    }

    private void Update()
    {
        if (stageController.IsGameStart == false) return;

        // Rotate 방향 * 속도 * 델타 : 오브젝트 회전
        transform.Rotate(rotateSpeed * rotateAngle * Time.deltaTime);
    }
}
