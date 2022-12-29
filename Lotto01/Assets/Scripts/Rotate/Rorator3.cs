using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorator3 : MonoBehaviour
{
    public void Rotation(float timer)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Cos(timer), 0));
    }

    public void RotateRotation(float timer)
    {
        transform.Rotate(new Vector3(0, Mathf.Cos(timer), 0));
    }

    // Rotate는 "방향, 속도" 를 가지고 있는 것이다.

    public void Forward(float timer)
    {
        transform.forward = new Vector3(Mathf.Cos(timer), 0, Mathf.Sin(timer));
        // 오브젝트가 바라보는 방향을 지정한다.
    }

    public void LookAt(float timer)
    {
        transform.LookAt(new Vector3(Mathf.Cos(timer), 0, Mathf.Sin(timer)));
        // 오브젝트가 바라보는 지점을 지정한다.
    }

    // 두 개의 의미 차이를 알아야 한다.
    // Forward는 오브젝트의 위치가 변해도 계속 주변을 돌고 있지만
    // LookAt는 오브젝트의 위치에 따라 회전하는 방향과 회전량이 달라진다.
    // LookAt는 눈동자 같은 것을 만들기에 좋다.
    // 특정 오브젝트를 따라 가는 만들기에 좋다.
}
