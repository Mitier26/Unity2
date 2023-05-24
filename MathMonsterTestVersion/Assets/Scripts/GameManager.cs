using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;

    public int level;
    public int exp = 0;
    public int[] nextExp = { 10, 20, 30, 40, 50 };
    public int energy = 0;

    public Slider expSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI energyText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levelText.text = "Level : " + (level + 1);
        energyText.text = "Energy : " + energy;

        expSlider.value = (float)exp / nextExp[level];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            AddExp(1);
        }
    }

    public void AddExp(int point)
    {
        exp += point;

        if(exp >= nextExp[level])
        {
            exp = exp - nextExp[level];
            level++;
        }
        expSlider.value = (float)exp / nextExp[level];
        levelText.text = "Level : " + (level + 1);
    }

    public void AddEnergy(int point)
    {
        energy += point;
        energyText.text = "Energy : " + energy;
    }

}
