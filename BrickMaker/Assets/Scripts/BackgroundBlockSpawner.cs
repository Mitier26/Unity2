using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;                 // 블럭 프리팹
    [SerializeField]
    private int orderInLayer;                       // 블록의 그림 순서

    //private Vector2Int blockCount = new Vector2Int(10, 10);         // 블록의 가로,세로 수
    //private Vector2 blockHalf = new Vector2(0.5f, 0.5f);            // 블록 크기의 반

    //private void Awake()
    public BackgroundBlock[] SpawnBlocks(Vector2Int blockCount, Vector2 blockHalf)
    {
        // Awake에 있던 것을 외부에서 작동시킬 수 있도록 변경
        BackgroundBlock[] blocks = new BackgroundBlock[blockCount.x * blockCount.y];

        for(int y = 0; y < blockCount.y; ++y)
        {
            for(int x = 0; x < blockCount.x; ++x)
            {
                float px = -blockCount.x * 0.5f + blockHalf.x + x;
                float py = blockCount.y * 0.5f - blockHalf.y - y;

                Vector3 position = new Vector3(px, py, 0);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

                // 생성한 모든 블록의 정보를 반환하기 위해 배열에 저장
                blocks[y * blockCount.x + x] = clone.GetComponent<BackgroundBlock>();
            }
        }

        return blocks;
    }
}
