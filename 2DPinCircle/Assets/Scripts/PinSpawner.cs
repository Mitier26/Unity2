using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private StageController stageController;           
    [SerializeField] private GameObject pinPrefab;                      // �� ������
    [SerializeField] private GameObject textPinIndexPrefab;             // �ɿ� ���ڸ� ǥ���ϴ� UI
    [SerializeField] private Transform textParent;                      // UI�� �θ�

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform;                 // ������ Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2;   // ������ ��ġ
    [SerializeField] private float targetRadius = 0.8f;                 // ������ ������
    [SerializeField] private float pinLength = 1.5f;                    // ������� ����

    [Header("Throwable Pin")]
    [SerializeField] private float bottomAngle = 270f;                  // ���ῡ ��ġ�Ǵ� ���� ����
    private List<Pin> throwablePins;                                    // �ϴܿ� �����Ǵ� �� ����Ʈ

    private AudioSource audioSource;

    public void Setup()
    {
        audioSource = GetComponent<AudioSource>();
        throwablePins = new List<Pin>();
    }

    private void Update()
    {
        if (stageController.IsGameStart == false || stageController.IsGameOver == true) return;

        if(Input.GetMouseButtonDown(0) && throwablePins.Count > 0)
        {
            // ����Ʈ�� �ִ� 0 �� ���� ���ῡ ��ġ
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            // ����Ʈ 0���� �ִ� ��� ����
            throwablePins.RemoveAt(0);

            for(int i = 0; i < throwablePins.Count; ++i)
            {
                throwablePins[i].MoveonStep(stageController.TPinDistance);
            }

            // ���� ��ġ�� �� ���� ���� ����
            stageController.DecreaseThrowablePin();

        }
    }

    public void SpawnThrowablePin(Vector3 position, int index)
    {
        // ���� �� ������Ʈ ����
        GameObject clone = Instantiate(pinPrefab, position, Quaternion.identity);
        // ������ ���� ������ 
        Pin pin = clone.GetComponent<Pin>();
        // ������ �� ������Ʈ�� �� ��ũ��Ʈ�� ����Ʈ�� �߰�

        pin.SetUp(stageController);

        throwablePins.Add(pin);

        // �ɿ� ǥ�õǴ� UI����
        SpawnTextUI(clone.transform, index);
    }

    public void SpawnStuckPin(float angle, int index)
    {
        // ���ῡ ��ġ�� �� ������Ʈ ����
        GameObject clone = Instantiate(pinPrefab);

        Pin pin = clone.GetComponent<Pin>();
        pin.SetUp(stageController);

        // ������ ������ ���� ���ῡ ��ġ �ǵ��� ����
        SetInPinStuckToTarget(clone.transform, angle);

        SpawnTextUI(clone.transform, index);
    }

    public void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // ���� ��ġ : �ش� ������ ���� ��ġ�Ǿ��� ���� ����
        pin.position = Utils.GetPositionFromAngle(targetRadius + pinLength, angle) + targetPosition;

        // ���� ȸ��
        pin.rotation = Quaternion.Euler(0, 0, angle);

        // ���� �θ� ���� : �θ�� �Բ� ȸ���ϱ� ����
        pin.SetParent(targetTransform);

        // ���� ����� Ȱ��ȭ
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }

    private void SpawnTextUI(Transform target, int index)
    {
        // ���ڸ� ǥ���� ������Ʈ ����
        GameObject textClone = Instantiate(textPinIndexPrefab);
        // ������ �θ� ������ ����
        textClone.transform.SetParent(textParent);
        // ������ ũ��� 1,1,1, �� ����
        textClone.transform.localScale = Vector3.one;
        // ���ڰ� ����ٴ� ��� ����
        textClone.GetComponent<WorldToScreenPosition>().Setup(target);
        // UI�� ǥ�õǴ� ���� ����
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }
}
