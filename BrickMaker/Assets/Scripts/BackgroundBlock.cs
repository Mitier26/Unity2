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
    /// 블록을 드래그 하여 배경에 배치했을 때
    /// 배경블록의 색을 드래그 블록 색과 동일한 색으로 변경
    /// </summary>
    public void  FillBlock(Color color)
    {
        BlockState = BlockState.Fill;
        spriteRenderer.color = color;
    }

    /// <summary>
    /// 줄이 완성되면 배경 블록의 크기 변경하는 에니메이션을 재생한다.
    /// 배경 블록의 정보를 초기화
    /// 블록들이 가지고 있는 것이다. NonDragBlock
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

        // 블록 축소 에니메이션이 끝나면 블록의 색과 크기를 기본 상태로 돌린다.
        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
