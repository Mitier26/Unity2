using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Cell { None, X, O,}
public class Board 
{
    Cell[] cells;

    public Board()
    {
        cells = new Cell[9];

        for(int i = 0; i< cells.Length; i++)
        {
            cells[i] = Cell.None;
        }
    }

    // 게임메니져에서 실행되서 처음 들어 오는 부분
    public bool UpdateCell(int row, int col, bool isPlayer)
    {
        int index = row * 3 + col;
        cells[index] = isPlayer ? Cell.X : Cell.O;
        return IsWinner(row, col);
    }

    private bool IsWinner(int row, int col)
    {
        Cell myCell = cells[row * 3 + col];
        return IsHorizontal(row,myCell) || IsVertical(col, myCell) || IsDiagonal(row*3 + col, myCell);
    }

    private bool IsHorizontal(int row, Cell cell)
    {
        return Match(row * 3, cell) && Match(row * 3 +1 ,cell) && Match(row * 3 +2, cell);
    }

    private bool IsVertical(int col, Cell cell)
    {
        return Match(col, cell) && Match(col + 3,cell) && Match(col + 6 ,cell);
    }

    private bool IsDiagonal(int index, Cell cell)
    {
        if(index == 0 || index == 4 || index == 8)
        {
            return Match(0,cell) && Match(4,cell) & Match(8,cell);
        }
        if(index == 2 || index == 4|| index == 6)
        {
            return Match(2, cell) && Match(4, cell) && Match(6, cell);
        }
        return false;
    }

    private bool Match(int index, Cell cell)
    {
        return cells[index] == cell;
    }
}
