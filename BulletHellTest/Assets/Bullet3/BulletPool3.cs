using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool3 : MonoBehaviour
{
    public static BulletPool3 Instance;

    [SerializeField] private GameObject poolBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if(bullets.Count > 0)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if ((!bullets[i].activeInHierarchy))
                {
                    return bullets[i];
                }
            }
        }

        if(notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(poolBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }
}
