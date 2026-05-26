using UnityEditor;
using UnityEngine;

public class QuestDemo : MonoBehaviour
{
    [SerializeField] QuestManager questManager;

    void Start(){
        GUID questID = new();
        questManager.RegisterQuest(new Quest{ ID = questID, Name = "Find the treasure"});

        questManager.UpdateQuest(new StartQuestMessage{QuestID = questID});
        questManager.UpdateQuest(new CompleteQuestMessage{QuestID = questID});
        questManager.UpdateQuest(new FailQuestMessage{QuestID = questID});

    }
}
