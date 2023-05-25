using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProGameManager : MonoBehaviour
{
    public static ProGameManager instance;      // ���� �޴��� �̱���
    public PoolManager poolManager;             // ���� �����Ǵ� ������ poolManager�� �����ϱ� ���� ��
    public GameObject points;                   // ���� �����Ǵ� ������ �̵��ϴ� ��ġ�� �����ϱ� ���� ��
    public Transform[] minePoints;
    public GameObject command;

    public int enemyGold;                       // ���� ��
    public int enemyCount;                      // ���� ��
    public int enemyMaxCount = 30;              // ���� �ִ� �� 
    public int enemyWorkerCount;                // ���� �ϲ� ��
    public int enemyWorkerMaxCount = 5;         // ���� �ϲ� �ִ� ��

    private int playerGold;                     // �÷��̾� ��
    public int PlayerGold                       // UI�� ǥ���ϴ� ���� ���ϰ� �ϱ� ���� ������Ƽ�� �����.
    {
        get { return playerGold; }
        set { playerGold = value; }
    }

    public float playTime;                      // ��ŷ�� �ð� ����


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
