using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] private int spawnAreaAtStart = 2;      // �����Ǵ� �� ��
    [SerializeField] private float distanceToNext = 30;     // ���� ������ �Ÿ�

    private int areaIndex = 0;


    [SerializeField] private Transform player;              // �÷��̾��� ���� ( ��ȯ�� )


    private void Awake()
    {
        for(int i = 0; i < spawnAreaAtStart; ++i)
        {
            SpawnArea();
        }

    }

    private void Update()
    {
        // �÷��̾��� y��ġ�� 30 ��� ���� ������ 1�� ���� �Ѵ�.
        int playerIndex = (int)(player.position.y / distanceToNext);

        if(playerIndex == areaIndex -1)
        {
            SpawnArea();
        }
    }

    private void SpawnArea()
    {
        // ������ ���� ����
        int  index = Random.Range(0, areaPrefabs.Length);

        // ���õ� ������ ��ȯ
        GameObject clone = Instantiate(areaPrefabs[index]);

        // ��ȯ�� ������ ��ġ : ���� * ��ȣ 
        // 0 ���� 0, 1 ���� 30, 2���� 60
        clone.transform.position = Vector3.up * distanceToNext * areaIndex;

        
        areaIndex++;
    }
}
