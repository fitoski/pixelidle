using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // buttonlar
    public Text goldText;
    public Text goldPerSecText;
    public Text clickPowerText;
    public Button clickUpgradeButton;
    public Button goldPerSecUpgradeButton;
    public Button goldPerSecUpgradeButton1;
    public Button goldPerSecUpgradeButton2;
    public Button goldPerSecUpgradeButton3;
    public Button goldPerSecUpgradeButton4;
    public Button incomeUpgradeButton;
    public Button incomeUpgradeButton1;
    public Button incomeUpgradeButton2;
    public Button incomeUpgradeButton3;
    public Button incomeUpgradeButton4;




    // fiyatlar
    private float gold = 0f;
    private float goldPerClick = 1f;
    private float goldPerSec = 0f;
    private float clickUpgradeCost = 10f;
    private float goldPerSecUpgradeCost = 20f;
    private float goldPerSecUpgradeCost1 = 20f;
    private float goldPerSecUpgradeCost2 = 20f;
    private float goldPerSecUpgradeCost3 = 20f;
    private float goldPerSecUpgradeCost4 = 20f;
    private float incomeMultiplier = 1f;
    private float incomeUpgradeCost = 50f;
    private float incomeUpgradeCost1 = 50f;
    private float incomeUpgradeCost2 = 50f;
    private float incomeUpgradeCost3 = 50f;
    private float incomeUpgradeCost4 = 50f;



    private void Start()
    {
        InvokeRepeating("IncreaseGoldPerSec", 0.0f, 1.0f);
        UpdateUI();
    }

    private void Update()
    {
        gold += goldPerSec * Time.deltaTime;
        UpdateUI();
    }

    // ekranda görünen sayılar
    private void UpdateUI()
    {
        // Scientific notation with 2 decimal places
        string goldScientific = gold.ToString("0.00E+00");

        // Engineering notation with 2 decimal places
        string goldEngineering = gold.ToString("0.00E+00").Replace("E+0", "E").Replace("E+", "E");

        // e notation with 2 decimal places
        string goldENotation = gold.ToString("0.00e+00");

        // Normalized notation with 2 decimal places
        string goldNormalized = gold.ToString("N2");

        goldText.text = "Gold: " + goldScientific + " " + goldEngineering + " " + goldENotation + " " + goldNormalized;
        goldPerSecText.text = "Gold Per Second: " + Mathf.Ceil(goldPerSec).ToString();
        clickPowerText.text = "Click Power: " + Mathf.Round(goldPerClick).ToString();
        clickUpgradeButton.GetComponentInChildren<Text>().text = "Buy Click Upgrade: " + Mathf.Ceil(clickUpgradeCost).ToString();
        goldPerSecUpgradeButton.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade: " + Mathf.Ceil(goldPerSecUpgradeCost).ToString();
        goldPerSecUpgradeButton1.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade1: " + Mathf.Ceil(goldPerSecUpgradeCost1).ToString();
        goldPerSecUpgradeButton2.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade2: " + Mathf.Ceil(goldPerSecUpgradeCost2).ToString();
        goldPerSecUpgradeButton3.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade3: " + Mathf.Ceil(goldPerSecUpgradeCost3).ToString();
        goldPerSecUpgradeButton4.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade4: " + Mathf.Ceil(goldPerSecUpgradeCost4).ToString();
        incomeUpgradeButton.GetComponentInChildren<Text>().text = "Upgrade Income: " + incomeUpgradeCost.ToString("F2");
    }


    private void IncreaseGoldPerSec()
    {
        gold += goldPerSec * incomeMultiplier;
    }

    // functionlar
    public void OnClick()
    {
        gold += goldPerClick;
        UpdateUI();
    }

    public void BuyClickUpgrade()
    {
        if (gold >= clickUpgradeCost)
        {
            gold -= clickUpgradeCost;
            goldPerClick *= 1.07f;
            clickUpgradeCost *= 1.1f;
            UpdateUI();
        }
    }

    public void BuyGoldPerSecUpgrade()
    {
        if (gold >= goldPerSecUpgradeCost)
        {
            gold -= goldPerSecUpgradeCost;
            goldPerSec += 1.15f;
            goldPerSecUpgradeCost *= 1.2f;
            UpdateUI();
        }
    }

    public void BuyGoldPerSecUpgrade1()
    {
        if (gold >= goldPerSecUpgradeCost1)
        {
            gold -= goldPerSecUpgradeCost1;
            goldPerSec += 1.15f;
            goldPerSecUpgradeCost1 *= 1.2f;
            UpdateUI();
        }
    }

    public void BuyGoldPerSecUpgrade2()
    {
        if (gold >= goldPerSecUpgradeCost2)
        {
            gold -= goldPerSecUpgradeCost2;
            goldPerSec += 1.15f;
            goldPerSecUpgradeCost2 *= 1.2f;
            UpdateUI();
        }
    }

    public void BuyGoldPerSecUpgrade3()
    {
        if (gold >= goldPerSecUpgradeCost3)
        {
            gold -= goldPerSecUpgradeCost3;
            goldPerSec += 1.15f;
            goldPerSecUpgradeCost3 *= 1.2f;
            UpdateUI();
        }
    }

    public void BuyGoldPerSecUpgrade4()
    {
        if (gold >= goldPerSecUpgradeCost4)
        {
            gold -= goldPerSecUpgradeCost4;
            goldPerSec += 1.15f;
            goldPerSecUpgradeCost4 *= 1.2f;
            UpdateUI();
        }
    }
    // p/sların multiplerları
    public void BuyIncomeUpgrade()
    {
        if (gold >= incomeUpgradeCost)
        {
            gold -= incomeUpgradeCost;
            incomeMultiplier += 1.15f;
            incomeUpgradeCost *= 1.2f;
            UpdateUI();
        }

        void UpgradeIncome()
        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }

    public void BuyIncomeUpgrade1()
    {
        if (gold >= incomeUpgradeCost1)
        {
            gold -= incomeUpgradeCost1;
            incomeMultiplier += 1.15f;
            incomeUpgradeCost *= 1.2f;
            UpdateUI();
        }

        void UpgradeIncome()
        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }

    public void BuyIncomeUpgrade2()
    {
        if (gold >= incomeUpgradeCost2)
        {
            gold -= incomeUpgradeCost2;
            incomeMultiplier += 1.15f;
            incomeUpgradeCost *= 1.2f;
            UpdateUI();
        }

        void UpgradeIncome()
        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }
    public void BuyIncomeUpgrade3()
    {
        if (gold >= incomeUpgradeCost3)
        {
            gold -= incomeUpgradeCost3;
            incomeMultiplier += 1.15f;
            incomeUpgradeCost *= 1.2f;
            UpdateUI();
        }

        void UpgradeIncome()
        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }
    public void BuyIncomeUpgrade4()
    {
        if (gold >= incomeUpgradeCost4)
        {
            gold -= incomeUpgradeCost4;
            incomeMultiplier += 1.15f;
            incomeUpgradeCost *= 1.2f;
            UpdateUI();
        }

        void UpgradeIncome()
        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }

}