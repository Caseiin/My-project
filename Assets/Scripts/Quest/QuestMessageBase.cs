using System;
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
    //TODO: Implement

    readonly Action <TMessage, Quest> _onProcess;
    readonly Func<TMessage, Quest, bool> _condition;

    public GenericQuestProcessor(Action<TMessage,Quest> onProcess, Func<TMessage,Quest,bool> condition = null){
        _onProcess = onProcess;
        _condition = condition;
    }

    public override void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests)
    {
        if(message is TMessage typedMessage && quests.TryGetValue(typedMessage.QuestID, out Quest quest) && (_condition == null || _condition(typedMessage,quest))){
            Debug.Log($"{GetType().Name}: Processing {typeof(TMessage).Name}");
            _onProcess(typedMessage, quest);
            return;
        }

        base.Process(message, quests);
    }
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

    // TODO: make use of the genericprocessor 
    Dictionary<GUID, Quest> quests = new Dictionary<GUID, Quest>();
    IQuestProcessor chain;

    void Awake(){
        chain = new StartQuestProcessor();
        chain.SetNext(new CompleteQuestProcessor())
            .SetNext(new FailQuestProcessor());
    }

    public void RegisterQuest(Quest quest) => quests.Add(quest.ID, quest);
    public void UpdateQuest(QuestMessageBase message) => chain.Process(message, quests);
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