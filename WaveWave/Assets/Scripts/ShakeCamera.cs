using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;

    // �ܺο��� get �� ���� ���� �ϵ���
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;

    /// <summary>
    /// Main Camera ������Ʈ�� �����ϸ�
    /// ������ ������ �� �޸� �Ҵ�
    /// </summary>
    public ShakeCamera()
    {
        instance = this;
    }

    /// <summary>
    /// �ܺο��� ī�޶� ��鸲�� ������ �� ����ϴ� �޼ҵ�
    /// </summary>
    /// <param name="shakeTime">��鸲 ���� �ð�</param>
    /// <param name="shakeIntensity">��鸲 ����</param>
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByRotation()
    {
        // ��鸮�� ���� ��
        Vector3 startRotation = transform.eulerAngles;

        // Intensity�� ū ���� �ʿ��ϴ�.
        float power = 10f;

        while (shakeTime > 0.0f)
        {
            // ȸ�� �ϱ� ���ϴ� ��
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
