using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private int numOfBlock = 8;

    [SerializeField]
    private Block blockPrefab;
    [SerializeField]
    private float distanceBetweenBlocks = 4f;
    private int rowsSpawned = 0;
    private List<Block> blockSpawned = new List<Block>();

    private void OnEnable()
    {
        SpawnBlocks();
    }

    public void SpawnBlocks()
    {
        foreach(var block in blockSpawned)
        {
            if(block != null)
            {
                block.transform.position += Vector3.down * distanceBetweenBlocks;
            }
        }

        for(int i = 0; i < numOfBlock; i++)
        {
            if(Random.Range(0,100) > 50)
            {
                var block = Instantiate(blockPrefab, GetPositon(i), Quaternion.identity);
                int hits = Random.Range(1, 4) + rowsSpawned;
                block.SetHit(hits);
                blockSpawned.Add(block);
            }
        }

        rowsSpawned++;
    }

    private Vector3 GetPositon(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBetweenBlocks;
        return position;
    }
}
