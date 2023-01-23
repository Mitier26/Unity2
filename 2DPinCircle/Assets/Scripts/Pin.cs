using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject square;         // 핀의 막대기

    [SerializeField] private float moveTime = 0.2f;     // 핀의 이동 시간

    private StageController stageController;            // 게임오버 실행을 위해

    public void SetUp(StageController stageController)
    {
        this. stageController = stageController;
    }


    public void SetInPinStuckToTarget()
    {
        StopCoroutine("MoveTo");
        // 핀의 막대기 활성화
        square.SetActive(true);
    }

    public void MoveonStep(float moveDistance)
    {
        StartCoroutine("MoveTo", moveDistance);
    }

    private IEnumerator MoveTo(float moveDistance)
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * moveDistance;

        float current = 0f;
        float percent = 0f;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pin"))
        {
            stageController.GameOver();
        }
    }
}
