using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

public abstract class QuestProcessorBase : IQuestProcessor
{
    IQuestProcessor next;
    public IQuestProcessor SetNext(IQuestProcessor processor) => next = processor;
    public virtual void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests) => next?.Process(message,quests);
}