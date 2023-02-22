using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPos { public int row, col; }
// 구조체 int형의 row와 col로 이루어진 구조체를 만든다.
// GridPos 에는 2개의 인자가 들어 있다.

public enum Choice { Fish, Gold, Shark }

public class Board : MonoBehaviour
{
    Choice[][] choices;             
    // 2중 배열을로 만들어진 enum
    List<GridPos> indexList;
    // 인덱스를 이용하여 row, col를 가지고 오고
    // 가지고온 row, col을 이용하여 해당 좌표에는 있는 Choice에 접근한다.

    public Board()
    {
        // 생성자 초기화
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
        GridPos temp;       // 임시 저장
        int index = Random.Range(0, indexList.Count);
        temp = new GridPos { row = indexList[index].row, col = indexList[index].col };
        indexList.RemoveAt(index);
        return temp;
    }

    private void AddSharkAndGold()
    {
        GridPos temp;       // 임시 저장
        // 금과 상어의 배치
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
