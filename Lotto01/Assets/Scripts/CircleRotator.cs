using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    public float duration = 7f;
    public float elapsedTime;
    public int count = 1; // 컨트롤러 에서 변경하도록 수정, collectorNum
    public Quaternion startQuaternion;

    public CollecterController controller;

    private bool isRotate = false;

    public void RotateStart()
    {
        elapsedTime = 0;
        isRotate = true;
    }

    private void FixedUpdate()
    {
        if(isRotate)
        {
            if(elapsedTime > 7) 
            {
                isRotate = false;
                count++;
                // 종료 이벤트
                controller.SetSwitch(State.COLLECTER_ON);
            }
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 60 * count), Mathf.Lerp(0, 1, percentage) * Time.deltaTime);
        }
        
    }
}
