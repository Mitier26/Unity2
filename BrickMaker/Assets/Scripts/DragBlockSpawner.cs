using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;

    [SerializeField]
    private Transform[] blockSpawnPoints;                       // �巡�� ������ ����� ��ġ�Ǵ� ��ġ
    [SerializeField]
    private GameObject[] blockPrefabs;                          // ���� ������ ��� ������

    [SerializeField]
    private Vector3 spawnGapAmount = new Vector3(10, 0, 0);     // ó�� ������ �� �θ�� ������ �Ÿ�


    // �ܺο��� �巡�� ����� �θ� �迭 ���� ����
    public Transform[] BlockSpawnPoints => blockSpawnPoints;

    //private void Awake()
    public void SpawnBlocks()
    {
        // �ܺο��� ��ȯ ����� �����ϱ� ���� ����
        StartCoroutine("OnSpawnBlocks");
    }

    private IEnumerator OnSpawnBlocks()
    {
        for(int i = 0; i < blockSpawnPoints.Length; ++i)
        {
            yield return new WaitForSeconds(0.1f);

            // ������ �巡�� ����� ��ȣ
            int index = Random.Range(0, blockPrefabs.Length);

            // ����� �����Ǵ� ��ġ
            Vector3 spawnPosition = blockSpawnPoints[i].position + spawnGapAmount;
            // ����� ������ �� �θ𿡼� ���������� 10 ��ū ������ ������ ����

            // ����� ����
            GameObject clone = Instantiate(blockPrefabs[index], spawnPosition, Quaternion.identity, blockSpawnPoints[i]);

            // ������ ����� �̵� ���ϸ��̼� ���
            //clone.GetComponent<DragBlock>().Setup(blockSpawnPoints[i].position);
            clone.GetComponent<DragBlock>().Setup(blockArrangeSystem, blockSpawnPoints[i].position);
        }
    }
}
