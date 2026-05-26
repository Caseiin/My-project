using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class QuestManager: MonoBehaviour{

    // TODO: make use of the genericprocessor 
    Dictionary<GUID, Quest> quests = new Dictionary<GUID, Quest>();
    IQuestProcessor chain;

    void Awake(){
                // Build each processor with its condition + action
        var startProcessor = new GenericQuestProcessor<StartQuestMessage>(
            condition: (msg, quest) => quest.State == QuestState.NotStarted,
            onProcess: (msg, quest) => {
                quest.State = QuestState.InProgress;
                Debug.Log($"Quest '{quest.Name}' started.");
            }
        );

        var completeProcessor = new GenericQuestProcessor<CompleteQuestMessage>(
            condition: (msg, quest) => quest.State == QuestState.InProgress,
            onProcess: (msg, quest) => {
                quest.State = QuestState.Completed;
                Debug.Log($"Quest '{quest.Name}' completed.");
            }
        );

        var failProcessor = new GenericQuestProcessor<FailQuestMessage>(
            condition: (msg, quest) => quest.State == QuestState.InProgress,
            onProcess: (msg, quest) => {
                quest.State = QuestState.Failed;
                Debug.Log($"Quest '{quest.Name}' failed.");
            }
        );

        // Wire the chain
        chain = startProcessor;
        startProcessor.SetNext(completeProcessor)
                        .SetNext(failProcessor);

        // chain = new StartQuestProcessor();
        // chain.SetNext(new CompleteQuestProcessor())
        //     .SetNext(new FailQuestProcessor());
    }

    public void RegisterQuest(Quest quest) => quests.Add(quest.ID, quest);
    public void UpdateQuest(QuestMessageBase message) => chain.Process(message, quests);
}