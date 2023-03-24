using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerFly : ProSpawner
{

    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            ProObject obj = pool.Get();

            obj.transform.position = new Vector3(transform.position.x, Random.Range(spawnRanges[0].localPosition.y, spawnRanges[1].localPosition.y), 0);

            obj.GetComponent<ProFly>().direction = spawnDirection * spawnPower;
            obj.GetComponent<ProFly>().startPositionX = transform.position.x;
            obj.GetComponent<ProFly>().SetFly();

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
