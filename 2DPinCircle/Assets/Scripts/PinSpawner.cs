using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private GameObject pinPrefab;      // �� ������

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform;                 // ������ Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2;   // ������ ��ġ
    [SerializeField] private float targetRadius = 0.8f;                 // ������ ������
    [SerializeField] private float pinLength = 1.5f;                    // ������� ����

    public void SpawnThrowablePin(Vector3 position)
    {
        // ���� �غ� �ϴ� �� ����
        Instantiate(pinPrefab, position, Quaternion.identity);
    }

    public void SpawnStuckPin(float angle)
    {
        // ���ῡ ��ġ�� �� ������Ʈ ����
        GameObject clone = Instantiate(pinPrefab);

        // ������ ������ ���� ���ῡ ��ġ �ǵ��� ����
        SetInPinStuckToTarget(clone.transform, angle);
    }

    public void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // ���� ��ġ : �ش� ������ ���� ��ġ�Ǿ��� ���� ����
        pin.position = Utils.GetPositionFromAngle(targetRadius+ pinLength, angle) + targetPosition;

        // ���� ȸ��
        pin.rotation = Quaternion.Euler(0, 0, angle);

        // ���� �θ� ���� : �θ�� �Բ� ȸ���ϱ� ����
        pin.SetParent(targetTransform);

        // ���� ����� Ȱ��ȭ
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }
}
