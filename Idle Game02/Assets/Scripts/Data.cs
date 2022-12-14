using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using System.Linq;
using System;

[Serializable]
public class Data
{
    public BigDouble potatoes;
    public BigDouble goldenPotatoes;

    public List<int> clickUpgradeLevel;
    public List<BigDouble> productionUpgradeLevel;
    public List<BigDouble> productionUpgradeGenerated;
    public List<int> generatorUpgradeLevel;

    public int notation;
    public Data()
    {
        potatoes = 0;
        goldenPotatoes = 0;

        //clickUpgradeLevel = Methods.CreateList<BigDouble>(4);
        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new BigDouble[4].ToList();
        productionUpgradeGenerated = new BigDouble[4].ToList();
        generatorUpgradeLevel = new int[4].ToList();

        notation = 0;
    }
}