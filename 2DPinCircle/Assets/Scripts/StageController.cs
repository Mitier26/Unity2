using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner;
    [SerializeField] private int throwablePinCount;     // Ŭ�����ϱ� ���� ������ ��� ���� ����

    // ����ȭ�� �ϴܿ� ��ġ�Ǵ� ������ �ϴ� ���ɵ��� ù ��° �� ��ġ
    private Vector3 firstTPinPosition = Vector3.down * 2;
    // ������ �ϴ� �ɵ� ������ ��ġ �Ÿ�
    public float TPinDistance { get; private set; } = 1;

    private void Awake()
    {
        // ���� �ϴܿ� ��ġ�Ǵ� �������� �ϴ� �� ������Ʈ ����
        for(int i = 0; i < throwablePinCount; ++i)
        {
            pinSpawner.SpawnThrowablePin(firstTPinPosition + Vector3.down * TPinDistance * i);
        }
    }
}
