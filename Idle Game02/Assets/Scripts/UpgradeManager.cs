using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public List<Upgrade> clickUpgrades;
    public Upgrade clickUpgradePrefab;

    public List<Upgrade> productionUpgrades;
    public Upgrade productionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradeBasePower;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradeBasePower;

    private void Awake()
    {
        instance = this; 
    }
    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.Instance.data.clickUpgradeLevel, 4);

        clickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 1000 };
        clickUpgradeCostMult = new BigDouble[] { 1.24, 1.36, 1.55, 2 };
        clickUpgradeBasePower = new BigDouble[] { 1, 5, 10, 25};

        productionUpgradeNames = new[] { "+1 Potato/s", "+2 Potatoes/s", "+10 Potatoes/s", "+100 Potatoes/s" };
        productionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3};
        productionUpgradeBasePower = new BigDouble[] { 1, 2, 10, 100 };

        for (int  i = 0; i < Controller.Instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrade upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.upgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for(int i = 0; i < Controller.Instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrade upgrade = Instantiate(productionUpgradesPrefab, productionUpgradesPanel);
            upgrade.upgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
    }


    public void UpdateUpgradeUI(string type, int upgradeID = -1)
    {
        var data = Controller.Instance.data;

        switch(type)
        {
            case "click":
                if (upgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, upgradeID);
                break;
            case "production":
                if (upgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, upgradeID);
                break;
        }

        void UpdateUI(List<Upgrade> upgrades, List<int> upgradeLevels,string[] upgradeName, int ID)
        {
            upgrades[ID].levelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].costText.text = $"Cost: {UpgradeCost(type, ID)} Potatoes";
            upgrades[ID].naneText.text = upgradeName[ID];
        }
    }

    //public BigDouble Cost()
    //{
    //    return clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, controller.data.clickUpgradeLevel);
    //}

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.Instance.data;

        switch(type)
        {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], (BigDouble)data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], (BigDouble)data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.Instance.data;

        switch(type)
        {
            case "click": Buy(data.clickUpgradeLevel);
                break;
            case "production": Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.potatoes >= UpgradeCost(type, UpgradeID))
            {
                data.potatoes -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
            }

            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
