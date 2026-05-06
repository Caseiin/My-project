using System;
using UnityEngine;

[Serializable]
public class TutorialData
{
    public string TutorialName {get;} = string.Empty;
    Action<Action> onSubscribe;
    Action<Action> unSubscribe;
    Action onTutorialStart;
    Action onTutorialEnd;
    Action onTutorialDone;
    TutorialData(string name){
        TutorialName = name;
    }

    public void StartTutorial() => onTutorialStart?.Invoke();
    public void Bind(Action onNextStep)
    {
        onTutorialDone = () =>
        {
            onTutorialEnd?.Invoke();
            onNextStep?.Invoke();
            Unbind();
        };
        
        onSubscribe?.Invoke(onTutorialDone);
    }

    void Unbind()=> unSubscribe?.Invoke(onTutorialDone);

    public class Builder
    {
        readonly TutorialData tutorialData;

        public Builder(string name)
        {
            tutorialData = new TutorialData(name);
        }

        public Builder WithCompletionCondition(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            tutorialData.onSubscribe = subscribe;
            tutorialData.unSubscribe = unsubscribe;
            return this;
        }

        public Builder WithInitialCondition(Action intial)
        {
            tutorialData.onTutorialStart = intial;
            return this;
        }

        public Builder WithEndCondition(Action end)
        {
            tutorialData.onTutorialEnd = end;
            return this;
        }

        public TutorialData Build() 
        {
            return tutorialData;
        }
    }

}
