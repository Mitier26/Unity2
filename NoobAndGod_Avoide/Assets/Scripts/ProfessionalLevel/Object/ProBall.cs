using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProBall : ProObject
{
    [SerializeField]
    private GameObject indicator, ball;     // 관리할 오브젝트

    public float startPositionX;            // 공이 생성외는 위치
    public Vector2 direction;               // 공의 이동 방향

    private void Start()
    {
        ball.transform.localPosition = new Vector3(startPositionX, 0, 0);
    }

    private void OnEnable()
    {
        ball.SetActive(false);
        indicator.SetActive(false);
        ball.transform.localPosition = new Vector3(startPositionX, transform.position.y, 0);
        StartCoroutine(Ball());

    }

    private IEnumerator Ball()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        indicator.SetActive(false);
        ball.SetActive(true);
        ball.GetComponent<Rigidbody2D>().velocity = direction;
    }

    private void LateUpdate()
    {
        // 볼이 경계 밖으로 나가면 풀로 반환
        if(ball.transform.localPosition.x > ProConstants.max.x || ball.transform.localPosition.x < ProConstants.min.x)
        {
            DestroyObject();
        }
    }
}
