using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int level;
    public int health;
    public int maxHealth = 100;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};

    [Header("# GameObject")]
    public Player player;
    public PoolManager pool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp >= nextExp[level])
        {
            exp -= nextExp[level];
            level++;
        }
    }
}
