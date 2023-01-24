using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;

    // 외부에서 get 만 접근 가능 하도록
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;

    /// <summary>
    /// Main Camera 오브젝트에 적용하면
    /// 게임을 실행할 때 메모리 할당
    /// </summary>
    public ShakeCamera()
    {
        instance = this;
    }

    /// <summary>
    /// 외부에서 카메라 흔들림을 조작할 때 사용하는 메소드
    /// </summary>
    /// <param name="shakeTime">흔들림 지속 시간</param>
    /// <param name="shakeIntensity">흔들림 강도</param>
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByRotation()
    {
        // 흔들리기 전의 값
        Vector3 startRotation = transform.eulerAngles;

        // Intensity에 큰 값이 필요하다.
        float power = 10f;

        while (shakeTime > 0.0f)
        {
            // 회전 하기 원하는 축
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);

            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(startRotation);
    }
}
