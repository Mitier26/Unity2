using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBirdSpawner : MonoBehaviour
{
    // 새 소환기
    // -6 ~ 6
    [SerializeField] private GameObject birdPrefab;             // 새 프리팹

    [SerializeField] private float spawnTime = 2f;              // 초기 소환딜레이

    [SerializeField] private float minSpawnTime, maxSpawnTime;  // 이후 소환딜레이
    [SerializeField] private float minX, maxX, y;               // 소환 위치

    private int count = 0;

    private IEnumerator Start()
    {
        while(count < BeginGameManager.instance.birdCount)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject bird = Instantiate(birdPrefab);

            float x = Random.Range(minX, maxX);
            bird.transform.position = new Vector3(x, y, 0);

            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            count++;
        }
    }

}
