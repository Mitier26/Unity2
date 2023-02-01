using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    
    int index = 0;
    
    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }

    public bool isSequenceRandom;
    private float chageTimer;
    private float intervalTimer;
    private float[] rotations;

    private void Start()
    {
        chageTimer = GetSpawnData().cooldown;
        intervalTimer = GetSpawnData().interval;
        rotations = new float[GetSpawnData().numberOfBullets];

        if(!GetSpawnData().isRandom)
        {
            DistributedRotations();
        }
    }

    private void Update()
    {
        if(intervalTimer <= 0)
        {
            if(chageTimer <= 0)
            {
                if (isSequenceRandom)
                {
                    index = Random.Range(0, spawnDatas.Length);
                }
                else
                {
                    index += 1;
                    if (index >= spawnDatas.Length) index = 0;
                }
                chageTimer = GetSpawnData().cooldown;
            }
            SpawnBullets();
            intervalTimer = GetSpawnData().interval;
        }
       
        intervalTimer -= Time.deltaTime;
        chageTimer -= Time.deltaTime;
    }

    public float[] DistributedRotations()
    {
        for(int i =0; i < GetSpawnData().numberOfBullets; i++)
        {
            var fraction = (float)i / (float)(GetSpawnData().numberOfBullets -1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;

            rotations[i] = fractionOfDifference + GetSpawnData().minRotation;
        }

        return rotations;
    }

    public float[] RandomRatations()
    {
        for(int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }

        return rotations;
    }

    public GameObject[] SpawnBullets()
    {
       
        if (GetSpawnData().isRandom)
        {
            RandomRatations();
        }

        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberOfBullets];

        for(int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetBulletFromPool();

            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletPrefab, transform);
                BulletManager.bullets.Add(spawnedBullets[i]);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(transform);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.moveSpeed = GetSpawnData().bulletSpeed;
            b.direction = GetSpawnData().bulletDiection;

            if(GetSpawnData().isParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }

        return spawnedBullets;
    }
}
