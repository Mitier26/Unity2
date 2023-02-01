using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 10f)]
    private float effectTime;

    private TMP_Text effectText;

    private void Awake()
    {
        effectText = GetComponent<TMP_Text>();
    }

    public void Play(float start, float end)
    {
        StartCoroutine(Process(start, end));
    }

    private IEnumerator Process(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / effectTime;

            effectText.fontSize = Mathf.Lerp(start, end, percent);

            yield return null;
        }
    }
}
