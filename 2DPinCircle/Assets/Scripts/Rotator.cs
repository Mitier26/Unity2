using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;                   // ȸ�� �ӵ�
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;     // ȸ�� ����

    private void Update()
    {
        // Rotate ���� * �ӵ� * ��Ÿ : ������Ʈ ȸ��
        transform.Rotate(rotateSpeed * rotateAngle * Time.deltaTime);
    }
}
