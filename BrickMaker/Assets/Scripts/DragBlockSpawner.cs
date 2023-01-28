using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;

    [SerializeField]
    private Transform[] blockSpawnPoints;                       // 드래그 가능한 블록이 배치되는 위치
    [SerializeField]
    private GameObject[] blockPrefabs;                          // 생성 가능한 블록 프리팹

    [SerializeField]
    private Vector3 spawnGapAmount = new Vector3(10, 0, 0);     // 처음 생성할 때 부모와 떨어진 거리


    // 외부에서 드래그 블록의 부모 배열 정보 접근
    public Transform[] BlockSpawnPoints => blockSpawnPoints;

    //private void Awake()
    public void SpawnBlocks()
    {
        // 외부에서 소환 명령을 전달하기 위해 변경
        StartCoroutine("OnSpawnBlocks");
    }

    private IEnumerator OnSpawnBlocks()
    {
        for(int i = 0; i < blockSpawnPoints.Length; ++i)
        {
            yield return new WaitForSeconds(0.1f);

            // 생성할 드래그 블록의 번호
            int index = Random.Range(0, blockPrefabs.Length);

            // 블로이 생성되는 위치
            Vector3 spawnPosition = blockSpawnPoints[i].position + spawnGapAmount;
            // 블록이 생성될 때 부모에서 오른쪽으로 10 만큰 떨어진 지점에 생성

            // 블록을 생성
            GameObject clone = Instantiate(blockPrefabs[index], spawnPosition, Quaternion.identity, blockSpawnPoints[i]);

            // 생성된 블록의 이동 에니메이션 재생
            //clone.GetComponent<DragBlock>().Setup(blockSpawnPoints[i].position);
            clone.GetComponent<DragBlock>().Setup(blockArrangeSystem, blockSpawnPoints[i].position);
        }
    }
}
