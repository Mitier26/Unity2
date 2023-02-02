using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyBPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletEnemyCPrefab;
    public GameObject bulletEnemyDPrefab;
    public GameObject bulletFollowerPrefab;

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;
    GameObject[] enemyB;

    GameObject[] itemCoin;
    GameObject[] itemBoom;
    GameObject[] itemPower;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletEnemyC;
    GameObject[] bulletEnemyD;

    GameObject[] bulletFollower;

    GameObject[] targetPool;

    private void Awake()
    {
        enemyL = new GameObject[20];
        enemyM = new GameObject[20];
        enemyS = new GameObject[20];
        enemyB = new GameObject[1];

        itemCoin = new GameObject[20];
        itemBoom = new GameObject[10];
        itemPower = new GameObject[10];

        bulletPlayerA = new GameObject[200];
        bulletPlayerB = new GameObject[200];
        bulletEnemyA = new GameObject[200];
        bulletEnemyB = new GameObject[200];
        bulletEnemyC = new GameObject[200];
        bulletEnemyD = new GameObject[200];
        bulletFollower = new GameObject[200];

        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < enemyB.Length; i++)
        {
            enemyB[i] = Instantiate(enemyBPrefab);
            enemyB[i].SetActive(false);
        }
        for (int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] =  Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }
        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }
        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
        }

        for (int i = 0; i < itemCoin.Length; i++)
        {
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }
        for (int i = 0; i < itemBoom.Length; i++)
        {
            itemBoom[i] = Instantiate(itemBoomPrefab);
            itemBoom[i].SetActive(false);
        }
        for (int i = 0; i < itemPower.Length; i++)
        {
            itemPower[i] = Instantiate(itemPowerPrefab);
            itemPower[i].SetActive(false);
        }

        for (int i = 0; i < bulletEnemyA.Length; i++)
        {
            bulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[i].SetActive(false);
        }
        for (int i = 0; i < bulletEnemyB.Length; i++)
        {
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
        }
        for (int i = 0; i < bulletEnemyC.Length; i++)
        {
            bulletEnemyC[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyC[i].SetActive(false);
        }
        for (int i = 0; i < bulletEnemyD.Length; i++)
        {
            bulletEnemyD[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyD[i].SetActive(false);
        }
        for (int i = 0; i < bulletPlayerA.Length; i++)
        {
            bulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[i].SetActive(false);
        }
        for (int i = 0; i < bulletPlayerB.Length; i++)
        {
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
        }
        for (int i = 0; i < bulletFollower.Length; i++)
        {
            bulletFollower[i] = Instantiate(bulletFollowerPrefab);
            bulletFollower[i].SetActive(false);
        }
    }

    public GameObject MakeObject(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletEnemyC":
                targetPool = bulletEnemyC;
                break;
            case "BulletEnemyD":
                targetPool = bulletEnemyD;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletEnemyC":
                targetPool = bulletEnemyC;
                break;
            case "BulletEnemyD":
                targetPool = bulletEnemyD;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }

        return targetPool;
    }
}
