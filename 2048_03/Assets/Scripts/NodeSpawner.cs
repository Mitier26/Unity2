using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform nodeRect;

    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;

    public List<Node> SpawnNodes(Board board, Vector2Int blockCount, float blockSize)
    {
        //Vector2Int blockCount = new Vector2Int(4, 4);

        gridLayoutGroup.cellSize = new Vector2(blockSize , blockSize);

        List<Node> nodeList = new List<Node>(blockCount.x * blockCount.y);

        for (int y = 0; y < blockCount.y; ++y)
        {
            for (int x = 0; x < blockCount.x; ++x)
            {
                //  생성과 부모 설정
                GameObject clone = Instantiate(nodePrefab, nodeRect.transform);

                // 노드의 좌표
                Vector2Int point = new Vector2Int(x, y);


                // 인접한 노드 정보 저장, 인접한 노드가 없다면 null
                Vector2Int?[] neighborNodes = new Vector2Int?[4];

                // 그리드레이아웃그룹의 StartCorner 가 UpperLeft일 경우 y 축은 up 방향이 -
                Vector2Int right = point + Vector2Int.right;
                Vector2Int down = point + Vector2Int.up;
                Vector2Int left = point + Vector2Int.left;
                Vector2Int up = point + Vector2Int.down;

                if (IsValid(right, blockCount)) neighborNodes[0] = right;
                if (IsValid(down, blockCount)) neighborNodes[1] = down;
                if (IsValid(left, blockCount)) neighborNodes[2] = left;
                if (IsValid(up, blockCount)) neighborNodes[3] = up;


                // 생성한 노드의 Setup 실행
                Node node = clone.GetComponent<Node>();
                node.Setup(board, neighborNodes, point);

                // 노드의 이름을 변경
                clone.name = $"[{node.Point.y}, {node.Point.x}]";

                nodeList.Add(node);
            }
        }

        return nodeList;
    }
    
    private bool IsValid(Vector2Int point, Vector2Int blockCount)
    {
        if (point.x == -1 || point.x == blockCount.x || point.y == blockCount.y || point.y == -1)
        {
            return false;
        }

        return true;
    }


}
