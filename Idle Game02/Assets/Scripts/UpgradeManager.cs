using BreakInfinity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager upgradeManager;

    public UpgradeHandler[] UpgradeHandlers;


    private void Awake()
    {
        upgradeManager = this; 
    }
    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.controller.data.clickUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.controller.data.productionUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.controller.data.productionUpgradeGenerated, 4);
        Methods.UpgradeCheck(Controller.controller.data.generatorUpgradeLevel, 4);

        UpgradeHandlers[0].UpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        UpgradeHandlers[1].UpgradeNames = new[] { "+1 Potato/s", "+2 Potatoes/s", "+10 Potatoes/s", "+100 Potatoes/s" };
        UpgradeHandlers[2].UpgradeNames = new[] 
        { 
            $"produces +0.1 \"{UpgradeHandlers[1].UpgradeNames[0]}\" Upgraders/s",
            $"produces +0.05 \"{UpgradeHandlers[1].UpgradeNames[1]}\" Upgraders/s",
            $"produces +0.02 \"{UpgradeHandlers[1].UpgradeNames[2]}\" Upgraders/s",
            $"produces +0.01 \"{UpgradeHandlers[1].UpgradeNames[3]}\" Upgraders/s"
        };
        
        
        UpgradeHandlers[0].UpgradesBaseCost = new BigDouble[] { 10, 50, 100, 1000 };
        UpgradeHandlers[0].UpgradesCostMult = new BigDouble[] { 1.24, 1.36, 1.55, 2 };
        UpgradeHandlers[0].UpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };
        UpgradeHandlers[0].UpgradesUnlock = new BigDouble[] { 0, 25, 50, 500 };

        UpgradeHandlers[1].UpgradesBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        UpgradeHandlers[1].UpgradesCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        UpgradeHandlers[1].UpgradesBasePower = new BigDouble[] { 1, 2, 10, 10 };
        UpgradeHandlers[1].UpgradesUnlock = new BigDouble[] { 0, 50, 500, 5000 };

        UpgradeHandlers[2].UpgradesBaseCost = new BigDouble[] { 5000, 1e4, 1e5, 1e6 };
        UpgradeHandlers[2].UpgradesCostMult = new BigDouble[] { 1.23, 1.5, 2, 2.5 };
        UpgradeHandlers[2].UpgradesBasePower = new BigDouble[] { 0.1, 0.05, 0.02, 0.01 };
        UpgradeHandlers[2].UpgradesUnlock = new BigDouble[] { 2500, 5e3, 5e3, 5e5 };

        CreateUpgrades(Controller.controller.data.clickUpgradeLevel, 0);
        CreateUpgrades(Controller.controller.data.productionUpgradeLevel, 1);
        CreateUpgrades(Controller.controller.data.generatorUpgradeLevel, 2);

        void CreateUpgrades<T>(List<T> level, int index)
        {
            for (int i = 0; i < level.Count; i++)
            {
                Upgrade upgrade = Instantiate(UpgradeHandlers[index].UpgradePrefab, UpgradeHandlers[index].UpgradesPanel);
                upgrade.upgradeID = i;
                upgrade.gameObject.SetActive(false);
                UpgradeHandlers[index].Upgrades.Add(upgrade);
            }

            UpgradeHandlers[index].UpgradesScroll.normalizedPosition = new Vector2(0, 0);
        }

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
        UpdateUpgradeUI("generator");
    }

    public void Update()
    {
        UpgradeUnlockSystem(Controller.controller.data.potatoes, UpgradeHandlers[0].UpgradesUnlock, 0);
        UpgradeUnlockSystem(Controller.controller.data.potatoes, UpgradeHandlers[1].UpgradesUnlock, 1);
        UpgradeUnlockSystem(Controller.controller.data.potatoes, UpgradeHandlers[2].UpgradesUnlock, 2);

        void UpgradeUnlockSystem(BigDouble currency, BigDouble[] unlock, int index)
        {
            for (var i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                if (!UpgradeHandlers[index].Upgrades[i].gameObject.activeSelf)
                    UpgradeHandlers[index].Upgrades[i].gameObject.SetActive(currency >= unlock[i]);
        }

        if (UpgradeHandlers[1].UpgradesScroll.gameObject.activeSelf) UpdateUpgradeUI("production");
    }

    public void UpdateUpgradeUI(string type, int upgradeID = -1)
    {
        var data = Controller.controller.data;

        switch(type)
        {
            case "click":
                UpdateAllUI(UpgradeHandlers[0].Upgrades, data.clickUpgradeLevel, UpgradeHandlers[0].UpgradeNames, 0, upgradeID, type);
                break;
            case "production":
                UpdateAllUI(UpgradeHandlers[1].Upgrades, data.productionUpgradeLevel, UpgradeHandlers[1].UpgradeNames, 1, upgradeID, type, data.productionUpgradeGenerated);
                break;
            case "generator":
                UpdateAllUI(UpgradeHandlers[2].Upgrades, data.generatorUpgradeLevel, UpgradeHandlers[2].UpgradeNames, 2, upgradeID, type);
                break;
        }
    }

    private void UpdateAllUI(List<Upgrade> upgrades, List<int> upgradeLevels, string[] upgradeName, int index, int upgradeID, string type)
    {
        if (upgradeID == -1)
            for (int i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                UpdateUI(i);
        else UpdateUI(upgradeID);

        void UpdateUI(int ID)
        {
            
            upgrades[ID].levelText.text = upgradeLevels[ID].ToString("F0");
            upgrades[ID].costText.text = $"Cost: {UpgradeCost(type, ID).Notate()} Potatoes";
            upgrades[ID].naneText.text = upgradeName[ID];
        }
    }

    private void UpdateAllUI(List<Upgrade> upgrades, List<BigDouble> upgradeLevels, string[] upgradeName, int index, int upgradeID, string type, List<BigDouble> upgradesGenerated = null)
    {
        if (upgradeID == -1)
            for (int i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                UpdateUI(i);
        else UpdateUI(upgradeID);

        void UpdateUI(int ID)
        {
            BigDouble generated = upgradesGenerated == null ? 0 : upgradesGenerated[ID];
            upgrades[ID].levelText.text = (upgradeLevels[ID] + generated).ToString("F2");
            upgrades[ID].costText.text = $"Cost: {UpgradeCost(type, ID)} Potatoes";
            upgrades[ID].naneText.text = upgradeName[ID];
        }
    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.controller.data;

        switch(type)
        {
            case "click":
                return UpgradeCost_Int(0, data.clickUpgradeLevel, UpgradeID);
            case "production":
                return UpgradeCost_BigDouble(1, data.productionUpgradeLevel, UpgradeID);
            case "generator":
                return UpgradeCost_Int(2, data.generatorUpgradeLevel, UpgradeID);
        }

        return 0;
    }

    private BigDouble UpgradeCost_BigDouble(int index, List<BigDouble> levels, int UpgradeID)
    {
        return UpgradeHandlers[index].UpgradesBaseCost[UpgradeID] * BigDouble.Pow(UpgradeHandlers[index].UpgradesCostMult[UpgradeID], levels[UpgradeID]);
    }

    private BigDouble UpgradeCost_Int(int index, List<int> levels, int UpgradeID)
    {
        return UpgradeHandlers[index].UpgradesBaseCost[UpgradeID] * BigDouble.Pow(UpgradeHandlers[index].UpgradesCostMult[UpgradeID], (BigDouble)levels[UpgradeID]);
    }


    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.controller.data;

        switch(type)
        {
            case "click": 
                Buy(data.clickUpgradeLevel, type, UpgradeID);
                break;
            case "production": 
                Buy(data.productionUpgradeLevel, type, UpgradeID);
                break;
            case "generator":
                Buy(data.generatorUpgradeLevel, type, UpgradeID);
                break;
        }

        
    }

    private void Buy(List<int> upgradeLevels, string type, int UpgradeID)
    {
        var data = Controller.controller.data;

        if (data.potatoes >= UpgradeCost(type, UpgradeID))
        {
            data.potatoes -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID] += 1;
        }

        UpdateUpgradeUI(type, UpgradeID);
    }

    private void Buy(List<BigDouble> upgradeLevels, string type, int UpgradeID)
    {
        var data = Controller.controller.data;

        if (data.potatoes >= UpgradeCost(type, UpgradeID))
        {
            data.potatoes -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID] += 1;
        }

        UpdateUpgradeUI(type, UpgradeID);
    }
}
