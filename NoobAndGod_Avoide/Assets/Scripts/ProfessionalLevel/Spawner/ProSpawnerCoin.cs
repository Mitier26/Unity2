using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerCoin : ProSpawner
{
    [Header("--------Coin--------")]
    [SerializeField]
    private Vector2 coinDirectionX;         // 발사하는 것의 X 방향
    [SerializeField]
    private Vector2 coinDirectionY;         // 발사하는 것의 Y 방향
    [SerializeField]
    private float coinPower;                // 발사하는 힘

    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            // 풀에서 코인을 가지고 온다.
            ProObject obj = pool.Get();

            // 코인이 소환될 방향을 랜덤으로 정한다
            spawnDirection = new Vector2(Random.Range(coinDirectionX.x, coinDirectionX.y), Random.Range(coinDirectionY.x, coinDirectionY.y));
            // 코인의 파워 = 코인의 속도를 정한다.
            spawnPower = Random.Range(5f, coinPower);

            // 풀에서 가지고온 코인의 초기 위치를 지정한다.
            obj.transform.position = transform.position + new Vector3(Random.Range(spawnRanges[0].localPosition.x, spawnRanges[1].localPosition.x), 0, 0);
            // 코인의 속도를 변경해 날려 버린다.
            obj.GetComponent<Rigidbody2D>().velocity = spawnDirection * spawnPower;

            // 소환 간격
            yield return new WaitForSeconds(Random.Range(spawnDelay, spawnDelay * 2));
        }
    }
}
