using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;       // 따라갈 대상
    [SerializeField]
    private float smooting = 15f;    // 천천히 이동하기 위한것

    private Vector3 offset;         // 플레이어와 카메라 사이의 거리

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
