using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text coinText;
    public Text coinValueText;

    public double coins;
    public double coinsClickValue;

    public Text coinsPerSecText;

    public Text clickUpgrade1Text;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public double coinsPerSecond;

    public int clickUpgrade1Level;
    public double clickUpgrade1Cost;
    public double clickUpgrade1Power;


    public int productionUpgrade1Level;
    public double productionUpgrade1Cost;
    public double productionUpgrade1Power;

    public int clickUpgrade2Level;
    public double clickUpgrade2Cost;
    public double clickUpgrade2Power;

    public int productionUpgrade2Level;
    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;

    private void Start()
    {
        coins = 0;
        coinsClickValue = 1;
        clickUpgrade1Cost = 10;
        clickUpgrade2Cost = 100;

        productionUpgrade1Cost = 25;
        productionUpgrade2Cost = 250;
        productionUpgrade2Power = 5;

    }

    private void Update()
    {
        coinsPerSecond = productionUpgrade1Level + (productionUpgrade2Power * productionUpgrade2Level);

        if(coinsClickValue > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coinsClickValue))));
            var mantissa = (coinsClickValue / System.Math.Pow(10, exponent));
            coinValueText.text = "Click\n" + mantissa.ToString("F0") + "e" + exponent + " Coins";
        }
        else
        {
            coinValueText.text = "Click\n" + coinsClickValue.ToString("F0") + " + Coins";
        }

        if (coins > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coins))));
            var mantissa = (coins / System.Math.Pow(10, exponent));
            coinText.text = "Coins : " + mantissa.ToString("F0") + "e" + exponent + " Coins";
        }
        else
        {
            coinText.text = "Coins : " + coins.ToString("F0");
        }

        
        coinsPerSecText.text = coinsPerSecond.ToString("F0") + " coins/s";

        string clickUpgrade1CostString;
        if (clickUpgrade1Cost > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Cost))));
            var mantissa = (clickUpgrade1Cost / System.Math.Pow(10, exponent));
            clickUpgrade1CostString = mantissa.ToString("F0") + "e" + exponent + " Coins";
        }
        else
        {
            clickUpgrade1CostString = clickUpgrade1Cost.ToString("F0");
        }

        string clickUpgrade1LevelString;
        if (clickUpgrade1Level > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Level))));
            var mantissa = (clickUpgrade1Level / System.Math.Pow(10, exponent));
            clickUpgrade1LevelString =  mantissa.ToString("F0") + "e" + exponent + " Coins";
        }
        else
        {
            clickUpgrade1LevelString = clickUpgrade1Level.ToString("F0");
        }


        clickUpgrade1Text.text = "Click Upgrade 1 \nCost : " + clickUpgrade1CostString + "\nPower: +1 Click\nLevel : " + clickUpgrade1LevelString;
        clickUpgrade2Text.text = "Click Upgrade 2 \nCost : " + clickUpgrade2Cost.ToString("F0") + "coins\nPower: +5 Click\nLevel : " + clickUpgrade2Level;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost : " + productionUpgrade1Cost.ToString("F0") + " coins\nPower : +1 coins/s\nLevel : " + productionUpgrade1Level;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost : " + productionUpgrade2Cost.ToString("F0") + " coins\nPower :" + productionUpgrade2Power + " coins/s\nLevel : " + productionUpgrade2Level;

        coins += coinsPerSecond * Time.deltaTime;
    }

    public void ClikcCoin()
    {
        coins += coinsClickValue;
    }

    public void BuyClickUpgrade1()
    {
        if(coins >= clickUpgrade1Cost)
        {
            clickUpgrade1Level++;
            coins -= clickUpgrade1Cost;
            clickUpgrade1Cost *= 1.07f;
            coinsClickValue += 1;
        }
    }

    public void BuyClickUpgrade2()
    {
        if (coins >= clickUpgrade2Cost)
        {
            clickUpgrade2Level++;
            coins -= clickUpgrade2Cost;
            clickUpgrade2Cost *= 1.07f;
            coinsClickValue += 5;
        }
    }

    public void BuyProductinoUpgrade1()
    {
        if (coins >= productionUpgrade1Cost)
        {
            productionUpgrade1Level++;
            coins -= productionUpgrade1Cost;
            productionUpgrade1Cost *= 1.07f;
        }
    }

    public void BuyProductinoUpgrade2()
    {
        if (coins >= productionUpgrade2Cost)
        {
            productionUpgrade2Level++;
            coins -= productionUpgrade2Cost;
            productionUpgrade2Cost *= 1.07f;
        }
    }
}
