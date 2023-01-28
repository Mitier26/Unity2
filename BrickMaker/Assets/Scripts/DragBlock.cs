using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;

    [SerializeField]
    private AnimationCurve curveMovement;           // �̵� ���� �׷���

    [SerializeField]
    private AnimationCurve cureveScale;             // ����� ũ�� ���� �׷���
    
    private float appearTime = 0.5f;                // ����� ������ �� �ҿ�Ǵ� �ð�
    private float returnTime = 0.1f;                // ����� ���� ��ġ�� ���ư� �� �ð�

    [field:SerializeField]      // ������Ƽ�� �ν��彺�Ϳ� ���� �ϱ� ���� field ����Ѵ�.
    public Vector2Int BlockCount { get; private set; }  // �ڽ� ����� ����

    /// �巡�� ����� ���� �� ��濡 ��ġ ���� ���� Ȯ�� �ϱ� ���� �ڽ� ����� ��ǥ
    public Color Color { get; private set; }
    public Vector3[] ChildBlocks { get; private set; }      // �ڽ� ����� ���� ��ǥ

    public void Setup(BlockArrangeSystem blockArrangeSystem, Vector3 parentPosition)
    {
        this.blockArrangeSystem = blockArrangeSystem;

        // �ڽ��� �� ������ �����Ѵ�.
        // �ڽ��� ��� ���� ���� ����ϰ� �־� �ƹ� �ڽ��� ���� ������ �͵� ����� ����.
        Color = GetComponentInChildren<SpriteRenderer>().color;

        // ��� ���� ũ�Ⱑ �ٸ��� ������ ũ�⸦ �� ���� �� �ش�.
        ChildBlocks = new Vector3[transform.childCount];

        // �� �ڽ��� ��ǥ�� �����Ѵ�.
        for(int i  = 0; i < ChildBlocks.Length; ++i)
        {
            ChildBlocks[i] = transform.GetChild(i).localPosition;
        }


        StartCoroutine(OnMoveTo(parentPosition, appearTime));
    }

    /// <summary>
    /// �ش� ������Ʈ�� Ŭ���� �� 1ȸ �۵�
    /// </summary>
    private void OnMouseDown()
    {
        // ũ�Ⱑ 1 �� ����
        // ��� ����� ũ��� 1 �̴�.
        // ������ ����� ũ��� 0.5 �̴�.
        // ����� ��ϰ� ũ�⸦ ���߱� ���� Vector3.one �� �ߴ�.
        StopCoroutine("OnScaleTo");
        StartCoroutine("OnScaleTo", Vector3.one);
    }

    /// <summary>
    /// �ش� ������Ʈ�� �巡���� �� �� ������ �۵�
    /// </summary>
    private void OnMouseDrag()
    {
        // ����� �巡���� ��
        // �巡�� �� ����� ��ġ�� ��� ���� ���̴�.
        // ����� y�� �� ����� ��  0.5 ��ŭ �ø��� 1�� �� �ø���.
        // z �� 10 �� ������ ī�޶��� ��ġ�� -10 �̱� ����
        Vector3 gap = new Vector3(0, BlockCount.y * 0.5f + 1, 10);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
        // ȭ�鿡 ���̴� ��� ��ǥ�� ���� ��ǥ�� ��ȯ
    }

    /// <summary>
    /// Ŭ���� ����
    /// </summary>
    private void OnMouseUp()
    {
        // ���� ���
        // ����� ������ Ȧ��, ¦�� �� �� �ٸ��� ���
        // RoundToInt : ���� �ݿø��Ѵ�.
        float x = Mathf.RoundToInt(transform.position.x - BlockCount.x % 2 * 0.5f) + BlockCount.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y - BlockCount.y % 2 * 0.5f) + BlockCount.y % 2 * 0.5f;

        transform.position = new Vector3(x, y, 0);

        bool isSuccess = blockArrangeSystem.TryArrangementBlock(this);

        // OnScaleTo�� �۵��ǰ� ������ �۵��� �����ϰ�
        // ũ�⸦ 0.5�� �Ѵ�.
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
