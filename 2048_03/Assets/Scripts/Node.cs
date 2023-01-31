using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None = -1, Right = 0, Down, Left, Up
}

public class Node : MonoBehaviour
{       
    public Block placeBlock;                                    // ���� ��忡 ��ġ�Ǿ� ���� ��� ����
    public Vector2 localPosition;                               // ���� ����� RectTransform ���� ��ǥ

    public bool combined = false;                               // ���� ����� ����� ���������� Ȯ��

    public Vector2Int Point { get; private set; }               // ���� ����� x,y ���� ��ǥ ���� ( �� ����� 0,0)

    public Vector2Int?[] NeighborNodes { get; private set; }    // ���� ��忡 ������ ����� ��ǥ
    // Nullable
    // 0 �� �ƴ� null ��� �ִ� ���� ������ �ְ� �����ϴ� ��
    // .HasVale : ���� �ִ��� Ȯ��
    // .Value : ��

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
            // ���⿡ �ִ� ��� ����
            Vector2Int point = NeighborNodes[(int)direction].Value;
            Node neighborNode = board.NodeList[point.y * board.BlockCount.x + point.x];

            // ���� ��尡 ���� ���̸�
            if (neighborNode != null && neighborNode.combined)
            {
                return this;
            }

            // ��尡 ��ġ�Ǿ� ������
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
