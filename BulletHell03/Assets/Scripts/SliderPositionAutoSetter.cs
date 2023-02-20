using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 35.0f;    // 슬라이더의 위치
    private Transform targetTransform;                  // 따라 다닐 것
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // 적이 사라지면 슬라이더도 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        rectTransform.position = screenPosition + distance;
    }
}
