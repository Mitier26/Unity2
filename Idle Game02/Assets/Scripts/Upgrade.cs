using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public int upgradeID;
    public Image upgradeButton;
    public TMP_Text levelText;
    public TMP_Text nameText;
    public TMP_Text costText;

    public Image Fill;

    public void BuyClickUpgrade()
    {
        UpgradeManager.upgradeManager.BuyUpgrade("click", upgradeID);
    }

    public void BuyProductionUpgrade()
    {
        UpgradeManager.upgradeManager.BuyUpgrade("production", upgradeID);
    }

    public void BuyGeneratorUpgrade()
    {
        UpgradeManager.upgradeManager.BuyUpgrade("generator", upgradeID);
    }
}
