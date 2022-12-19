using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private float transitionTime = 1f;

    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;

    private void Awake()
    {
        startPosition = rectTransform.anchoredPosition;
    }

    public  void ShowUI()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        float currentTime = 0;
        while(currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, Vector3.zero, Mathf.Clamp01(currentTime / transitionTime));
            yield return null;
        }
    }
}
