using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }     // 좌표

    public Tile tile { get; set; }

    public bool empty => tile == null;

    public bool occupied => tile != null;           // 내용물이 있는
}
