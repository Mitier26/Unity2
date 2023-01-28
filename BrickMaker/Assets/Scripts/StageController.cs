using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private BackgroundBlockSpawner backgroundBlockSpawner;                  // ����� ��ϻ�����
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner;                  // ������ ��ϻ�����
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;                              // ��ġ�� �� �ִ� ��ϻ�����

    private BackgroundBlock[] backgroundBlocks;                             // ������� ����� ���� 
    private int currentDragBlockCount;                                      // �巡�� ����� ��, ������ �ٽ� �����ϱ� ����

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);        // ������� ����, ���� ���� ũ��
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);           // ��� 1���� �߽�
    private readonly int maxDragBlockCount = 3;                             // ȭ�鿡 ǥ���Ǵ� �巡�� ����� ��

    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;                          // �巡�� ��� ��ġ

    [SerializeField]
    private UIController UIController;

    // ���� �ϼ��� ��ϵ��� �����ϱ� ���� ������ ����� ������ ����Ʈ�� ����
    private List<BackgroundBlock> filledBlockList;

    // ���� ����
    public int CurrentScore { get; private set; }
    // �ְ� ����
    public int HighScore { get; private set; }

    private void Awake()
    {
        // ���� �ʱ�ȭ
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");


        // �ӽ������ �ʱ�ȭ
        filledBlockList = new List<BackgroundBlock>();

        // ������� ���Ǵ� ��� ��ġ
        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        // �巡�� ����� ��ġ�Ǹ� ������ ����� ��� ��ġ �ϱ� ���� ũ�� ���Ѵ�.
        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        // ����� ����
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        // ��� �˻� �ý��� ����
        blockArrangeSystem.Setup(blockCount, blockHalf, backgroundBlocks, this);

        // �巡�� ��ϻ���
        //SpawnDragBlocks();
        StartCoroutine(SpawnDragBlocks());
    }

    private IEnumerator SpawnDragBlocks()
    {
        // ���� ����� �� �ִ� �巡�� ��� ���� ����
        currentDragBlockCount = maxDragBlockCount;
        // �巡�� ��� ����
        dragBlockSpawner.SpawnBlocks();

        // �巡�� ����� �̵��� �Ϸ�� ������ ���
        // IsCompleteSpawnBlocks �� treu �� ������ ���
        yield return new WaitUntil(() => IsCompleteSpawnBlocks());
    }

    private bool IsCompleteSpawnBlocks()
    {
        int count = 0;
        for(int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; i++)
        {
            if (dragBlockSpawner.BlockSpawnPoints[i].childCount != 0 &&
                dragBlockSpawner.BlockSpawnPoints[i].GetChild(0).localPosition == Vector3.zero)
            {
                count++;
            }
        }

        // ������ �巡�� ������Ʈ�� �θ� ��ġ ���� �̵��� �Ϸ��ϸ� 
        return count == dragBlockSpawner.BlockSpawnPoints.Length;
    }

    /// <summary>
    /// ��� ��ġ �� �۵��ϰ� �ϴ� ��
    /// </summary>
    public void AfterBlockArrangement(DragBlock block)
    {
        StartCoroutine("OnAfterBlockArrangement", block);
    }

    /// <summary>
    /// ��� ��ġ ��ó��
    /// </summary>
    private IEnumerator OnAfterBlockArrangement(DragBlock block)
    {
        // ��ġ�� �Ϸ�� �鷡�� ��� ����
        // ��ġ�� �޷�Ǹ� background�� ���� ���ϸ鼭 ��ġ�ϰ� �ȴ�.
        // �巡�� ����� ���� ������ ��ġ�� ����� ��ġ�� ������ �� �ֵ� �ȴ�.
        Destroy(block.gameObject);

        // �ϼ��� ���� �ִ��� �˻�
        int filledLineCount = CheckFilledLine();



        // ������ ���
        // �ϼ��� ���� ������ 0, �ϼ��� ���� ������ 2 �� fillLineCount �� * 10 �� ( 10, 20, 40, 80...)
        int lineScore = filledLineCount == 0 ? 0 : (int)Mathf.Pow(2, filledLineCount - 1) * 10;
        // ������� + ���� ����
        CurrentScore += block.ChildBlocks.Length + lineScore;


        // ���� �ϼ��� ��ϵ��� ����
        yield return StartCoroutine(DestroyFilledBlocks(block));

        // �Ʒ� ��ġ�� ����� ���� �����Ѵ�.
        currentDragBlockCount--;

        // �巡�� ������ ����� 0�� �̸� ���ο� ����� ��ȯ�Ѵ�.
        if (currentDragBlockCount == 0)
        {
            //SpawnDragBlocks();
            yield return StartCoroutine(SpawnDragBlocks());
            // ���� ������ �巡�� ����� �θ���ġ�� ��ġ �޷� �ϸ�
            
            // �ڷ�ƾ�� �ƴ� ���
            // ������ �巡�� ����� ��ġ�ϰ� ���ӿ����� �Ǹ� ���ڱ� ������ ����Ǿ� ���� ������ ����Ȱ��� �� �� ����.
            // �׷��� ���� �巡�� ���� ��ġ�� �Ϸ�Ǹ� ���ӿ��� ó���� �Ѵ�.
        }

        // ���� �������� ����� ������ ���
        yield return new WaitForEndOfFrame();

        // ���� ����
        if(IsGameOver())
        {
            if(CurrentScore > HighScore)
            {
                PlayerPrefs.SetInt("HighScore", CurrentScore);
            }

            // ���� ���� UI
            UIController.GameOver();
        }
    }

    /// <summary>
    ///  ���� �ִ� ����� �ִ� �˻� �ϰ� ����� ��ġ�� �� �ִ� ������ �ִ��� �˻�.
    /// </summary>
    private bool IsGameOver()
    {
        int dragBlockCount = 0;

        // ��ġ ������ �巡�� ����� �������� ��
        for(int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; ++i)
        {
            // �ڽ��� �ִٸ�, �巡�� ����� �ִٸ�
            if (dragBlockSpawner.BlockSpawnPoints[i].childCount != 0)
            {
                dragBlockCount++;

                // �巡�� ����� ��ġ�� �� �ִ� ������ �ִ��� �˻�
                if (blockArrangeSystem.IsPossibleArrangement(dragBlockSpawner.BlockSpawnPoints[i].GetComponentInChildren<DragBlock>()))
                {
                    // ��ġ�� �����ϴ�.
                    return false;
                }
            }
        }

        // dragBlockCount�� �ʿ� ��ġ ������ �巡�� ����� ��
        // dragBlockCount 0 �� �ƴϸ� ���� ����
        return dragBlockCount != 0;
    }

    /// <summary>
    /// �ϼ��� ���� �ִ��� �˻�
    /// </summary>
    private int CheckFilledLine()
    {
        int filledLineCount = 0;

        filledBlockList.Clear();

        // ���� �� �˻�
        for (int y = 0; y < blockCount.y; ++y)
        {
            int fillBlockCount = 0;

            for( int x = 0; x < blockCount.x; ++x)
            {
                // 0, 10, 20, 30  + ��ȣ 1,2,3,4
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            // �� ���� �˻� �� fill�� 10�� ��, ���� fill �̶�� ���̴�.
            if ( fillBlockCount == blockCount.x)
            {
                for( int x = 0; x < blockCount.x; ++x)
                {
                    // �ϼ��� ���� ��� ����� �ӽ� ����Ʈ�� ����
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }

        // ���� �� �˻�
        for( int x = 0; x < blockCount.x; ++x)
        {
            int fillBlockCount = 0;

            for (int y = 0; y < blockCount.y; ++y)
            {
                // 0, 10, 20, 30  + ��ȣ 1,2,3,4
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            // �� ���� �˻� �� fill�� 10�� ��, ���� fill �̶�� ���̴�.
            if (fillBlockCount == blockCount.x)
            {
                for (int y = 0; y < blockCount.y; ++y)
                {
                    // �ϼ��� ���� ��� ����� �ӽ� ����Ʈ�� ����
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }

        return filledLineCount;
    }

    /// <summary>
    /// ���� �ϼ��� ����� �������� ���������� ��� ����
    /// </summary>
    private IEnumerator DestroyFilledBlocks(DragBlock block)
    {
        // ������ ��ġ�� ��ϰ� �Ÿ��� ����� ������ ����
        filledBlockList.Sort((a, b) => (a.transform.position - block.transform.position).sqrMagnitude.
        CompareTo((b.transform.position - block.transform.position).sqrMagnitude));

        // �ӽ� ����Ʈ�� ����� ��� ����� �ʱ�ȭ
        for( int i = 0; i < filledBlockList.Count; ++i)
        {
            filledBlockList[i].EmptyBlock();

            yield return new WaitForSeconds(0.01f);
        }

        filledBlockList.Clear();
    }
}
