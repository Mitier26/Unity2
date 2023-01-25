using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float increaseAmount;           // 이동 속도 증가량
    [SerializeField]
    private float increaseCycleTime;        // 이동 속도 증가 시간
    private Vector3 moveDirection;

    public Vector3 MoveDirection => moveDirection;

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(increaseCycleTime);

            moveSpeed += increaseAmount;
        }
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
