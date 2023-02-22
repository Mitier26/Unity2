using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPos { public int row, col; }
// ����ü int���� row�� col�� �̷���� ����ü�� �����.
// GridPos ���� 2���� ���ڰ� ��� �ִ�.

public enum Choice { Fish, Gold, Shark }

public class Board : MonoBehaviour
{
    Choice[][] choices;             
    // 2�� �迭���� ������� enum
    List<GridPos> indexList;
    // �ε����� �̿��Ͽ� row, col�� ������ ����
    // ������� row, col�� �̿��Ͽ� �ش� ��ǥ���� �ִ� Choice�� �����Ѵ�.

    public Board()
    {
        // ������ �ʱ�ȭ
        choices = new Choice[5][];
        indexList = new List<GridPos>();

        for (int i = 0; i < 5; i++)
        {
            choices[i] = new Choice[5];
            for(int j = 0; j < choices[i].Length; j++)
            {
                choices[i][j] = Choice.Fish;
                indexList.Add(new GridPos {  row = i, col = j });
            }
        }

        AddSharkAndGold();
    }

    private GridPos GetRandomFromList()
    {
        GridPos temp;       // �ӽ� ����
        int index = Random.Range(0, indexList.Count);
        temp = new GridPos { row = indexList[index].row, col = indexList[index].col };
        indexList.RemoveAt(index);
        return temp;
    }

    private void AddSharkAndGold()
    {
        GridPos temp;       // �ӽ� ����
        // �ݰ� ����� ��ġ
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice.Gold;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice.Gold;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice.Shark;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice.Shark;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice.Shark;
    }

    public Choice GetChoice ( int row, int col)
    {
        return choices[row][col];
    }
}
