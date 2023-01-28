using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState { Empty = 0, Fill = 1 };
public class BackgroundBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public BlockState BlockState { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BlockState = BlockState.Empty;
    }

    /// <summary>
    /// ����� �巡�� �Ͽ� ��濡 ��ġ���� ��
    /// ������� ���� �巡�� ��� ���� ������ ������ ����
    /// </summary>
    public void  FillBlock(Color color)
    {
        BlockState = BlockState.Fill;
        spriteRenderer.color = color;
    }

    /// <summary>
    /// ���� �ϼ��Ǹ� ��� ����� ũ�� �����ϴ� ���ϸ��̼��� ����Ѵ�.
    /// ��� ����� ������ �ʱ�ȭ
    /// ��ϵ��� ������ �ִ� ���̴�. NonDragBlock
    /// </summary>
    public void EmptyBlock()
    {
        BlockState = BlockState.Empty;

        StartCoroutine("ScaleTo", Vector3.zero);
    }

    private IEnumerator ScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;
        float time = 0.15f;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        // ��� ��� ���ϸ��̼��� ������ ����� ���� ũ�⸦ �⺻ ���·� ������.
        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
