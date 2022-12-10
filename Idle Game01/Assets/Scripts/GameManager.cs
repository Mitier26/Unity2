using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text coinText;
    public double coins;
    public double coinsClickValue;

    public Text coinsPerSecText;

    public Text clickUpgrade1Text;
    public Text productionUpgrade1Text;

    public double coinsPerSecond;
    public double clickUpgrade1Cost;
    public double clickUpgrade1Level;
    public double clickUpgrade1Power;

    public double productionUpgrade1Cost;
    public int productionUpgrade1Level;

    private void Start()
    {
        coins = 0;
        clickUpgrade1Cost = 10;
        productionUpgrade1Cost = 25;
    }

    private void Update()
    {
        coinsPerSecond = productionUpgrade1Level;

        coinText.text = "Coins : " + coins.ToString("F0");
        coinsPerSecText.text = coinsPerSecond + " coins/s";
        clickUpgrade1Text.text = "Click Upgrade 1 \nCost : " + clickUpgrade1Cost.ToString("F0") + "coins\nPower: +1 Click\nLevel : " + clickUpgrade1Level;
        productionUpgrade1Text.text = "Production Upgrade 1\nCost : " + productionUpgrade1Cost.ToString("F0") + " coins\nPower : +1 coins/s\nLevel : " + productionUpgrade1Level;

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
            coinsClickValue++;
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
}
