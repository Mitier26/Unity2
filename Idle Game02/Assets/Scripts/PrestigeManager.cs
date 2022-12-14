using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Controller;

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager prestigeManager;
    public TMP_Text PrestigeGainsText;
    public TMP_Text PrestigeCurrencyText;

    public GameObject PrestigeConfirmation;

    private void Awake()
    {
        prestigeManager = this;
    }

    public BigDouble PrestigeGains()
    {
        return BigDouble.Sqrt(controller.data.potatoes / 1000);
    }

    public BigDouble PrestigeEffect()
    {
        return controller.data.goldenPotatoes / 100 + 1;
    }

    public void Update()
    {
        PrestigeGainsText.text = $"Prestige:\n +{ PrestigeGains().Notate()} Golend Potatoes";
        PrestigeCurrencyText.text = $"{controller.data.goldenPotatoes.Notate()} Goden Potates";
    }

    public void TogglePrestigeConfirm()
    {
        PrestigeConfirmation.SetActive(!PrestigeConfirmation.activeSelf);
    }

    public void Prestige()
    {
        var data = controller.data;

        data.goldenPotatoes += PrestigeGains();

        data.potatoes = 0;

        data.clickUpgradeLevel = new int[4].ToList();
        data.productionUpgradeLevel = new BigDouble[4].ToList();
        data.productionUpgradeGenerated = new BigDouble[4].ToList();
        data.generatorUpgradeLevel = new int[4].ToList();

        

        UpgradeManager.upgradeManager.UpdateUpgradeUI("production");
        UpgradeManager.upgradeManager.UpdateUpgradeUI("generator");
        UpgradeManager.upgradeManager.UpdateUpgradeUI("click");

        TogglePrestigeConfirm();
    }                  
}
