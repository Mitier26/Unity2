using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerFish : ProSpawner
{
    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            ProObject obj = pool.Get();

            obj.transform.position = transform.position + new Vector3(Random.Range(spawnRanges[0].localPosition.x, spawnRanges[1].localPosition.x), 0, 0);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
