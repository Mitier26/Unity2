using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSpawnerCoin : ProSpawner
{
    [Header("--------Coin--------")]
    [SerializeField]
    private Vector2 coinDirectionX;         // �߻��ϴ� ���� X ����
    [SerializeField]
    private Vector2 coinDirectionY;         // �߻��ϴ� ���� Y ����
    [SerializeField]
    private float coinPower;                // �߻��ϴ� ��

    protected override IEnumerator SpawnObject()
    {
        while (true)
        {
            // Ǯ���� ������ ������ �´�.
            ProObject obj = pool.Get();

            // ������ ��ȯ�� ������ �������� ���Ѵ�
            spawnDirection = new Vector2(Random.Range(coinDirectionX.x, coinDirectionX.y), Random.Range(coinDirectionY.x, coinDirectionY.y));
            // ������ �Ŀ� = ������ �ӵ��� ���Ѵ�.
            spawnPower = Random.Range(5f, coinPower);

            // Ǯ���� ������� ������ �ʱ� ��ġ�� �����Ѵ�.
            obj.transform.position = transform.position + new Vector3(Random.Range(spawnRanges[0].localPosition.x, spawnRanges[1].localPosition.x), 0, 0);
            // ������ �ӵ��� ������ ���� ������.
            obj.GetComponent<Rigidbody2D>().velocity = spawnDirection * spawnPower;

            // ��ȯ ����
            yield return new WaitForSeconds(Random.Range(spawnDelay, spawnDelay * 2));
        }
    }
}
