using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject tilePrefab;                          // �ʿ� ��ġ�Ǵ� Ÿ��
    [SerializeField]
    private Transform currentTile;                          // ������ Ÿ�� ( ������ ���� Ÿ�Ͽ� ��� )

    [SerializeField]
    private int spawnTileCountAtStart = 100;                // ������ ������ �� �����Ǵ� Ÿ���� ��

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

        // Ÿ�� ��ġ �缳���� ���� ��
        clone.GetComponent<Tile>().Setup(this);

        clone.transform.GetChild(1).GetComponent<Item>().Setup(gameController);

        SetTilePosition(clone.transform);
        }

    public void SetTilePosition(Transform tile)
    {
        // Ÿ���� ���̵��� ����
        tile.gameObject.SetActive(true);

        // ������ �̿� 0 �� ������ ������
        // 1�� ������ ����
        int index = Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position = currentTile.position + addPosition;

        // �������� ������ Ÿ���� ���� Ÿ�Ϸ� ������
        // ������ �߰��Ǵ� Ÿ���� ����Ǿ� ���� �� �ְ� �Ѵ�.
        currentTile = tile;

        int spawnItem = Random.Range(0, 100);
        if(spawnItem < 20)
        {
            tile.GetChild(1).gameObject.SetActive(true);
        }
    }

}
