using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    private float fadeTime = 0.1f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine("TwinkleLoop");
    }

    private IEnumerator TwinkleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeEffect(1, 0));

            yield return StartCoroutine(FadeEffect(0, 1));
        }
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0;
        float percent = 0;

        while(percent < 1.0f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // spriteRenderer.color.a = 1
            // 이렇게 값을 지정하는 것은 불가능
            // new Color을 사용해 설정해야한다.
            // 그래서 color 에 값을 저장하고 저장한 color값을 적용해 주어야한다.
            // 이유 : 프로퍼티로 되어있기 때문
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }
}
