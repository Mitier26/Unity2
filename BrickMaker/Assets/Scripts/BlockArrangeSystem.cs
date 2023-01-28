using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArrangeSystem : MonoBehaviour
{
    private Vector2Int blockCount;
    private Vector2 blockHalf;
    private BackgroundBlock[] backgroundBlocks;
    private StageController stageController;

    public void Setup(Vector2Int blockCount, Vector2 blockHalf, BackgroundBlock[] backgroundBlocks, StageController stageController)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
        this.backgroundBlocks = backgroundBlocks;
        this.stageController = stageController;
    }

    /// <summary>
    /// 매개변수로 받아온 드래그 블록을 배치할 수 있는지 검사
    /// 배치가 가능하면 블록배치, 줄 완성 검사, 점수 계산 처리
    /// </summary>
    public bool TryArrangementBlock(DragBlock block)
    {
        // 블록의 배치가 가능한지 검사
        for(int i = 0; i < block.ChildBlocks.Length; ++i)
        {
            // 자식 블록의 월드 위치
            Vector3 position = block.transform.position + block.ChildBlocks[i];

            // 드래그 블록이 배경안에 있는지 확인
            if ( !IsBlockInsideMap(position) ) return false;

            // 드래그 블록 위치에 다른 블록이 있는지 확인
            if (!IsOtherBlockInThisBlock(position)) return false;
        }

        // 블록의 배치
        for ( int i = 0; i < block.ChildBlocks.Length; ++i )
        {
            Vector3 position = block.transform.position + block.ChildBlocks[i];

            // 해당 위치 배경블록의 색을 변경하고 Fill 로 변경
            backgroundBlocks[PositionToIndex(position)].FillBlock(block.Color);
        }

        // 블록 배치 이후 처리
        stageController.AfterBlockArrangement(block);

        return true;
    }

    /// <summary>
    /// 블록을 배치하는 위치가 배경안에 있는지 확인.
    /// </summary>
    private bool IsBlockInsideMap(Vector2 position)
    {
        if ( position.x < -blockCount.x * 0.5f + blockHalf.x || position.x > blockCount.x * 0.5f - blockHalf.x||
            position.y < -blockCount.y * 0.5f + blockHalf.y || position.y > blockCount.y * 0.5f - blockHalf.y)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 드래그 블록의 위치 값을 이용하여 맵에 배치된 블록의 index를 반환
    /// </summary>
    private int PositionToIndex(Vector2 position)
    {
        float x = blockCount.x * 0.5f - blockHalf.x + position.x;
        float y = blockCount.y * 0.5f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }

    /// <summary>
    /// 위에서 구한 index를 이용하여 배치하려는 장소에 블록이 있는지 검사
    /// </summary>
    private bool IsOtherBlockInThisBlock(Vector2 position)
    {
        int index = PositionToIndex(position);

        if (backgroundBlocks[index].BlockState == BlockState.Fill)
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// 드래그 블록에 포함된 자식 블록들이 배치 가능한지 검사.
    /// </summary>
    public bool IsPossibleArrangement(DragBlock block)
    {
        for(int y = 0; y  < blockCount.y; ++y)
        {
            for(int x = 0; x < blockCount.x; ++x)
            {
                int count = 0;
                Vector3 position = new Vector3(-blockCount.x * 0.5f + blockHalf.x + x, blockCount.y * 0.5f - blockHalf.y - y, 0);

                // 블록의 개수가 홀수 이면 좌표 그래도 짝수이면 0.5, 0.5
                position.x = block.BlockCount.x % 2 == 0 ? position.x + 0.5f : position.x;
                position.y = block.BlockCount.y % 2 == 0 ? position.y - 0.5f : position.y;

                // 현재 블록의 자식이 맵 내부인지 다른 블록이 있는지 검사
                for (int i = 0; i < block.ChildBlocks.Length; ++i)
                {
                    Vector3 blockPosition = block.ChildBlocks[i] + position;

                    if (!IsBlockInsideMap(blockPosition)) break;
                    if (!IsOtherBlockInThisBlock(blockPosition)) break;

                    count++;
                }

                // count 가 블록의 수와 같지 않느면 배치할 수 없다.
                if( count == block.ChildBlocks.Length)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
