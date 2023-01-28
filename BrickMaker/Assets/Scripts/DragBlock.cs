using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;

    [SerializeField]
    private AnimationCurve curveMovement;           // 이동 제어 그래프

    [SerializeField]
    private AnimationCurve cureveScale;             // 블록의 크기 제어 그래프
    
    private float appearTime = 0.5f;                // 블록이 등장할 때 소요되는 시간
    private float returnTime = 0.1f;                // 블록이 원래 위치로 돌아갈 때 시간

    [field:SerializeField]      // 프로퍼티를 인스펙스터에 제어 하기 위해 field 사용한다.
    public Vector2Int BlockCount { get; private set; }  // 자식 블록의 개수

    /// 드래그 블록의 색상 과 배경에 배치 가능 한지 확인 하기 위한 자식 블록의 좌표
    public Color Color { get; private set; }
    public Vector3[] ChildBlocks { get; private set; }      // 자식 블록의 지역 좌표

    public void Setup(BlockArrangeSystem blockArrangeSystem, Vector3 parentPosition)
    {
        this.blockArrangeSystem = blockArrangeSystem;

        // 자식의 색 정보를 저장한다.
        // 자식은 모두 같은 색을 사용하고 있어 아무 자식의 색을 가지고 와도 상관이 없다.
        Color = GetComponentInChildren<SpriteRenderer>().color;

        // 블록 마다 크기가 다르기 때문에 크기를 재 설정 해 준다.
        ChildBlocks = new Vector3[transform.childCount];

        // 각 자식의 좌표를 저장한다.
        for(int i  = 0; i < ChildBlocks.Length; ++i)
        {
            ChildBlocks[i] = transform.GetChild(i).localPosition;
        }


        StartCoroutine(OnMoveTo(parentPosition, appearTime));
    }

    /// <summary>
    /// 해당 오브젝트를 클릭할 때 1회 작동
    /// </summary>
    private void OnMouseDown()
    {
        // 크기가 1 인 이유
        // 배경 블록의 크기는 1 이다.
        // 생성된 블록의 크기는 0.5 이다.
        // 배경의 블록과 크기를 맞추기 위해 Vector3.one 을 했다.
        StopCoroutine("OnScaleTo");
        StartCoroutine("OnScaleTo", Vector3.one);
    }

    /// <summary>
    /// 해당 오브젝트를 드래그할 대 매 프레임 작동
    /// </summary>
    private void OnMouseDrag()
    {
        // 블록을 드래그할 때
        // 드래그 한 블록의 위치를 잡기 위한 것이다.
        // 블록의 y축 을 블록의 반  0.5 만큼 올리고 1을 더 올린다.
        // z 가 10 인 이유는 카메라의 위치가 -10 이기 때문
        Vector3 gap = new Vector3(0, BlockCount.y * 0.5f + 1, 10);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
        // 화면에 보이는 블록 좌표를 월드 좌표로 변환
    }

    /// <summary>
    /// 클릭의 종료
    /// </summary>
    private void OnMouseUp()
    {
        // 스냅 기능
        // 블록의 개수가 홀수, 짝수 일 때 다르게 계산
        // RoundToInt : 값을 반올림한다.
        float x = Mathf.RoundToInt(transform.position.x - BlockCount.x % 2 * 0.5f) + BlockCount.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y - BlockCount.y % 2 * 0.5f) + BlockCount.y % 2 * 0.5f;

        transform.position = new Vector3(x, y, 0);

        bool isSuccess = blockArrangeSystem.TryArrangementBlock(this);

        // OnScaleTo가 작동되고 있으면 작동을 중지하고
        // 크기를 0.5로 한다.
        if(isSuccess == false)
        {
            StopCoroutine("OnScaleTo");
            StartCoroutine("OnScaleTo", Vector3.one * 0.5f);
            StartCoroutine(OnMoveTo(transform.parent.position, returnTime));
        }
       
    }

    private IEnumerator OnMoveTo(Vector3 end, float time)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, curveMovement.Evaluate(percent));

            yield return null;
        }
    }

    private IEnumerator OnScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / returnTime;

            transform.localScale = Vector3.Lerp(start, end, cureveScale.Evaluate(percent));

            yield return null;
        }
    }
}
