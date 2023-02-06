using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern03 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningImage;
    [SerializeField]
    private Transform boom;
    [SerializeField]
    private GameObject boomEffect;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));   
    }

    private void OnDisable()
    {
        boom.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // 패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        // 경고 이미지 활성/ 비활성
        warningImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage.SetActive(false);

        // 폭탄 오브젝트 활성활, 이동
        yield return StartCoroutine(nameof(MoveUp));

        // 폭탄 이펙트 활성/ 비활성
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        boomEffect.SetActive(false);

        gameObject.SetActive(false);
    }

    private IEnumerator MoveUp()
    {
        // 목표 위치
        float boomDestinationY = 0;

        boom.gameObject.SetActive(true);

        while (true)
        {
            if(boom.transform.position.y >= boomDestinationY)
            {
                boom.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
