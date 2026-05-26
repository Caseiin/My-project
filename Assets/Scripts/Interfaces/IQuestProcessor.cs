using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IQuestProcessor
{
    IQuestProcessor SetNext(IQuestProcessor processor);
    void Process(QuestMessageBase message, Dictionary<GUID, Quest> quests);
}