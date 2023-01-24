using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] private int spawnAreaAtStart = 2;      // 생성되는 판 수
    [SerializeField] private float distanceToNext = 30;     // 구역 사이의 거리

    private int areaIndex = 0;


    [SerializeField] private Transform player;              // 플레이어의 정보 ( 소환용 )


    private void Awake()
    {
        for(int i = 0; i < spawnAreaAtStart; ++i)
        {
            SpawnArea();
        }

    }

    private void Update()
    {
        // 플레이어의 y위치가 30 배수 마다 넘으면 1씩 증가 한다.
        int playerIndex = (int)(player.position.y / distanceToNext);

        if(playerIndex == areaIndex -1)
        {
            SpawnArea();
        }
    }

    private void SpawnArea()
    {
        // 임의의 구역 선택
        int  index = Random.Range(0, areaPrefabs.Length);

        // 선택된 구역을 소환
        GameObject clone = Instantiate(areaPrefabs[index]);

        // 소환된 구역을 배치 : 간격 * 번호 
        // 0 번은 0, 1 번은 30, 2번은 60
        clone.transform.position = Vector3.up * distanceToNext * areaIndex;

        
        areaIndex++;
    }
}
