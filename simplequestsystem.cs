using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest
{
    public string id;
    public string description;
    public Func<bool> IsCompleted;
    public Action Complete;
    private bool rewardReadyToClaim = false;

    public Quest(string id, string description, Func<bool> IsCompleted, Action Complete)
    {
        this.id = id;
        this.description = description;
        this.IsCompleted = IsCompleted;
        this.Complete = Complete;
    }
}

public class SimpleQuestSystem : MonoBehaviour
{
    public gameManager gameManagerInstance;
    public Text questDescriptionText;
    public Text questStatusText;

    private List<Quest> quests = new List<Quest>();
    private Quest currentQuest;

    private void Start()
    {
        AddNewQuest();
        UpdateQuests();
    }

    private void Update()
    {
        if (currentQuest != null && currentQuest.IsCompleted())
        {
            currentQuest.Complete();
            currentQuest = null;
            questStatusText.text = "Görev tamamlandı!";
        }
    }

    private void AddNewQuest()
    {
        float requiredPlayTime = 60f;
        float rewardGold = 100f;

        Quest playTimeQuest = new Quest(
            "play-time",
            "1 dakika boyunca oyna",
            () => Time.time >= requiredPlayTime,
            () => {
                gameManagerInstance.AddCoins((int)rewardGold);
                Debug.Log("Ödül olarak " + rewardGold + " altın kazandınız!");
            }
        );

        quests.Add(playTimeQuest);
    }

    private void UpdateQuests()
    {
        if (quests.Count > 0 && currentQuest == null)
        {
            currentQuest = quests[0];
            quests.RemoveAt(0);

            questDescriptionText.text = currentQuest.description;
            questStatusText.text = "Yeni görev başladı!";
        }
    }

    private void CompleteQuest()
    {
        questCompleted = true;
        rewardReadyToClaim = true;
        questStatusText.text = "Ödülünü almak için tıkla";
    }

    public void ClaimReward()
    {
        if (rewardReadyToClaim)
        {
            gameManagerInstance.AddCoins(rewardAmount);
            rewardReadyToClaim = false;
            StartCoroutine(StartNewQuestWithDelay(1800f)); 
        }
    }

}
