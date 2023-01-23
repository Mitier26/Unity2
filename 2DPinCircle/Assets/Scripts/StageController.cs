using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner;

    [SerializeField] private Camera mainCamera;         // 배경 색 변경을 위한 카메라
    [SerializeField] private Rotator rotatorTarget;     // 핀이 배치되는 과녁

    [SerializeField] private Rotator rotatorIndexPanel; // 핀 숫자가 배치되어 있는 오브젝트

    [SerializeField] private MainMenuUI mainMenuUI;     // 메인 메뉴 이동을 위해

    [SerializeField] private int throwablePinCount;     // 클리어하기 위해 던져야 햐는 핀의 개수

    [SerializeField] private int stuckPincount;         // 스테이지 시작시 과녁에 배치되어 있는 핀의 수

    [SerializeField] private int[] stuckPinAngles;      // 핀들의 각도 배열

    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioGameClear;
    [SerializeField] private AudioSource audioSource;

    // 게임화면 하단에 배치되는 전져야 하느 ㄴ핀들의 첫 번째 핀 위치
    private Vector3 firstTPinPosition = Vector3.down * 2;
    // 던져야 하는 핀들 사이의 배치 거리
    public float TPinDistance { get; private set; } = 1;
    // 게임 오버 되었을 때 배경의 색
    private Color failBackgroundColor = new Color(0.4f, 0.1f, 0.1f);
    private Color clearBackgroundColor = new Color(0f, 0.5f, 0.25f);

    // 게임 제어를 위한 변수
    public bool IsGameOver { get; set; } = false;
    public bool IsGameStart { get; set; } = false;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();

        pinSpawner.Setup();

        // 게임 하단에 배치되느 ㄴ던져야 하는 핀 오브젝트 생성
        for(int i = 0; i < throwablePinCount; ++i)
        {
            pinSpawner.SpawnThrowablePin(firstTPinPosition + Vector3.down * TPinDistance * i, throwablePinCount-i);
        }

        // 게임을 시작할 때 과녁에 배치되어 있는 핀 오브젝트 생성
        for(int i = 0; i < stuckPincount; ++i)
        {
            // 과녁에 배치되는 핀의 개수에 따라 일정한 간격으로 배치될 각도
            float angle = (360 /stuckPincount ) * i;

            pinSpawner.SpawnStuckPin(angle, throwablePinCount+1+i);
        }

        // 각도를 지정하여 핀을 배치
        //for(int i = 0; i < stuckPinAngles.Length; ++i)
        //{
        //    pinSpawner.SpawnStuckPin(stuckPinAngles[i]);
        //}
    }

    public void GameOver()
    {
        IsGameOver = true;
        // 배경 색 변경
        mainCamera.backgroundColor = failBackgroundColor;
        // 회전 정지
        rotatorTarget.Stop();

        audioSource.clip = audioGameOver;
        audioSource.Play();

        StartCoroutine("StageExit", 0.5f);
    }

    public void DecreaseThrowablePin()
    {
        throwablePinCount--;

        if(throwablePinCount == 0)
        {
            // 마지막 핀을 발 살 했을 때 바로 클리어 되지 않게 하기 위해
            StartCoroutine("GameClear");
        }
    }

    private IEnumerator GameClear()
    {
        // 핀의 충돌 검사 이후에 실행 할 수 있도록
        yield return new WaitForSeconds(0.1f);

        // 충돌해서 게임 오버가 되면 코루틴 정지
        if(IsGameOver)
        {
            yield break;
        }

        // 카메라 배경 색 변경
        mainCamera.backgroundColor = clearBackgroundColor;
        // 과녁 빠르게 회전
        rotatorTarget.RotateFast();
        // 판넬 빠르게 회전
        rotatorTarget.RotateFast();

        int index = PlayerPrefs.GetInt("StageLevel");
        PlayerPrefs.SetInt("StageLevel", index + 1);

        audioSource.clip = audioGameClear;
        audioSource.Play();

        StartCoroutine("StageExit", 1f);
    }

    private IEnumerator StageExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        mainMenuUI.StageExit();
    }
}
