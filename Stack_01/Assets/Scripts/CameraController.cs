using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [Header("One Step Move Parameters")]
    [SerializeField]
    private float moveDistance = 0.1f;
    [SerializeField]
    private float oneStepMovetime = 0.25f;

    // 중요
    [Header("Game Over Parameters")]
    [SerializeField]
    private float gameOverAnimationTime = 1.5f;
    [SerializeField]
    private float limitMinY = 4;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void MoveOneStep()
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * moveDistance;

        StartCoroutine(OnMoveTo(start, end, oneStepMovetime));
    }

    public void GameOverAnimation(float lastCubeY, UnityAction action = null)
    {
        // 큐브 탑의 높이가 4 보다 작으 면 작동하지 않는다.
        if(limitMinY > lastCubeY)
        {
            if (action != null) action.Invoke();

            return;
        }

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, lastCubeY + 1, transform.position.z);

        // 카메라를 이동
        StartCoroutine(OnMoveTo(startPosition, endPosition, gameOverAnimationTime));

        float startSize = mainCamera.orthographicSize;
        float endSize = lastCubeY - 1;

        StartCoroutine(OnOrthographicSizeTo(startSize, endSize, gameOverAnimationTime, action));
    }

    private IEnumerator OnMoveTo(Vector3 start, Vector3 end, float oneStepMovetime)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / oneStepMovetime;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

    }

    private IEnumerator OnOrthographicSizeTo(float start, float end, float time, UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / time;

            mainCamera.orthographicSize = Mathf.Lerp(start, end, percent);

            yield return null;
        }

        if (action != null) action.Invoke();
    }
}
