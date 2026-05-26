using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartQuestProcessor: QuestProcessorBase{
    public override void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests)
    {
        Debug.Log($"{GetType().Name}: Processing message of type {message.GetType().Name}");

        if(message is StartQuestMessage startMessage && quests.TryGetValue(startMessage.QuestID, out var quest)){
            if(quest.State == QuestState.NotStarted){
                quest.State = QuestState.InProgress;
                Debug.Log($"Quest {quest.Name} started");
            }
            return;
        }

        base.Process(message, quests);
    }
}