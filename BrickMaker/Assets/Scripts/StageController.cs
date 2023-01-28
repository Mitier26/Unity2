using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private BackgroundBlockSpawner backgroundBlockSpawner;                  // 배경의 블록생성기
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner;                  // 배경앞의 블록생성기
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;                              // 배치할 수 있는 블록생성기

    private BackgroundBlock[] backgroundBlocks;                             // 만들어진 블록을 관리 
    private int currentDragBlockCount;                                      // 드래그 블록의 수, 없으면 다시 생성하기 위해

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);        // 배경블록의 수량, 게임 판의 크기
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);           // 블록 1개의 중심
    private readonly int maxDragBlockCount = 3;                             // 화면에 표현되는 드래그 블록의 수

    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;                          // 드래그 블록 배치

    [SerializeField]
    private UIController UIController;

    // 줄이 완성된 블록들을 삭제하기 위해 삭제할 블록을 정보롤 리스트에 저장
    private List<BackgroundBlock> filledBlockList;

    // 현재 점수
    public int CurrentScore { get; private set; }
    // 최고 점수
    public int HighScore { get; private set; }

    private void Awake()
    {
        // 점수 초기화
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");


        // 임시저장소 초기화
        filledBlockList = new List<BackgroundBlock>();

        // 배경으로 사용되는 블록 배치
        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        // 드래그 블록이 배치되면 색상이 변경될 블록 배치 하기 번에 크기 정한다.
        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        // 블록을 생성
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        // 블록 검사 시스템 세팅
        blockArrangeSystem.Setup(blockCount, blockHalf, backgroundBlocks, this);

        // 드래그 블록생성
        //SpawnDragBlocks();
        StartCoroutine(SpawnDragBlocks());
    }

    private IEnumerator SpawnDragBlocks()
    {
        // 현재 사용할 수 있는 드래그 블록 수를 설정
        currentDragBlockCount = maxDragBlockCount;
        // 드래그 블록 생성
        dragBlockSpawner.SpawnBlocks();

        // 드래그 블록의 이동이 완료될 때까지 대기
        // IsCompleteSpawnBlocks 가 treu 일 때까지 대기
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

        // 생성된 드래그 오브젝트가 부모 위치 까지 이동을 완료하면 
        return count == dragBlockSpawner.BlockSpawnPoints.Length;
    }

    /// <summary>
    /// 블록 배치 후 작동하게 하는 것
    /// </summary>
    public void AfterBlockArrangement(DragBlock block)
    {
        StartCoroutine("OnAfterBlockArrangement", block);
    }

    /// <summary>
    /// 블록 배치 후처리
    /// </summary>
    private IEnumerator OnAfterBlockArrangement(DragBlock block)
    {
        // 배치가 완료된 들래그 블록 삭제
        // 배치가 왼료되면 background에 색을 변하면서 배치하게 된다.
        // 드래그 블록이 남아 있으면 배치한 블록의 위치를 변경할 수 있데 된다.
        Destroy(block.gameObject);

        // 완성된 줄이 있는지 검사
        int filledLineCount = CheckFilledLine();



        // 점수의 계산
        // 완성돈 줄이 없으면 0, 완성된 줄이 있으면 2 의 fillLineCount 승 * 10 점 ( 10, 20, 40, 80...)
        int lineScore = filledLineCount == 0 ? 0 : (int)Mathf.Pow(2, filledLineCount - 1) * 10;
        // 블록점수 + 라인 점수
        CurrentScore += block.ChildBlocks.Length + lineScore;


        // 줄이 완성된 블록들을 삭제
        yield return StartCoroutine(DestroyFilledBlocks(block));

        // 아래 배치할 블록의 수를 감소한다.
        currentDragBlockCount--;

        // 드래그 가능한 블록이 0개 이면 새로운 블록을 소환한다.
        if (currentDragBlockCount == 0)
        {
            //SpawnDragBlocks();
            yield return StartCoroutine(SpawnDragBlocks());
            // 새로 생성된 드래그 블록이 부모위치에 배치 왼료 하면
            
            // 코루틴이 아닐 경우
            // 마지막 드래그 블록을 배치하고 게임오버가 되면 갑자기 게임이 종료되어 무엇 때문에 종료된건지 알 수 없다.
            // 그렇기 때문 드래그 블럭의 배치가 완료되면 게임오버 처리를 한다.
        }

        // 현재 프레임이 종료될 때까지 대기
        yield return new WaitForEndOfFrame();

        // 게임 오버
        if(IsGameOver())
        {
            if(CurrentScore > HighScore)
            {
                PlayerPrefs.SetInt("HighScore", CurrentScore);
            }

            // 게임 오버 UI
            UIController.GameOver();
        }
    }

    /// <summary>
    ///  남아 있는 블록이 있는 검사 하고 블록을 배치할 수 있는 공간이 있는지 검사.
    /// </summary>
    private bool IsGameOver()
    {
        int dragBlockCount = 0;

        // 배치 가능한 드래그 블록이 남아있을 때
        for(int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; ++i)
        {
            // 자식이 있다면, 드래그 블록이 있다면
            if (dragBlockSpawner.BlockSpawnPoints[i].childCount != 0)
            {
                dragBlockCount++;

                // 드래그 블록을 배치할 수 있는 공간이 있는지 검사
                if (blockArrangeSystem.IsPossibleArrangement(dragBlockSpawner.BlockSpawnPoints[i].GetComponentInChildren<DragBlock>()))
                {
                    // 배치가 가능하다.
                    return false;
                }
            }
        }

        // dragBlockCount는 맵에 배치 가능한 드래그 블록의 수
        // dragBlockCount 0 이 아니면 게임 오버
        return dragBlockCount != 0;
    }

    /// <summary>
    /// 완성된 줄이 있는지 검사
    /// </summary>
    private int CheckFilledLine()
    {
        int filledLineCount = 0;

        filledBlockList.Clear();

        // 가로 줄 검사
        for (int y = 0; y < blockCount.y; ++y)
        {
            int fillBlockCount = 0;

            for( int x = 0; x < blockCount.x; ++x)
            {
                // 0, 10, 20, 30  + 번호 1,2,3,4
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            // 한 줄을 검사 후 fill이 10개 면, 전부 fill 이라는 것이다.
            if ( fillBlockCount == blockCount.x)
            {
                for( int x = 0; x < blockCount.x; ++x)
                {
                    // 완성된 줄의 배경 블록을 임시 리스트에 저장
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }

        // 세로 줄 검사
        for( int x = 0; x < blockCount.x; ++x)
        {
            int fillBlockCount = 0;

            for (int y = 0; y < blockCount.y; ++y)
            {
                // 0, 10, 20, 30  + 번호 1,2,3,4
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            // 한 줄을 검사 후 fill이 10개 면, 전부 fill 이라는 것이다.
            if (fillBlockCount == blockCount.x)
            {
                for (int y = 0; y < blockCount.y; ++y)
                {
                    // 완성된 줄의 배경 블록을 임시 리스트에 저장
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }

        return filledLineCount;
    }

    /// <summary>
    /// 줄을 완성한 블록을 기주으로 펴져나가듯 블록 삭제
    /// </summary>
    private IEnumerator DestroyFilledBlocks(DragBlock block)
    {
        // 마지막 배치한 블록과 거리가 가까운 순서로 정렬
        filledBlockList.Sort((a, b) => (a.transform.position - block.transform.position).sqrMagnitude.
        CompareTo((b.transform.position - block.transform.position).sqrMagnitude));

        // 임시 리스트에 저장된 배경 블록을 초기화
        for( int i = 0; i < filledBlockList.Count; ++i)
        {
            filledBlockList[i].EmptyBlock();

            yield return new WaitForSeconds(0.01f);
        }

        filledBlockList.Clear();
    }
}
