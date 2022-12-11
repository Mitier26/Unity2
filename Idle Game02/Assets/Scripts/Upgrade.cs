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
    public TMP_Text naneText;
    public TMP_Text costText;

    public void BuyClickUpgrade()
    {
        UpgradeManager.instance.BuyUpgrade("click", upgradeID);
    }

    public void BuyProductionUpgrade()
    {
        UpgradeManager.instance.BuyUpgrade("production", upgradeID);
    }
}
