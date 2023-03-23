using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurSpawner : MonoBehaviour
{
    // 3���� 1���� ��ȯ �ؾ��Ѵ�. ����, ��ü, �����
    // ��ȯ�� �Ϳ� ���� �ٸ��� �������� �Ѵ�.
    // ����ǥ��, ��Ƽ�����͸� ������ �Ѵ�.

    // ���ݴ� �ڵ��� ���Ǽ��� ���� 0,1,2 ��� ���������� �����.
    private enum ObjectType { Coin, Obstacle, Fish }
    private ObjectType objType;
    
    // ��ȯ�� ������Ʈ
    [SerializeField]
    private UnityEngine.GameObject[] objectPrefabs;

    // ��ȯ�� ������Ʈ���� ���� �θ�
    [SerializeField]
    Transform[] transforms;

    // ��ȯ�� ����
    private float border = 2.5f;
    // ����� ���� ���� Ȯ�ο�
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
        // ������ ���� �۵� �Ǵ� �� ������ �Ѵ�.
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
        // ���߿� isLive�� �����Ѵ�.
        while(AmateurManager.instance.isPlay)
        {
            UnityEngine.GameObject spawnObj;

            // ��ȯ�� �� ����
            float selectNum = Random.Range(0, 100);
            if ( selectNum < 16)
            {
                spawnObj = GetObject((int)ObjectType.Coin);
            }
            else
            {
                spawnObj = GetObject((int)ObjectType.Obstacle);
            }

            // ��ȯ�� ��ġ ����
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
            spawnObj.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.1f;    // ������ ���� ����
            spawnObj.SetActive(true);
            yield return new WaitForSeconds(AmateurManager.instance.spawnObjectInterval);     // spawnInterval �� ���� GameManager
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