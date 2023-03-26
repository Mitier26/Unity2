using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerBall : ProSpawner
{
    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            ProObject obj = pool.Get();

            obj.transform.position = new Vector3(0, Random.Range(spawnRanges[0].localPosition.y, spawnRanges[1].localPosition.y) , 0);

            obj.GetComponent<ProBall>().direction = spawnDirection * spawnPower;
            obj.GetComponent<ProBall>().startPositionX = transform.position.x;

            yield return new WaitForSeconds(Random.Range(spawnDelay, spawnDelay * 2));
        }
    }
}
