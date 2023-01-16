using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    // ������ ���� �´ٸ� ���ϸ��̼��� ����Ѵ�.
    // ������ ���� ����� ���� ������ ���ϸ��̼��� ����ؾ� �Ѵ�.

    // ���� �����Ѵ�.
    // ���ϸ��̼��� ����Ѵ�. ���İ��� ��ȭ
    // �ش� �������� �ı��Ѵ�.

    [SerializeField] private Color currentColor;
    [SerializeField] private float destroyTime;     // �ı��ð�
    public void Init(Color color)
    {
        currentColor = color;

        // ���ϸ��̼� ����
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        float speed = 1 / destroyTime;
        float timeElapsed = 0;

        // ����Ǵ� ���ϸ��̼��� ũ��, ������ ���ؾ��Ѵ�.
        Vector3 startScale = Vector3.one * 0.64f;
        Vector3 endScale = Vector3.one * 1.32f;
        Vector3 scaleOffset = endScale- startScale;
        Vector3 currentScale = startScale;

        // ���� ��ȭ
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
