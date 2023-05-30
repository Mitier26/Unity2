using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProPlayerBuildingUI : MonoBehaviour
{
    public Animator animator;
    public ProPlayerBuilding command;

    public TextMeshProUGUI repairCost, upgradeBuildingCost, upgradeUnitCost, cheatText;

    public int[] unitUpgradeCost = { 100, 300, 500, 800 };
    public int[] buildingCost = { 50, 80, 130, 190, 250, 300, 400 };

    public Button repairButton, buildingButton, unitButton, cheatButton;

    private void Start()
    {
        ReflashUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("isComing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isComing", false);
        }
    }

    private void ReflashUI()
    {
        if (command.hp < command.maxHp)
        {
            repairButton.interactable = true;
        }
    }

    public void RepairCommand()
    {

    }

    public void UPgradeCommand()
    {

    }

    public void UpgradeUnit()
    {

    }

    public void NoneButton()
    {

    }
}
