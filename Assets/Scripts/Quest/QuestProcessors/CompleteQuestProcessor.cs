using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


public class CompleteQuestProcessor: QuestProcessorBase{
    public override void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests)
    {
        Debug.Log($"{GetType().Name}: Processing message of type {message.GetType().Name}");

        if(message is CompleteQuestMessage completeMessage && quests.TryGetValue(completeMessage.QuestID, out var quest)){
            if(quest.State == QuestState.InProgress){
                quest.State = QuestState.Completed;
                Debug.Log($"Quest {quest.Name} Completed");
            }
            return;
        }

        base.Process(message, quests);
    }
}