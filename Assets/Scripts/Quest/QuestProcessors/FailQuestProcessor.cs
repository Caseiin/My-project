using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class FailQuestProcessor: QuestProcessorBase{
    public override void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests)
    {
        Debug.Log($"{GetType().Name}: Processing message of type {message.GetType().Name}");

        if(message is FailQuestMessage failMessage && quests.TryGetValue(failMessage.QuestID, out var quest)){
            if(quest.State == QuestState.InProgress){
                quest.State = QuestState.Failed;
                Debug.Log($"Quest {quest.Name} failed");
            }
            return;
        }

        base.Process(message, quests);
    }
}