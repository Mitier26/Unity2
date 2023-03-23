using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurSpawner : MonoBehaviour
{
    // 3개중 1개를 소환 해야한다. 코인, 물체, 물고기
    // 소환한 것에 따라 다르게 움직여야 한다.
    // 위험표시, 인티케이터를 만들어야 한다.

    // 조금더 코드의 편의성을 위해 0,1,2 대신 열거형으로 만든다.
    private enum ObjectType { Coin, Obstacle, Fish }
    private ObjectType objType;
    
    // 소환될 오브젝트
    [SerializeField]
    private UnityEngine.GameObject[] objectPrefabs;

    // 소환된 오브젝트들을 담을 부모
    [SerializeField]
    Transform[] transforms;

    // 소환의 범위
    private float border = 2.5f;
    // 물고기 등장 시작 확인용
    public bool isFishing = false;

    private void Start()
    {
        ObjectSetting();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnObjects());
    }

    public void StartFishing()
    {
        if (!isFishing)
        {
            isFishing = true;
            StartCoroutine(SpawnFish());
        }
    }

    private void ObjectSetting()
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                UnityEngine.GameObject obj = Instantiate(objectPrefabs[i], transforms[i]);
                obj.SetActive(false);
            }
        }
    }

    private IEnumerator SpawnFish()
    {
        UnityEngine.GameObject selected;
        // 레벨에 따라 작동 되는 것 만들어야 한다.
        while (AmateurManager.instance.isPlay)
        {
            selected = GetObject((int)ObjectType.Fish);
            selected.transform.position = new Vector3(Random.Range(-border, border), 0, 0);
            selected.SetActive(true);

            yield return new WaitForSeconds(AmateurManager.instance.spawnFishInterval);
        }
    }


    private IEnumerator SpawnObjects()
    {
        // 나중에 isLive로 변경한다.
        while(AmateurManager.instance.isPlay)
        {
            UnityEngine.GameObject spawnObj;

            // 소환할 것 선택
            float selectNum = Random.Range(0, 100);
            if ( selectNum < 16)
            {
                spawnObj = GetObject((int)ObjectType.Coin);
            }
            else
            {
                spawnObj = GetObject((int)ObjectType.Obstacle);
            }

            // 소환할 위치 선택
            selectNum = Random.Range(0, 100);
            Vector3 spawnPosition = Vector3.zero;

            if( selectNum < 30)
            {
                Transform player = UnityEngine.GameObject.FindGameObjectWithTag("Player").transform;
                if(player.position.x < 0 )
                {
                    spawnPosition = new Vector3(player.position.x + Random.Range(0,1f), transform.position.y, transform.position.z);
                }
                else
                {
                    spawnPosition = new Vector3(player.position.x - Random.Range(0, 1f), transform.position.y, transform.position.z);
                }
            }
            else
            {
                spawnPosition = new Vector3(Random.Range(-border, border), transform.position.y, transform.position.z);
            }

            spawnObj.transform.position = spawnPosition;
            spawnObj.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.1f;    // 레벨에 따라 변경
            spawnObj.SetActive(true);
            yield return new WaitForSeconds(AmateurManager.instance.spawnObjectInterval);     // spawnInterval 로 변경 GameManager
        }
    }
    

    private UnityEngine.GameObject GetObject(int index)
    {
        UnityEngine.GameObject selected = null;

        for (int i = 0; i < transforms[index].childCount; i++)
        {
            if (!transforms[index].GetChild(i).gameObject.activeSelf)
            {
                selected = transforms[index].GetChild(i).gameObject;
                break;
            }
        }

        if(selected == null)
        {
            selected = Instantiate(objectPrefabs[index], transforms[index]);
        }

        return selected;
    }
}