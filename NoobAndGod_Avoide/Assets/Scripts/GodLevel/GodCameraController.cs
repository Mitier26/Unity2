using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;       // ���� ���
    [SerializeField]
    private float smooting = 15f;    // õõ�� �̵��ϱ� ���Ѱ�

    private Vector3 offset;         // �÷��̾�� ī�޶� ������ �Ÿ�

    public void SetOffset()
    {
        if(GodGameManager.Instance.isStart)
            offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (!GodGameManager.Instance.isStart) return;

        Vector3 targetCamPos = player.position + offset;
        if(targetCamPos.x > -6 && targetCamPos.x < 6)
        {
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smooting * Time.deltaTime);
        }
    }
}
