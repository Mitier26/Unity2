using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private GridLayoutGroup gridLayout;


    public List<Block> SpawnBlock(int blockCount)
    {
        List<Block> blockList = new List<Block>(blockCount * blockCount);


        int cellSize = 300 - 50 * (blockCount - 2);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);

        gridLayout.constraintCount = blockCount;

        for(int y=0; y < blockCount; ++y)
        {
            for(int x = 0; x < blockCount; ++x)
            {
                Block block = Instantiate(blockPrefab, gridLayout.transform);
                blockList.Add(block);
            }
        }

        return blockList;
    }
}
