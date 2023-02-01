using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountingEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float effectTime;

    private TMP_Text effectText;

    private void Awake()
    {
        effectText = GetComponent<TMP_Text>();
    }

    public void Play(int start, int end, UnityAction action = null)
    {
        StartCoroutine(Process(start, end, action));
    }

    private IEnumerator Process(int start, int end, UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / effectTime;

            effectText.text = Mathf.Lerp(start, end, current).ToString("F0");

            yield return null;
        }

        action.Invoke();
    }
}
