using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public enum PlayerType { None, Red, Blue, }
public struct GridPos { public int row, col; }

public class Board
{
    PlayerType[][] playerBoard;
    GridPos currentPos;

    public Board()
    {
        playerBoard = new PlayerType[6][];
        for(int i = 0; i < playerBoard.Length; i++)
        {
            playerBoard[i] = new PlayerType[7];

            for(int j = 0; j < playerBoard[i].Length; j++)
            {
                playerBoard[i][j] = PlayerType.None;
            }
        }
    }

    public void UpdateBoard(int col, bool isPlayer)
    {
        int updatePos = 6;
        for(int i = 5; i >= 0; i--)
        {
            if (playerBoard[i][col] == PlayerType.None)
            {
                updatePos--;
            }
            else
            {
                break;
            }
        }

        playerBoard[updatePos][col] = isPlayer ? PlayerType.Red : PlayerType.Blue;
        currentPos = new GridPos { row = updatePos, col = col };
    }

    public bool Result(bool isPlayer)
    {
        PlayerType current = isPlayer ? PlayerType.Red : PlayerType.Blue;
        return IsHorizontal(current) || IsVertical(current) || IsDiagonal(current) || IsReverseDiagonal(current);
    }

    private bool IsVertical(PlayerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = 0 });

        List<GridPos> toSearchList = GetPlayerList(start, new GridPos { row = 1, col = 0 });

        return SearchResult(toSearchList, current);
    }

    private bool IsHorizontal(PlayerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = 0, col = -1 });

        List<GridPos> toSearchList = GetPlayerList(start, new GridPos { row = 0, col = 1 });

        return SearchResult(toSearchList, current);
    }

    private bool IsDiagonal(PlayerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = -1 });

        List<GridPos> toSearchList = GetPlayerList(start, new GridPos { row = 1, col = 1 });

        return SearchResult(toSearchList, current);
    }

    private bool IsReverseDiagonal(PlayerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = 1 });

        List<GridPos> toSearchList = GetPlayerList(start, new GridPos { row = 1, col = -1 });

        return SearchResult(toSearchList, current);
    }

    public GridPos GetEndPoint(GridPos diff)
    {
        GridPos result = new GridPos { row = currentPos.row, col = currentPos.col };

        while(result.row + diff.row < 6 && result.col + diff.col < 7 && result.row + diff.row >= 0 && result.col + diff.col >= 0) 
        {
            result.row += diff.row;
            result.col += diff.col;
        }

        return result;
    }

    private List<GridPos> GetPlayerList(GridPos start, GridPos diff)
    {
        List<GridPos> resultList;
        resultList = new List<GridPos> { start };
        GridPos result = new GridPos { row = start.row, col = start.col };

        while (result.row + diff.row < 6 && result.col + diff.col < 7 && result.row + diff.row >= 0 && result.col + diff.col >= 0)
        {
            result.row += diff.row;
            result.col += diff.col;
            resultList.Add(result);
        }
        return resultList;
    }

    private bool SearchResult(List<GridPos> searchList, PlayerType current)
    {
        int count = 0;
        for(int i = 0; i < searchList.Count; i++)
        {
            PlayerType compare = playerBoard[searchList[i].row][searchList[i].col];
            if(compare == current)
            {
                count++;
                if (count == 4)
                    break;
            }
            else
            {
                count = 0;
            }
        }

        return count >= 4;
    }
}
