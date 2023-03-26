using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProSpawnManager : MonoBehaviour
{
    public int level = 1;

    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private ProSpawner boxSpawner, fishSpawner, coinSpawner1, coinSpawner2, flySpawner1, flySpawner2, ballSpawner1, ballSpawner2;

    private float boxDelay = 1;
    private float coinDelay = 4;
    private float fishDelay = 5;
    private float flyDelay = 5;
    private float ballDelay = 5;

    private float flyPower = 5;
    private float ballPower = 10;

    // 레벨에 따라 각 소환기의 수치를 변경해야 한다.
    // 소환기의 수치는 전부 다름.

    private void Start()
    {
        levelText.text = "Level " + level;
        boxSpawner.spawnDelay = boxDelay;
        fishSpawner.spawnDelay = fishDelay;
        coinSpawner1.spawnDelay = coinDelay;
        coinSpawner2.spawnDelay = coinDelay;
        ballSpawner1.spawnDelay = ballDelay;
        ballSpawner2.spawnDelay = ballDelay;
        flySpawner1.spawnDelay = flyDelay;
        flySpawner2.spawnDelay = flyDelay;

        flySpawner1.spawnPower = flyPower;
        flySpawner2.spawnPower = flyPower;
        ballSpawner1.spawnPower = ballPower;
        ballSpawner2.spawnPower = ballPower;
    }


    public void AddLevel()
    {
        level++;
        levelText.text = "Level " + level;

        if(level > 8)
        {
            boxSpawner.spawnDelay = Mathf.Max(0.1f, (float)(ballDelay - (0.2 * (level -8))));
            fishSpawner.spawnDelay = Mathf.Max(0.5f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            coinSpawner1.spawnDelay = Mathf.Max(0.5f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            coinSpawner2.spawnDelay = Mathf.Max(0.5f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            ballSpawner1.spawnDelay = Mathf.Max(1f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            ballSpawner2.spawnDelay = Mathf.Max(1f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            flySpawner1.spawnDelay = Mathf.Max(1f, (float)(ballDelay - (0.2 * (level - 8)))); ;
            flySpawner2.spawnDelay = Mathf.Max(1f, (float)(ballDelay - (0.2 * (level - 8)))); ;

            flySpawner1.spawnPower = flyPower;
            flySpawner2.spawnPower = flyPower;
            ballSpawner1.spawnPower = ballPower;
            ballSpawner2.spawnPower = ballPower;
        }

        switch(level)
        {
            case 2:
                coinSpawner1.gameObject.SetActive(true);
                break;
            case 3:
                fishSpawner.gameObject.SetActive(true);
                break;
            case 4:
                coinSpawner2.gameObject.SetActive(true);
                break;
            case 5:
                ballSpawner1.gameObject.SetActive(true);
                break;
            case 6:
                flySpawner1.gameObject.SetActive(true);
                break;
            case 7:
                ballSpawner2.gameObject.SetActive(true);
                break;
            case 8:
                flySpawner2.gameObject.SetActive(true);
                break;
        }
    }
}
