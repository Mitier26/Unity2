using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject clickUpgradesSelected;
    public GameObject productionUpgradesSelected;
    public GameObject generatorUpgradesSelected;

    public TMP_Text clickUpgradesTitleText;
    public TMP_Text productionUpgradesTitleText;
    public TMP_Text generatorUpgradesTitleText;

    public GameObject HomeScreen;
    public GameObject SettingScreen;

    public void SwitchUpgrades(string location)
    {
        UpgradeManager.upgradeManager.UpgradeHandlers[0].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.upgradeManager.UpgradeHandlers[1].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.upgradeManager.UpgradeHandlers[2].UpgradesScroll.gameObject.SetActive(false);

        clickUpgradesSelected.SetActive(false);
        productionUpgradesSelected.SetActive(false);
        generatorUpgradesSelected.SetActive(false);

        clickUpgradesTitleText.color = Color.gray;
        productionUpgradesTitleText.color = Color.gray;
        generatorUpgradesTitleText.color = Color.gray;

        switch (location)
        {
            case "click":
                UpgradeManager.upgradeManager.UpgradeHandlers[0].UpgradesScroll.gameObject.SetActive(true);
                clickUpgradesSelected.SetActive(true);
                clickUpgradesTitleText.color = Color.white;
                break;
            case "production":
                UpgradeManager.upgradeManager.UpgradeHandlers[1].UpgradesScroll.gameObject.SetActive(true);
                productionUpgradesSelected.SetActive(true);
                productionUpgradesTitleText.color = Color.white;
                break;
            case "generator":
                UpgradeManager.upgradeManager.UpgradeHandlers[2].UpgradesScroll.gameObject.SetActive(true);
                generatorUpgradesSelected.SetActive(true);
                generatorUpgradesTitleText.color = Color.white;
                break;
        }
    }

    public void Navigate(string location)
    {
        HomeScreen.SetActive(false);
        SettingScreen.SetActive(false);

        switch(location)
        {
            case "Home":
                HomeScreen.SetActive(true);
                break;
            case "Setting":
                SettingScreen.SetActive(true);
                break;

        }
    }
}
