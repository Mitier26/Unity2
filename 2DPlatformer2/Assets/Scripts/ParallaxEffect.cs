using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // ����� ���� ��ġ
    private Vector2 startingPosition;

    // ����� z �� �ָ� ���� ���� �۴�
    private float startingZ;

    // ī�޶�� �������� ������ �Ÿ�
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // �÷��̾�� ������ Z �Ÿ�
    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // ���� ������Ʈ�� �տ� ������ near ( 0.3 ) �ָ� ������ far (1000)
    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // �ָ� ���� ���� �� ������.
    // ���� �¿����� ǥ���ϱ� ���� �̷��� ����
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
