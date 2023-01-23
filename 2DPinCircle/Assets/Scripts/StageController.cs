using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner;

    [SerializeField] private Camera mainCamera;         // ��� �� ������ ���� ī�޶�
    [SerializeField] private Rotator rotatorTarget;     // ���� ��ġ�Ǵ� ����

    [SerializeField] private Rotator rotatorIndexPanel; // �� ���ڰ� ��ġ�Ǿ� �ִ� ������Ʈ

    [SerializeField] private MainMenuUI mainMenuUI;     // ���� �޴� �̵��� ����

    [SerializeField] private int throwablePinCount;     // Ŭ�����ϱ� ���� ������ ��� ���� ����

    [SerializeField] private int stuckPincount;         // �������� ���۽� ���ῡ ��ġ�Ǿ� �ִ� ���� ��

    [SerializeField] private int[] stuckPinAngles;      // �ɵ��� ���� �迭

    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioGameClear;
    [SerializeField] private AudioSource audioSource;

    // ����ȭ�� �ϴܿ� ��ġ�Ǵ� ������ �ϴ� ���ɵ��� ù ��° �� ��ġ
    private Vector3 firstTPinPosition = Vector3.down * 2;
    // ������ �ϴ� �ɵ� ������ ��ġ �Ÿ�
    public float TPinDistance { get; private set; } = 1;
    // ���� ���� �Ǿ��� �� ����� ��
    private Color failBackgroundColor = new Color(0.4f, 0.1f, 0.1f);
    private Color clearBackgroundColor = new Color(0f, 0.5f, 0.25f);

    // ���� ��� ���� ����
    public bool IsGameOver { get; set; } = false;
    public bool IsGameStart { get; set; } = false;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();

        pinSpawner.Setup();

        // ���� �ϴܿ� ��ġ�Ǵ� �������� �ϴ� �� ������Ʈ ����
        for(int i = 0; i < throwablePinCount; ++i)
        {
            pinSpawner.SpawnThrowablePin(firstTPinPosition + Vector3.down * TPinDistance * i, throwablePinCount-i);
        }

        // ������ ������ �� ���ῡ ��ġ�Ǿ� �ִ� �� ������Ʈ ����
        for(int i = 0; i < stuckPincount; ++i)
        {
            // ���ῡ ��ġ�Ǵ� ���� ������ ���� ������ �������� ��ġ�� ����
            float angle = (360 /stuckPincount ) * i;

            pinSpawner.SpawnStuckPin(angle, throwablePinCount+1+i);
        }

        // ������ �����Ͽ� ���� ��ġ
        //for(int i = 0; i < stuckPinAngles.Length; ++i)
        //{
        //    pinSpawner.SpawnStuckPin(stuckPinAngles[i]);
        //}
    }

    public void GameOver()
    {
        IsGameOver = true;
        // ��� �� ����
        mainCamera.backgroundColor = failBackgroundColor;
        // ȸ�� ����
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
            // ������ ���� �� �� ���� �� �ٷ� Ŭ���� ���� �ʰ� �ϱ� ����
            StartCoroutine("GameClear");
        }
    }

    private IEnumerator GameClear()
    {
        // ���� �浹 �˻� ���Ŀ� ���� �� �� �ֵ���
        yield return new WaitForSeconds(0.1f);

        // �浹�ؼ� ���� ������ �Ǹ� �ڷ�ƾ ����
        if(IsGameOver)
        {
            yield break;
        }

        // ī�޶� ��� �� ����
        mainCamera.backgroundColor = clearBackgroundColor;
        // ���� ������ ȸ��
        rotatorTarget.RotateFast();
        // �ǳ� ������ ȸ��
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
