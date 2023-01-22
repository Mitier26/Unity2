using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private GameObject pinPrefab;      // 핀 프리팹

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform;                 // 과녁의 Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2;   // 과녁의 위치
    [SerializeField] private float targetRadius = 0.8f;                 // 과녁의 반지름
    [SerializeField] private float pinLength = 1.5f;                    // 막대기의 길이

    public void SpawnThrowablePin(Vector3 position)
    {
        // 던질 준비 하는 핀 생성
        Instantiate(pinPrefab, position, Quaternion.identity);
    }

    public void SpawnStuckPin(float angle)
    {
        // 과녁에 배치된 핀 오브젝트 생성
        GameObject clone = Instantiate(pinPrefab);

        // 위에서 생성된 핀이 과녁에 배치 되도록 설정
        SetInPinStuckToTarget(clone.transform, angle);
    }

    public void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // 핀의 위치 : 해당 각도에 핀이 배치되었을 때의 각도
        pin.position = Utils.GetPositionFromAngle(targetRadius+ pinLength, angle) + targetPosition;

        // 핀의 회전
        pin.rotation = Quaternion.Euler(0, 0, angle);

        // 핀의 부모 설정 : 부모와 함께 회전하기 위해
        pin.SetParent(targetTransform);

        // 핀의 막대기 활성화
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }
}
