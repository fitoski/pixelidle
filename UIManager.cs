using UnityEngine;

public class UIManager: MonoBehaviour
{
    public GameObject cardsPanel;
    public GameObject achievementsPanel;
    public GameObject mainPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        cardsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
    }


    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        cardsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
    }

    public void ShowCardsPanel()
    {
        mainPanel.SetActive(false);
        cardsPanel.SetActive(true);
        achievementsPanel.SetActive(false);
    }

    public void ShowAchievementsPanel()
    {
        mainPanel.SetActive(false);
        cardsPanel.SetActive(false);
        achievementsPanel.SetActive(true);
    }
}
