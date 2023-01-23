using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    [SerializeField] private float rotateSpeed = 50f;                   // ȸ�� �ӵ�
    [SerializeField] private float maxRotateSpeed = 500f;               // �ִ� ȸ�� �ӵ�
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;     // ȸ�� ����

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

        // Rotate ���� * �ӵ� * ��Ÿ : ������Ʈ ȸ��
        transform.Rotate(rotateSpeed * rotateAngle * Time.deltaTime);
    }
}
