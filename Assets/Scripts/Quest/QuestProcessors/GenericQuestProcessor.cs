using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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