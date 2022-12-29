using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rotator2 : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        // transform.rotation = new Vector3(10, 20, 0);
        // 흔히 사용하는 각도 : 오일러 각도를 사용하면 rotation 을 작동할 수 없다.
        // rotation 은 쿼터니온 각도를 사용하기 때문이다.
        transform.eulerAngles = new Vector3(50, 20, 0);
        Vector3 rotation = transform.eulerAngles;

        // 유니티 인스펙터에서 보이는 것은 오일러 각도이다.
        // 이것은 사용자 친화적은 각도
        // 오일러 각도는 " 짐벌락 " 현상이 있다.
        // x 각도를 90도로 하고 y 각과 z 각을 변경하면 y와 z가 같이 각도로 회전한다.
        // 오일러는 사로 다른 각도를 보간할 때 문제가 있다.
        // 쿼터니언은 위의 문제를 해결하기 위한 것으로 4 차원의 숫자 이다.
        // 쿼터니언의 단점은 직관적이지 않아 복잡할 수 있다는 것이다.
    }

    public void Rotator1()
    {
        Vector3 direction  = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        Convert();
        RotationChange();
    }

    public Vector3 convertedRotation;

    public void Convert()
    {
        convertedRotation = transform.rotation.eulerAngles;
        // 인스팩터에 변하는 각도와 
        // Convert 의 각도가 많이 차이 난다.
    }

    Vector3 rotationChange;
    public float sensitivity = 10;

    public void RotationChange()
    {
        rotationChange += new Vector3(Input.GetAxis("Vertical") * sensitivity, Input.GetAxis("Horizontal") * sensitivity, 0) * Time.deltaTime;
        transform.eulerAngles = rotationChange;
    }

    // Rotate
    // 벡터3 값을 전달하여 특정 축을 중심으로 해당 객체를 상대적으로 회전할 수 있다.
    // Rotate(Vector3 eulers)
    // Rotate(float x, float y, float y)
    // 해당하는 축을 중심으로 회전하는 각도
    // Rotate(Vector3 axis, float angle)
    // 회전할 축을 지정하고 해당 각도로 회전

    public void Rotate1()
    {
        transform.Rotate(Vector3.up, 90);

        transform.Rotate(0, 10 * Time.deltaTime, 0, Space.World);
        // y축을 기분으로 매초 10도씩 회전
    }

    public float degrees;
    public Transform target2;
    public void AroundRotate()
    {
        transform.RotateAround(target.position, transform.up, degrees * Time.deltaTime);
        // 타켓을 중심으로 위쪽 방향 축으로 매초 degrees 만큼 회전한다.
    }

    public Transform objectToOrbit;
    public float radius;

    public void CameraRotateAround()
    {
        radius += Input.mouseScrollDelta.y;
        transform.position = objectToOrbit.position - (transform.forward * radius);
        transform.RotateAround(objectToOrbit.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
        transform.RotateAround(objectToOrbit.position, Vector3.right, Input.GetAxis("Mouse Y") * sensitivity);
        // 카메라 회전을 구현
    }

    public Transform origin;
    public void RotateVector()
    {
        Vector3 originalVector = origin.forward * 5;
        Vector3 offset = Quaternion.Euler(0, 30, 0) * originalVector;
        transform.position = origin.position + offset;
    }

    public Transform objectToLookAt;
    public float maxTurnSpeed = 50;

    public void RotateTowardsObject()
    {
        Vector3 direction = objectToLookAt.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), maxTurnSpeed * Time.deltaTime);
    }

    // 시작과 끝의 각도를 지정하고 
    // 일정 시간동안 각도를 변화 시킬 수 있다.
    public float revs;
    Quaternion rotation1 = Quaternion.Euler(0,0,0);
    Quaternion rotation2 = Quaternion.Euler(0,0,-180);

    public void SpeedMeter()
    {
        transform.rotation = Quaternion.Slerp(rotation1, rotation2, revs);
    }

    public Transform objectToLook;
    public float smoothing = 5;

    public void LerpRotate()
    {
        Vector3 direction = objectToLook.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * smoothing);
    }

    public Transform target1;
    public float smoothTime = 1.5f;
    Vector3 velocity;
    public void SmoothDamp()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }

    public Transform objectToLookAt1;
    public float smoothTime1 = 1;

    public Vector3 newRotation;
    float xVel;
    float yVel;
    float zVel;

    public void SmoothRotateTowards()
    {
        Vector3 direction = objectToLookAt1.position - transform.position;
        Vector3 targetRotation = Quaternion.LookRotation(direction).eulerAngles;

        newRotation = new Vector3(
            Mathf.SmoothDampAngle(newRotation.x, targetRotation.x, ref xVel, smoothTime),
            Mathf.SmoothDampAngle(newRotation.y, targetRotation.y, ref yVel, smoothTime),
            Mathf.SmoothDampAngle(newRotation.z, targetRotation.z, ref zVel, smoothTime));

        transform.eulerAngles = newRotation;
    }
}
