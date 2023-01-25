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
        // ī�޶�� ��� ������ �Ÿ��� ����
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + transform.rotation * new Vector3(0,0,-distance);
        // ī�޶��� ��ġ��
        // rotation �� �ؾ��Ѵ�. ī�޶��� ȸ������ �⺻���� �̵��� ���Ѿ��Ѵ�.
    }
}
