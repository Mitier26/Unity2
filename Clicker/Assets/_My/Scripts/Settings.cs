using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private BigInteger attackDMG = 1;
    private BigInteger enemyHP = 3;
    private BigInteger newEnenyHP;

    private BigInteger gold = 0;
    private BigInteger payGold = 1;
    private BigInteger dropGold = 1;

    public int stage = 1;
    public int enemyCount = 6;

    private void Start()
    {
        newEnenyHP = enemyHP;
    }

    public bool IsEnemyDie()
    {
        bool result = false;

        enemyHP -= attackDMG;

        if(enemyHP <= 0)
        {
            enemyHP = 0;
            result = true;
        }

        return result;
    }

    public void InitEnemyHP()
    {
        BigInteger hp = (BigInteger)((float)enemyHP * 1.8f);
        enemyHP = hp;
        newEnenyHP = enemyHP;
    }

    public void GetEnemyHP()
    {
        enemyHP = newEnenyHP;
    }

    public BigInteger GetGold()
    {
        dropGold = BigInteger.Pow(2, stage) / 2;

        if (dropGold < 1) dropGold = 1;

        gold += dropGold;

        return gold;
    }

    public void LvUpPayGold()
    {
        if(gold >= payGold)
        {
            gold -= payGold;
            attackDMG += 1;
            payGold += (BigInteger)((float)payGold * 1.2f);
        }
    }

    public float GetEnemyHpVal()
    {
        float hp = (float)enemyHP / (float)newEnenyHP;

        return hp;
    }

    private string FormaNum(BigInteger num)
    {
        string[] unit = { "", "K", "M", "Y", "T" };
        int unitIndex = 0;

        while(num > 1000 && unitIndex < unit.Length - 1)
        {
            num /= 1000;
            unitIndex++;
        }

        string fNum = string.Format("{0}{1}", num.ToString(), unit[unitIndex]);

        return fNum;
    }


    public string stringGold()
    {
        return FormaNum(gold);
    }
    public string stringPayGold()
    {
        return FormaNum(payGold);
    }

    public void SumGold()
    {
        gold += 999;
    }
}
