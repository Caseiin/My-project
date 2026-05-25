using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public interface IQuestProcessor
{
    IQuestProcessor SetNext(IQuestProcessor processor);
    void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests);
}

public abstract class QuestProcessorBase : IQuestProcessor
{
    IQuestProcessor next;
    public IQuestProcessor SetNext(IQuestProcessor processor) => next = processor;
    public virtual void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests) => next?.Process(message,quests);
}

public class GenericQuestProcessor<TMessage> : QuestProcessorBase where TMessage : QuestMessageBase
{
    // Implement
}

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


public class QuestManager: MonoBehaviour{
    Dictionary<GUID, Quest> quests = new Dictionary<GUID, Quest>();
    IQuestProcessor chain;

    void Awake(){
        chain = new StartQuestProcessor();
        chain.SetNext(new CompleteQuestProcessor())
            .SetNext(new FailQuestProcessor());
    }

    public void RegisterQuest(Quest quest) => quests.Add(quest.ID, quest);

    // 
}

public abstract class QuestMessageBase{
    public GUID QuestID;
}

public class StartQuestMessage: QuestMessageBase{}
public class CompleteQuestMessage: QuestMessageBase{}
public class FailQuestMessage: QuestMessageBase{}


[System.Serializable]
public class Quest{
    public GUID ID;
    public string Name;
    public QuestState State = QuestState.NotStarted;
}

public enum QuestState{NotStarted, InProgress, Completed, Failed}