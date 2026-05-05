using System;
using UnityEngine;

[Serializable]
public class TutorialData
{
    public string TutorialName {get;}
    public TutorialEvent CompletionCondition{get;}
    public Action onTutorialStart;
    public Action onTutorialEnd;

    public TutorialData(string name, TutorialEvent condition){
        TutorialName = name;
        CompletionCondition = condition;
    }
}
