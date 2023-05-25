using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProGameManager : MonoBehaviour
{
    public static ProGameManager instance;      // 게임 메니져 싱글톤
    public PoolManager poolManager;             // 새로 생성되는 적들이 poolManager를 연결하기 위한 것
    public GameObject points;                   // 새로 생성되는 적들이 이동하는 위치를 연결하기 위한 것
    public Transform[] minePoints;
    public GameObject command;

    public int enemyGold;                       // 적의 돈
    public int enemyCount;                      // 적의 수
    public int enemyMaxCount = 30;              // 적의 최대 수 
    public int enemyWorkerCount;                // 적의 일꾼 수
    public int enemyWorkerMaxCount = 5;         // 적의 일꾼 최대 수

    private int playerGold;                     // 플레이어 돈
    public int PlayerGold                       // UI에 표시하는 것을 편하게 하기 위해 프로퍼티로 만든다.
    {
        get { return playerGold; }
        set { playerGold = value; }
    }

    public float playTime;                      // 랭킹용 시간 측정


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
