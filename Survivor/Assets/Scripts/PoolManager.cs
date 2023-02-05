using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당 하는 리스트
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에서 작동하고 있지 않는 오브젝트 접근

        foreach (GameObject item in pools[index])
        {
            if( !item.activeSelf)
            {
                // 작동하고 있지 않는 오브젝트를 발견하면 리턴
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        // 모든 오브젝트가 작동하고 있다면
            // 새로운 오브젝트를 생성

        return select;
    }

}
