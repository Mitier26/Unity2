using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private StageController stageController;           
    [SerializeField] private GameObject pinPrefab;                      // 핀 프리팹
    [SerializeField] private GameObject textPinIndexPrefab;             // 핀에 숫자를 표시하는 UI
    [SerializeField] private Transform textParent;                      // UI의 부모

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform;                 // 과녁의 Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2;   // 과녁의 위치
    [SerializeField] private float targetRadius = 0.8f;                 // 과녁의 반지름
    [SerializeField] private float pinLength = 1.5f;                    // 막대기의 길이

    [Header("Throwable Pin")]
    [SerializeField] private float bottomAngle = 270f;                  // 과녁에 배치되는 핀의 각도
    private List<Pin> throwablePins;                                    // 하단에 생성되는 핀 리스트

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
            // 리스트에 있는 0 번 핀을 과녁에 배치
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            // 리스트 0번에 있는 요소 삭제
            throwablePins.RemoveAt(0);

            for(int i = 0; i < throwablePins.Count; ++i)
            {
                throwablePins[i].MoveonStep(stageController.TPinDistance);
            }

            // 핀이 배치될 때 핀의 수를 감소
            stageController.DecreaseThrowablePin();

        }
    }

    public void SpawnThrowablePin(Vector3 position, int index)
    {
        // 던질 핀 오브젝트 생성
        GameObject clone = Instantiate(pinPrefab, position, Quaternion.identity);
        // 생성된 핀의 정보를 
        Pin pin = clone.GetComponent<Pin>();
        // 생성된 핀 오브젝트의 핀 스크립트를 리스트에 추가

        pin.SetUp(stageController);

        throwablePins.Add(pin);

        // 핀에 표시되는 UI생성
        SpawnTextUI(clone.transform, index);
    }

    public void SpawnStuckPin(float angle, int index)
    {
        // 과녁에 배치된 핀 오브젝트 생성
        GameObject clone = Instantiate(pinPrefab);

        Pin pin = clone.GetComponent<Pin>();
        pin.SetUp(stageController);

        // 위에서 생성된 핀이 과녁에 배치 되도록 설정
        SetInPinStuckToTarget(clone.transform, angle);

        SpawnTextUI(clone.transform, index);
    }

    public void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // 핀의 위치 : 해당 각도에 핀이 배치되었을 때의 각도
        pin.position = Utils.GetPositionFromAngle(targetRadius + pinLength, angle) + targetPosition;

        // 핀의 회전
        pin.rotation = Quaternion.Euler(0, 0, angle);

        // 핀의 부모 설정 : 부모와 함께 회전하기 위해
        pin.SetParent(targetTransform);

        // 핀의 막대기 활성화
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }

    private void SpawnTextUI(Transform target, int index)
    {
        // 숫자를 표시하 오브젝트 생성
        GameObject textClone = Instantiate(textPinIndexPrefab);
        // 숫자의 부모를 핀으로 설정
        textClone.transform.SetParent(textParent);
        // 숫자의 크기는 1,1,1, 로 설정
        textClone.transform.localScale = Vector3.one;
        // 숫자가 따라다닐 대상 설정
        textClone.GetComponent<WorldToScreenPosition>().Setup(target);
        // UI에 표시되는 내용 설정
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }
}
