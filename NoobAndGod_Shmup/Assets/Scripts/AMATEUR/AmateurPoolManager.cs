using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurPoolManager : MonoBehaviour
{
    public static AmateurPoolManager instance;

    [SerializeField] private GameObject[] spawnPrefabs;

    private List<GameObject>[] pools;
    private GameObject[] parents;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        pools = new List<GameObject>[spawnPrefabs.Length];
        parents = new GameObject[spawnPrefabs.Length];
            
        for(int i = 0; i < pools.Length; i++)
        {
            parents[i] = new GameObject(spawnPrefabs[i].name);
            parents[i].transform.SetParent(transform);
            
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetObject(AmateurObject amateurObject)
    {
        GameObject go = null;
        foreach (GameObject obj in pools[(int)amateurObject])
        {
            if(!obj.activeSelf)
            {
                go = obj;
                go.SetActive(true);
                break;
            }    
        }

        if(!go)
        {
            go = Instantiate(spawnPrefabs[(int)amateurObject], transform);
            go.transform.SetParent(parents[(int)amateurObject].transform);
            pools[(int)amateurObject].Add(go);
        }

        return go;
    }

}
