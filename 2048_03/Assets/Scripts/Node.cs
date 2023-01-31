using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None = -1, Right = 0, Down, Left, Up
}

public class Node : MonoBehaviour
{       
    public Block placeBlock;                                    // 현재 노드에 배치되어 있응 블록 정보
    public Vector2 localPosition;                               // 현재 노드의 RectTransform 로컬 좌표

    public bool combined = false;                               // 현재 노드의 블록이 병합중인지 확인

    public Vector2Int Point { get; private set; }               // 현재 노드의 x,y 격자 좌표 정보 ( 좌 상단이 0,0)

    public Vector2Int?[] NeighborNodes { get; private set; }    // 현재 노드에 인접한 노드의 좌표
    // Nullable
    // 0 이 아닌 null 비어 있는 값을 가질수 있게 설정하는 것
    // .HasVale : 값이 있는지 확인
    // .Value : 값

    private Board board;

    public void Setup(Board board, Vector2Int?[] neighborNodes, Vector2Int point)
    {
        this.board = board;
        NeighborNodes = neighborNodes;
        Point = point;
    }

    public Node FindTarget(Node originalNode, Direction direction, Node farNode=null)
    {
        if (NeighborNodes[(int)direction].HasValue == true)
        {
            // 방향에 있는 노드 정보
            Vector2Int point = NeighborNodes[(int)direction].Value;
            Node neighborNode = board.NodeList[point.y * board.BlockCount.x + point.x];

            // 인접 노드가 병합 중이면
            if (neighborNode != null && neighborNode.combined)
            {
                return this;
            }

            // 노드가 배치되어 있으면
            if(neighborNode.placeBlock != null && originalNode.placeBlock != null)
            {
                if(neighborNode.placeBlock.Numeric == originalNode.placeBlock.Numeric)
                {
                    return neighborNode;
                }
                else
                {
                    return farNode;
                }
            }

            return neighborNode.FindTarget(originalNode, direction, neighborNode);
        }

        return farNode;
    }
}
