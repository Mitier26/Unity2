using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using System.Linq;

public class Data
{
    public BigDouble potatoes;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;
    public Data()
    {
        potatoes = 0;

        //clickUpgradeLevel = Methods.CreateList<BigDouble>(4);
        clickUpgradeLevel = new int[4].ToList();
        productionUpgradeLevel = new int[4].ToList();
    }
}