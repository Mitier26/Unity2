using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Color[] blockColors;                // 블록이 숫자에 따라 변경될 색상
    [SerializeField]
    private Image imageBlock;                   // 색상 변경을 위한 Image
    [SerializeField]
    private TextMeshProUGUI textBlockNumeric;   // 블록 숫자 변경을 위한 text

    private int numeric;                        // 블록이 가지는 숫자
    private bool combine = false;

    // 이동, 병합 하기 위애 이동하는 목표
    public Node Target { get; private set; }

    // 모든 블록 이동 후 한번에 삭제 하기 위해
    // 삭제할 블록은 true
    public bool NeedDestroy { get; private set; } = false;

    public int Numeric
    {
        set
        {
            // 실제 숫자 데이터 변경
            numeric = value;
            // 숫자가 변경되면 표시되는 숫자를 변경한다.
            textBlockNumeric.text = value.ToString();
            // 숫자에 해당하는 색으로 변경한다.
            imageBlock.color = blockColors[(int)Mathf.Log(value, 2) - 1];

            // 2^n 일때 블록의 색상 blockColor[n-1]
            // 결과가 2 이면 0번 색상을 가져와야 한다.
            // 32 = 2^5   a = b^n
            // log2 32 = 5
        }

        get => numeric;
    }

    public void Setup()
    {
        Numeric = Random.Range(0, 100) < 90 ? 2 : 4;

        StartCoroutine(OnScaleAnimation(Vector3.one * 0.5f, Vector3.one, 0.15f));
    }

    public void MoveToNode(Node to)
    {
        Target = to;
        combine = false;
    }

    public void CombineToNode(Node to)
    {
        Target = to;
        combine = true;
    }

    /// <summary>
    /// 블록의 이동, 병합을 위해 Target 위치 까지 이동할 때 호출
    /// 현재 위치에서 Target.localPosition 위치까지 0.1초 이동 하고
    /// 이동이 완료되면 OnAfterMove 호출
    /// </summary>
    public void StartMove()
    {
        float moveTime = 0.1f;
        StartCoroutine(OnLocalMoveAnimation(Target.localPosition, moveTime, OnAfterMove));
    }

    private void OnAfterMove()
    {
        // 이동을 완료했기 때문에 
        if(Target != null)
        {
            // 병합되는 블록이면
            if(combine)
            {
                Target.placeBlock.Numeric *= 2;
                Target.placeBlock.StartPunchScale(Vector3.one * 0.25f, 0.15f, OnAfterPunchScale);

                gameObject.SetActive(false);
            }
            else
            {
                Target = null;
            }
        }
    }

    public void StartPunchScale(Vector3 punch, float time, UnityAction action = null)
    {
        StartCoroutine(OnPunchScale(punch, time, action));
    }

    private void OnAfterPunchScale()
    {
        Target = null;
        NeedDestroy = true;
    }

    private IEnumerator OnPunchScale(Vector3 punch, float time, UnityAction action)
    {
        Vector3 current = Vector3.one;

        yield return StartCoroutine(OnScaleAnimation(current, current + punch, time * 0.5f));

        yield return StartCoroutine(OnScaleAnimation(current + punch, current, time * 0.5f));

        if (action != null) action.Invoke();
    }

    private IEnumerator OnScaleAnimation(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current+= Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private IEnumerator OnLocalMoveAnimation(Vector3 end, float time, UnityAction action)
    {
        float current = 0;
        float percent = 0;

        Vector3 start = GetComponent<RectTransform>().localPosition;

        while(percent <1 )
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if(action != null) action.Invoke();
    }
}
