using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProPlayerBuildingUI : MonoBehaviour
{
    public Animator animator;
    public ProPlayerBuilding command;
    public ProPlayer player;

    public TextMeshProUGUI repairCost, upgradeBuildingCost, upgradeUnitCost, cheatText;

    public int[] unitUpgradeCost = { 100, 300, 500, 800 };
    public int[] buildingCost = { 50, 80, 130, 190, 250, 300, 400 };

    public Button repairButton, buildingButton, unitButton, cheatButton;

    float cost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            ReflashUI();
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
        cost = command.maxHp - command.hp;
        cost *= 5;

        //-----------------
        if (command.hp < command.maxHp)
        {
            repairButton.interactable = true;
            repairCost.text = cost + "Gold";
        }
        else
        {
            repairButton.interactable= false;
            repairCost.text = "최대 체력";
        }

        //-----------------
        if(command.maxHpLevel < buildingCost.Length)
        {
            buildingButton.interactable = true;
            upgradeBuildingCost.text = buildingCost[command.maxHpLevel] + "Gold";
        }
        else
        {
            buildingButton.interactable= false;
            upgradeBuildingCost.text = "최대 레벨";
        }

        //------------------
        if(player.level  < unitUpgradeCost.Length)
        {
            unitButton.interactable = true;
            upgradeUnitCost.text = unitUpgradeCost[player.level] + "Gold";
        }
        else
        {
            unitButton.interactable= false;
            upgradeUnitCost.text = "최대 레벨";
        }

        
    }

    public void RepairCommand()
    {
        command.hp = command.maxHp;
        ReflashUI();
    }

    public void UPgradeCommand()
    {
        command.AddHp();
        ReflashUI();
    }

    public void UpgradeUnit()
    {
        player.LevelUp();
        ReflashUI();
    }

    public void NoneButton()
    {
        ReflashUI();
    }
}
