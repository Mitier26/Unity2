using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerCoin : ProSpawner
{
    [Header("--------Coin--------")]
    [SerializeField]
    private Vector2 coinDirectionX;
    [SerializeField]
    private Vector2 coinDirectionY;
    [SerializeField]
    private float coinPower;

    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            ProObject obj = pool.Get();

            spawnDirection = new Vector2(Random.Range(coinDirectionX.x, coinDirectionX.y), Random.Range(coinDirectionY.x, coinDirectionY.y));
            spawnPower = Random.Range(5f, coinPower);

            obj.transform.position = transform.position + new Vector3(Random.Range(spawnRanges[0].localPosition.x, spawnRanges[1].localPosition.x), 0, 0);
            obj.GetComponent<Rigidbody2D>().velocity = spawnDirection * spawnPower;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
