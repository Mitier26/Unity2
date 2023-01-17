using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPosLeft, startPosRight;   // 왼쪽, 오른쪽 시작 값

    [SerializeField] float moveTime;                                // 이동하는 시간

    private void Start()
    {
        // 오브젝트가 생성되면 랜덤을 이동하여 왼쪽, 오른쪽중 하나를 선택한다.
        transform.position = Random.Range(0,1) > 0.5f ? startPosLeft : startPosRight;
    }

    // 메니저에서 소환을 할 때 작동하게 하는 함수
    public void MoveToPos(Vector3 targetPos)
    {
        StartCoroutine(IMoveToPos(targetPos));
    }

    // 이동하는 기능
    public IEnumerator IMoveToPos(Vector3 targetPos)
    {
        float timeElapsed = 0f;
        while (timeElapsed < moveTime)
        {
            timeElapsed += Time.fixedDeltaTime;
            // 시작 지점에서 목표지점 까지 이동하는 명령이다.
            // targerPos를 이동하는 방향으로 계속 증가하면 계속 이동한다.
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveTime * timeElapsed);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        transform.position = targetPos;
    }
}
