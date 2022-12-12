using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public List<Upgrade> Upgrades;
    public Upgrade UpgradePrefab;

    public ScrollRect UpgradesScroll;
    public Transform UpgradesPanel;

    public string[] UpgradeNames;

    public BigDouble[] UpgradesBaseCost;
    public BigDouble[] UpgradesCostMult;
    public BigDouble[] UpgradesBasePower;
    public BigDouble[] UpgradesUnlock;
}
