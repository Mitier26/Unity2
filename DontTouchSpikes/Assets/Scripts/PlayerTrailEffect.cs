using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailEffect : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float duration = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Color originColor;
    private Vector3 originScale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        originScale = transform.localScale;
    }

    private void OnEnable()
    {
        spriteRenderer.color = originColor;
        transform.localScale = originScale;

        StartCoroutine(nameof(Process));

    }

    private IEnumerator Process()
    {
        float current = 0;
        float percent = 0;

        Vector3 start = transform.localScale;
        Vector3 end = transform.localScale * 0.2f;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / duration;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1, 0, curve.Evaluate(percent));
            spriteRenderer.color = color;

            transform.localScale = Vector3.Lerp(start, end, curve.Evaluate(percent));


            yield return null;
        }

        gameObject.SetActive(false);
    }
}
