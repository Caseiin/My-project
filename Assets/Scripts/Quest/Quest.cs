using UnityEditor;
using System;

[Serializable]
public class Quest{
    public GUID ID;
    public string Name;
    public QuestState State = QuestState.NotStarted;
}

public enum QuestState{NotStarted, InProgress, Completed, Failed}