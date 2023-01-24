using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;              // ī�޶��� �߰� ���
    [SerializeField] private float yOffset = 8f;            // ī�޶� y ��ġ
    [SerializeField] private float smoothTime = 0.3f;       // �ε巴�� �̵��ϴ� �ð�
    private Vector3 velocity = Vector3.zero;                // ���� ��ȭ��

    private Camera mainCamera;    // ���� �����

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0,yOffset,-10));
        
        // Vector3 worldPosition = Transform.TransformPosint(Vector3 localPosition)
        // ���� ��ǥ�� ���� ��ǥ�� ��ȯ�Ѵ�.
        // target ( Player ) �� ��ǥ���� Y ������ yOffset ��ŭ, z -10 �� ū ������ ��Ҹ� �����Ѵ�.
        
        targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);
        // Player ��ġ�� �ƴϰ� ī�޶� ���� ����̴�.

        // �ش� ��ġ ���� �ε巴�� �̵�
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        // ���� ��ġ, ��ǥ ��ġ, ���� �ӵ�, ��ǥ ���� �ð�
    }

    public void ChangeBackgroundColor()
    {
        float colorHue = Random.Range(0, 10);
        colorHue *= 0.1f;
        mainCamera.backgroundColor = Color.HSVToRGB(colorHue, 0.6f, 0.8f);
        // HSVToRGB
        // ����(Hue), ä��(Saturation), ��(value) �� RGB �������� ��ȯ
    }
}
