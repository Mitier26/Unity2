using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject tilePrefab;                          // 맵에 배치되는 타일
    [SerializeField]
    private Transform currentTile;                          // 현재의 타일 ( 다음에 만들 타일에 사용 )

    [SerializeField]
    private int spawnTileCountAtStart = 100;                // 게임을 시작할 때 생성되는 타일의 수

    private void Awake()
    {
        for ( int i = 0; i < spawnTileCountAtStart; ++i)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {
        GameObject clone = Instantiate(tilePrefab);

        clone.transform.SetParent(transform);

        // 타일 위치 재설정을 위한 것
        clone.GetComponent<Tile>().Setup(this);

        clone.transform.GetChild(1).GetComponent<Item>().Setup(gameController);

        SetTilePosition(clone.transform);
        }

    public void SetTilePosition(Transform tile)
    {
        // 타일을 보이도록 설정
        tile.gameObject.SetActive(true);

        // 랜덤을 이용 0 이 나오면 오른쪽
        // 1이 나오면 앞쪽
        int index = Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position = currentTile.position + addPosition;

        // 마지막에 생성된 타일을 현재 타일로 설정해
        // 다음에 추가되는 타일이 연결되어 나올 수 있게 한다.
        currentTile = tile;

        int spawnItem = Random.Range(0, 100);
        if(spawnItem < 20)
        {
            tile.GetChild(1).gameObject.SetActive(true);
        }
    }

}
