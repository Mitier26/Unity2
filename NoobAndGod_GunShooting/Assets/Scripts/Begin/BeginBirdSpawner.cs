using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBirdSpawner : MonoBehaviour
{
    // �� ��ȯ��
    // -6 ~ 6
    [SerializeField] private GameObject birdPrefab;             // �� ������

    [SerializeField] private float spawnTime = 2f;              // �ʱ� ��ȯ������

    [SerializeField] private float minSpawnTime, maxSpawnTime;  // ���� ��ȯ������
    [SerializeField] private float minX, maxX, y;               // ��ȯ ��ġ

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
