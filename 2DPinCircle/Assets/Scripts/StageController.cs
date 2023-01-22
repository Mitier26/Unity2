using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner;
    [SerializeField] private int throwablePinCount;     // 클리어하기 위해 던져야 햐는 핀의 개수

    [SerializeField] private int stuckPincount;         // 스테이지 시작시 과녁에 배치되어 있는 핀의 수

    [SerializeField] private int[] stuckPinAngles;      // 핀들의 각도 배열

    // 게임화면 하단에 배치되는 전져야 하느 ㄴ핀들의 첫 번째 핀 위치
    private Vector3 firstTPinPosition = Vector3.down * 2;
    // 던져야 하는 핀들 사이의 배치 거리
    public float TPinDistance { get; private set; } = 1;

    private void Awake()
    {
        // 게임 하단에 배치되느 ㄴ던져야 하는 핀 오브젝트 생성
        for(int i = 0; i < throwablePinCount; ++i)
        {
            pinSpawner.SpawnThrowablePin(firstTPinPosition + Vector3.down * TPinDistance * i);
        }

        // 게임을 시작할 때 과녁에 배치되어 있는 핀 오브젝트 생성
        for(int i = 0; i < stuckPincount; ++i)
        {
            // 과녁에 배치되는 핀의 개수에 따라 일정한 간격으로 배치될 각도
            float angle = (360 /stuckPincount ) * i;

            pinSpawner.SpawnStuckPin(angle);
        }

        //for(int i = 0; i < stuckPinAngles.Length; ++i)
        //{
        //    pinSpawner.SpawnStuckPin(stuckPinAngles[i]);
        //}
    }
}
