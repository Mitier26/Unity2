using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move4 : MonoBehaviour
{
    public bool isTranslate;
    public float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (isTranslate)
            MoveTranslate(new Vector2(0, Mathf.Cos(timer)));
        else
            MovePosition(new Vector2(transform.position.x, Mathf.Cos(timer)));
        // x 의 값을 다르게 해야 한다.
        // Position은 "위치" Translata는 "방향, 속도" 이기 때문이다.
        // Translate x 값을 2 로 하면 매 프레임 마다 x 방향으로 2씩 증가한다.
        // 2,4,6,8,10.....
        // Cos(timer) 이기 때문에 시작 1 가속도 다음 1(위치) 에 0.9를 더한 1.9(위치) 로 되어 많이 이동된다.

    }
    public void MovePosition(Vector2 position)
    {
        transform.position = position;
    }

    public void MoveTranslate(Vector2 position)
    {
        transform.Translate(position);
    }
}
