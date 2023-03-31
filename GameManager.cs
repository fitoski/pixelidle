using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NotationSystem
{
    Default,
    Scientific,
    Engineering,
    ENotation,
    Normalized
}

[Serializable]
public class GameData
{
    public double gold;
    public double goldPerClick;
    public double goldPerSec;
    public double clickUpgradeCost;
    public double goldPerSecUpgradeCost;
    public double goldPerSecUpgradeCost1;
    public double goldPerSecUpgradeCost2;
    public double goldPerSecUpgradeCost3;
    public double goldPerSecUpgradeCost4;
    public double incomeMultiplier;
    public double incomeUpgradeCost;
    public double incomeUpgradeCost1;
    public double incomeUpgradeCost2;
    public double incomeUpgradeCost3;
    public double incomeUpgradeCost4;
}

public class GameManager : MonoBehaviour
{
    private NotationSystem notation = NotationSystem.Default;
    [SerializeField]
    private Text goldText;

    // buttonlar
    public Text goldPerSecText;
    public Text clickPowerText;
    public int totalclickPower;
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

    // dropdown
    public Dropdown notationDropdown;

    // fiyatlar
    private double gold = 0d;
    private double goldPerClick = 1d;
    private double goldPerSec = 0d;
    private double clickUpgradeCost = 10d;
    private double goldPerSecUpgradeCost = 20d;
    private double goldPerSecUpgradeCost1 = 20d;
    private double goldPerSecUpgradeCost2 = 20d;
    private double goldPerSecUpgradeCost3 = 20d;
    private double goldPerSecUpgradeCost4 = 20d;
    private double incomeMultiplier = 1d;
    private double incomeUpgradeCost = 50d;
    private double incomeUpgradeCost1 = 50d;
    private double incomeUpgradeCost2 = 50d;
    private double incomeUpgradeCost3 = 50d;
    private double incomeUpgradeCost4 = 50d;



    private void SaveGame()
    {
        GameData gameData = new GameData
        {
            gold = this.gold,
            goldPerClick = this.goldPerClick,
            goldPerSec = this.goldPerSec,
            clickUpgradeCost = this.clickUpgradeCost,
            goldPerSecUpgradeCost = this.goldPerSecUpgradeCost,
            goldPerSecUpgradeCost1 = this.goldPerSecUpgradeCost1,
            goldPerSecUpgradeCost2 = this.goldPerSecUpgradeCost2,
            goldPerSecUpgradeCost3 = this.goldPerSecUpgradeCost3,
            goldPerSecUpgradeCost4 = this.goldPerSecUpgradeCost4,
            incomeMultiplier = this.incomeMultiplier,
            incomeUpgradeCost = this.incomeUpgradeCost,
            incomeUpgradeCost1 = this.incomeUpgradeCost1,
            incomeUpgradeCost2 = this.incomeUpgradeCost2,
            incomeUpgradeCost3 = this.incomeUpgradeCost3,
            incomeUpgradeCost4 = this.incomeUpgradeCost4,
        };

        PlayerPrefs.SetString("GameData", JsonUtility.ToJson(gameData));
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            GameData gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("GameData"));

            gold = gameData.gold;
            goldPerClick = gameData.goldPerClick;
            goldPerSec = gameData.goldPerSec;
            clickUpgradeCost = gameData.clickUpgradeCost;
            goldPerSecUpgradeCost = gameData.goldPerSecUpgradeCost;
            goldPerSecUpgradeCost1 = gameData.goldPerSecUpgradeCost1;
            goldPerSecUpgradeCost2 = gameData.goldPerSecUpgradeCost2;
            goldPerSecUpgradeCost3 = gameData.goldPerSecUpgradeCost3;
            goldPerSecUpgradeCost4 = gameData.goldPerSecUpgradeCost4;
            incomeMultiplier = gameData.incomeMultiplier;
            incomeUpgradeCost = gameData.incomeUpgradeCost;
            incomeUpgradeCost1 = gameData.incomeUpgradeCost1;
            incomeUpgradeCost2 = gameData.incomeUpgradeCost2;
            incomeUpgradeCost3 = gameData.incomeUpgradeCost3;
            incomeUpgradeCost4 = gameData.incomeUpgradeCost4;
        }
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveGame();
        }
    }

    private void Start()
    {
        LoadGame();

        InvokeRepeating("IncreaseGoldPerSec", 0.0f, 1.0f);

        // Add an event listener to the dropdown
        notationDropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(notationDropdown);
        });
    }


    public void OnDropdownValueChanged(Dropdown change)
    {
        notation = (NotationSystem)change.value;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    // ekranda görünen sayılar
    private void UpdateUI()
    {
        goldText.text = gold.ToString(notation);

        if (notation == NotationSystem.Default) // Default Notation
        {
            goldText.text = $"Gold: {gold.ToString("N0")}";
        }
        else if (notation == NotationSystem.Scientific) // Scientific Notation
        {
            goldText.text = $"Gold: {gold.ToString("0.###E+0")}";
        }
        else if (notation == NotationSystem.Engineering) // Engineering Notation
        {
            double exponent = Math.Floor(Math.Log10(Math.Abs(gold)) / 3);
            double mantissa = gold / Math.Pow(10, exponent * 3);
            goldText.text = $"Gold: {mantissa:0.###}E{(exponent * 3):+#;-#;+0}";
        }
        else if (notation == NotationSystem.ENotation) // E Notation
        {
            goldText.text = $"Gold: {gold.ToString("N3")}";
        }
        else if (notation == NotationSystem.Normalized) // Normalized
        {
            goldText.text = $"Gold: {gold.ToString("0.###E+0")}";
        }



        goldPerSecText.text = "Gold Per Second: " + Mathf.Ceil((float)goldPerSec).ToString();
        clickPowerText.text = "Click Power: " + Mathf.Floor((float)totalclickPower).ToString();
        clickUpgradeButton.GetComponentInChildren<Text>().text = "Buy Click Upgrade: " + Mathf.Ceil((float)clickUpgradeCost).ToString();
        goldPerSecUpgradeButton.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade: " + Mathf.Ceil((float)goldPerSecUpgradeCost).ToString();
        goldPerSecUpgradeButton1.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade1: " + Mathf.Ceil((float)goldPerSecUpgradeCost1).ToString();
        goldPerSecUpgradeButton2.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade2: " + Mathf.Ceil((float)goldPerSecUpgradeCost2).ToString();
        goldPerSecUpgradeButton3.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade3: " + Mathf.Ceil((float)goldPerSecUpgradeCost3).ToString();
        goldPerSecUpgradeButton4.GetComponentInChildren<Text>().text = "Buy Gold Per Sec Upgrade4: " + Mathf.Ceil((float)goldPerSecUpgradeCost4).ToString();
        incomeUpgradeButton.GetComponentInChildren<Text>().text = "Buy Income Upgrade: " + Mathf.Ceil((float)incomeUpgradeCost).ToString();
        incomeUpgradeButton1.GetComponentInChildren<Text>().text = "Buy Income Upgrade1: " + Mathf.Ceil((float)incomeUpgradeCost1).ToString();
        incomeUpgradeButton2.GetComponentInChildren<Text>().text = "Buy Income Upgrade2: " + Mathf.Ceil((float)incomeUpgradeCost2).ToString();
        incomeUpgradeButton3.GetComponentInChildren<Text>().text = "Buy Income Upgrade3: " + Mathf.Ceil((float)incomeUpgradeCost3).ToString();
        incomeUpgradeButton4.GetComponentInChildren<Text>().text = "Buy Income Upgrade4: " + Mathf.Ceil((float)incomeUpgradeCost4).ToString();
      

    }

    public void DisplayTotalGold()
    {
        double gold = this.gold;
        Debug.Log("Total Gold: " + gold);
    }


    private void IncreaseGoldPerSec()
    {
        gold += goldPerSec;
        UpdateUI();
    }

    // functionlar
    public void OnClick()
    {
        gold += goldPerClick;
        UpdateUI();
        DisplayTotalGold();
    }

    public void BuyClickUpgrade()
    {
        if (gold >= clickUpgradeCost)
        {
            totalclickPower++;
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

        {
            goldPerSec += 0.5f;
            UpdateUI();
        }
    }
}
