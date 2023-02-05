using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ������ ������ ����
    public GameObject[] prefabs;

    // Ǯ ��� �ϴ� ����Ʈ
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

        // ������ Ǯ���� �۵��ϰ� ���� �ʴ� ������Ʈ ����

        foreach (GameObject item in pools[index])
        {
            if( !item.activeSelf)
            {
                // �۵��ϰ� ���� �ʴ� ������Ʈ�� �߰��ϸ� ����
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

        // ��� ������Ʈ�� �۵��ϰ� �ִٸ�
            // ���ο� ������Ʈ�� ����

        return select;
    }

}
