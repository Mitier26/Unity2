using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static List<GameObject> bullets;

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    public static GameObject GetBulletFromPool()
    {
        for(int i =0; i< bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeSelf)
            {
                bullets[i].GetComponent<Bullet>().ResetTimer();
                bullets[i].SetActive(true);
                return bullets[i];
            }
        }
        return null;
    }
}
