using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public  Transform target;
    public Rigidbody rb;
    public float t = 0.1f;
    public float speed = 0.1f;
    public float force;
    public Vector3 moveDirection;

    private void FixedUpdate()
    {
    }

    public void SetPosition()
    {
        transform.position = target.position;
        // 순간 이동한다.
    }

    public void LerpPosition()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.Lerp(a, b, t);
        // 스무스 하게 움직인다.
    }

    public void MoveTowardPosition()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.MoveTowards(a, b, speed);
        // 일정한 속도로 움직인다.
    }

    public void MoveTowardandLerpPosition()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,t), speed);
    }

    public void RigidbodyPosition()
    {
        Vector3 f = target.position - transform.position;
        f = f.normalized;
        f = f * force;
        rb.AddForce(f);
    }

    public void TranslataPosition()
    {
        transform.Translate(moveDirection, Space.World);
        // 지정한 방향으로 계속 이동한다.
        // Self 이면 자신의 방향을 기준으로 이동한다.
    }
}
