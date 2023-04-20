using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;



public class gameManager : MonoBehaviour
{
    public Image progressBarBackground;
    public Image progressBarFill;
    private float progressTime = 0f;
    private float progressDuration = 5f;
    public float unit1FillTime = 5f;
    private bool isFillingProgressBar = false;
    public GameObject offlineEarningsPanel;
    public Text offlineEarningsText;

    // Part 1 info
    public Text coinsText;

    // public Text coinsPerSecText;

    public double coins;

    // public double coinsPerSecond;

    // Part 2   units



    public Text unit1Text;
    public bool isAuto1 = false;
    public bool isAuto2 = false;
    //public Text unit1CoinsPerSecondText;

    public double unit1CostStart;
    public double unit1Cost;

    public double unit1Count;

    //public double unit1CoinsPerSecond;

    public double unit1CostMultply;

    public double unit1PowerPU;



    public Text unit2Text;

    //public Text unit2CoinsPerSecondText;

    public double unit2CostStart;
    public double unit2Cost;

    public double unit2Count;

    //public double unit2CoinsPerSecond;

    public double unit2CostMultply;

    public double unit2PowerPU;

    //Part 3

    public double Unit1Power()
    {
        return unit1Count * unit1PowerPU;
    }
    public double Unit2Power()
    {
        return unit2Count * unit2PowerPU;
    }
    public double Unit1Cost()
    {
        return unit1CostStart * Math.Pow(unit1CostMultply, unit1Count);
    }
    public double Unit2Cost()
    {
        return unit2CostStart * Math.Pow(unit2CostMultply, unit2Count);
    }

    // Start is called before the first frame update
    public void Start()
    {
        Application.targetFrameRate = 60;
        Load();
        progressBarFill.fillAmount = 0f;
        CalculateOfflineEarnings();
    }

    public void OnApplicationQuit()
    {
        Save();
        PlayerPrefs.SetString("lastQuitTime", DateTime.UtcNow.ToString()); 
    }

    public void Load()
    {
        unit1CostStart = 25;
        unit1PowerPU = 1;
        unit1CostMultply = 1.1;
        unit2CostStart = 100;
        unit2PowerPU = 5;
        unit2CostMultply = 1.12;
        coins = double.Parse(PlayerPrefs.GetString("coins", "0"));
        //coinsPerSecond = double.Parse(PlayerPrefs.GetString("coinsPerSecond", "0"));
        unit1Count = double.Parse(PlayerPrefs.GetString("unit1Count", "1"));
        //unit1CoinsPerSecond = double.Parse(PlayerPrefs.GetString("unit1CoinsPerSecond", "0"));
        unit2Count = double.Parse(PlayerPrefs.GetString("unit2Count", "0"));
        //unit2CoinsPerSecond = double.Parse(PlayerPrefs.GetString("unit2CoinsPerSecond", "0"));
        isAuto1 = bool.Parse(PlayerPrefs.GetString("isAuto1"));
        isAuto2 = bool.Parse(PlayerPrefs.GetString("isAuto2"));

        if (PlayerPrefs.HasKey("lastQuitTime"))
        {
            DateTime lastQuitTime = DateTime.Parse(PlayerPrefs.GetString("lastQuitTime"));
            TimeSpan timePassed = DateTime.UtcNow - lastQuitTime;
            PlayerPrefs.SetString("timePassed", timePassed.ToString());
        }

    }
    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("unit1Count", unit1Count.ToString());
        PlayerPrefs.SetString("unit2Count", unit2Count.ToString());
        PlayerPrefs.SetString("isAuto1", isAuto1.ToString());
        PlayerPrefs.SetString("isAuto2", isAuto2.ToString());

    }

    // Update is called once per frame
    public void Update()
    {
        // coinsPerSecond = unit1Level;
        coinsText.text = "Coins: " + coins.ToString("F0");
        //coinsPerSecText.text = coinsPerSecond.ToString("F0") + " coins/s";
        unit1Text.text = "Unit Count: " + unit1Count.ToString("F0") + "\nNext Price: " + Unit1Cost().ToString("F0") + "coins\nClick Power: " + Unit1Power().ToString("F0") + "coins\nPower Per Unite: " + unit1PowerPU.ToString("F0");
        unit2Text.text = "Unit Count: " + unit2Count.ToString("F0") + "\nNext Price: " + Unit2Cost().ToString("F0") + "coins\nClick Power: " + Unit2Power().ToString("F0") + "coins\nPower Per Unite: " + unit2PowerPU.ToString("F0");
        Save();
        if (isAuto1 == true && !isFillingProgressBar)
        {
            unit1Click();
        }
    }

    public void autoToggle1()
    {
        isAuto1 = !isAuto1;
    }
    public void autoToggle2()
    {
        isAuto2 = !isAuto2;
    }

    public void unit1Buy()
    {
        if (coins >= Unit1Cost())
        {
            coins -= Unit1Cost();
            unit1Count++;
        }
    }

    public void unit1Click()
    {
        if (!isFillingProgressBar)
        {
            isFillingProgressBar = true;
            StartCoroutine(FillProgressBar(progressBarFill, unit1FillTime));
        }
    }

    public void unit2Buy()
    {
        if (coins >= Unit2Cost())
        {
            coins -= Unit2Cost();
            unit2Count++;

        }
    }
    public void unit2Click()
    {
        coins += Unit2Power();
    }

    private IEnumerator FillProgressBar(Image progressBar, float fillTime)
    {
        float fillAmount = 0f;

        while (fillAmount < 1f)
        {
            fillAmount += Time.deltaTime / fillTime;
            progressBar.fillAmount = fillAmount;
            yield return null;
        }

        coins += Unit1Power();
        isFillingProgressBar = false;
    }

    private void CalculateOfflineEarnings()
    {
        if (PlayerPrefs.HasKey("timePassed"))
        {
            TimeSpan timePassed = TimeSpan.Parse(PlayerPrefs.GetString("timePassed"));
            double autoEarnings = 0;

            if (isAuto1)
            {
                autoEarnings += Unit1Power() * timePassed.TotalSeconds;
            }
            if (isAuto2)
            {
                autoEarnings += Unit2Power() * timePassed.TotalSeconds;
            }

            coins += autoEarnings;

            ShowOfflineEarnings(autoEarnings);
        }
    }
    private void ShowOfflineEarnings(double earnings)
    {
        offlineEarningsPanel.SetActive(true);
        offlineEarningsText.text = "Offline Earnings: " + earnings.ToString("F0") + " coins";
    }

    public void CloseOfflineEarningsPanel() 
    {
        offlineEarningsPanel.SetActive(false);
    }

}
