using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    // 선택한 것이 맞다면 에니메이션을 출력한다.
    // 선택한 것의 색상과 같은 색상의 에니메이션을 출력해야 한다.

    // 색을 변경한다.
    // 에니메이션을 출력한다. 알파값의 변화
    // 해당 오브젝를 파괴한다.

    [SerializeField] private Color currentColor;
    [SerializeField] private float destroyTime;     // 파괴시간
    public void Init(Color color)
    {
        currentColor = color;

        // 에니메이션 실행
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        float speed = 1 / destroyTime;
        float timeElapsed = 0;

        // 재생되는 에니메이션의 크기, 색상을 정해야한다.
        Vector3 startScale = Vector3.one * 0.64f;
        Vector3 endScale = Vector3.one * 1.32f;
        Vector3 scaleOffset = endScale- startScale;
        Vector3 currentScale = startScale;

        // 색의 변화
        Color startColor = currentColor;
        startColor.a = 0.8f;
        Color endColor = currentColor;
        endColor.a = 0.2f;
        Color colorOffset = endColor - startColor;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = startColor;

        while(timeElapsed < 1)
        {
            timeElapsed += speed * Time.deltaTime;

            currentScale = startScale + timeElapsed * scaleOffset;
            transform.localScale = currentScale;

            sr.color = startColor + timeElapsed * colorOffset;

            yield return null;
        }

        Destroy(gameObject, destroyTime);


    }
}
