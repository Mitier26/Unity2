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

    public void SwitchUpgrades(string location)
    {
        UpgradeManager.instance.UpgradeHandlers[0].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.instance.UpgradeHandlers[1].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.instance.UpgradeHandlers[2].UpgradesScroll.gameObject.SetActive(false);

        clickUpgradesSelected.SetActive(false);
        productionUpgradesSelected.SetActive(false);
        generatorUpgradesSelected.SetActive(false);

        clickUpgradesTitleText.color = Color.gray;
        productionUpgradesTitleText.color = Color.gray;
        generatorUpgradesTitleText.color = Color.gray;

        switch (location)
        {
            case "click":
                UpgradeManager.instance.UpgradeHandlers[0].UpgradesScroll.gameObject.SetActive(true);
                clickUpgradesSelected.SetActive(true);
                clickUpgradesTitleText.color = Color.white;
                break;
            case "production":
                UpgradeManager.instance.UpgradeHandlers[1].UpgradesScroll.gameObject.SetActive(true);
                productionUpgradesSelected.SetActive(true);
                productionUpgradesTitleText.color = Color.white;
                break;
            case "generator":
                UpgradeManager.instance.UpgradeHandlers[2].UpgradesScroll.gameObject.SetActive(true);
                generatorUpgradesSelected.SetActive(true);
                generatorUpgradesTitleText.color = Color.white;
                break;
        }
    }
}
