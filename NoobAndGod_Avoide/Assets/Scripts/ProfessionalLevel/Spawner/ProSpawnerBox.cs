using System.Collections;
using UnityEngine;

public class ProSpawnerBox : ProSpawner
{
    protected override IEnumerator SpawnObject()
    {
        while(true)
        {
            ProObject obj = pool.Get();

            obj.transform.position = transform.position + new Vector3(Random.Range(spawnRanges[0].localPosition.x, spawnRanges[1].localPosition.x), 0, 0);

            obj.GetComponent<Rigidbody2D>().velocity = spawnDirection * spawnPower;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
