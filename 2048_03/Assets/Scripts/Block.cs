using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Color[] blockColors;                // ����� ���ڿ� ���� ����� ����
    [SerializeField]
    private Image imageBlock;                   // ���� ������ ���� Image
    [SerializeField]
    private TextMeshProUGUI textBlockNumeric;   // ��� ���� ������ ���� text

    private int numeric;                        // ����� ������ ����
    private bool combine = false;

    // �̵�, ���� �ϱ� ���� �̵��ϴ� ��ǥ
    public Node Target { get; private set; }

    // ��� ��� �̵� �� �ѹ��� ���� �ϱ� ����
    // ������ ����� true
    public bool NeedDestroy { get; private set; } = false;

    public int Numeric
    {
        set
        {
            // ���� ���� ������ ����
            numeric = value;
            // ���ڰ� ����Ǹ� ǥ�õǴ� ���ڸ� �����Ѵ�.
            textBlockNumeric.text = value.ToString();
            // ���ڿ� �ش��ϴ� ������ �����Ѵ�.
            imageBlock.color = blockColors[(int)Mathf.Log(value, 2) - 1];

            // 2^n �϶� ����� ���� blockColor[n-1]
            // ����� 2 �̸� 0�� ������ �����;� �Ѵ�.
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
    /// ����� �̵�, ������ ���� Target ��ġ ���� �̵��� �� ȣ��
    /// ���� ��ġ���� Target.localPosition ��ġ���� 0.1�� �̵� �ϰ�
    /// �̵��� �Ϸ�Ǹ� OnAfterMove ȣ��
    /// </summary>
    public void StartMove()
    {
        float moveTime = 0.1f;
        StartCoroutine(OnLocalMoveAnimation(Target.localPosition, moveTime, OnAfterMove));
    }

    private void OnAfterMove()
    {
        // �̵��� �Ϸ��߱� ������ 
        if(Target != null)
        {
            // ���յǴ� ����̸�
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
