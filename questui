using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject mainPanel;
    public Text[] questTexts;

    private void Start()
    {
        questTexts = mainPanel.GetComponentsInChildren<Text>();
    }

    void Update()
    {
        if (questManager.quests.Count > 0 && questManager.activeQuests < questManager.maxActiveQuests)
        {
            for (int i = 0; i < questManager.quests.Count; i++)
            {
                if (questManager.quests[i].isActive)
                {
                    questTexts[i].text = questManager.quests[i].description;
                }
            }
        }
    }
}
