using System;

public class TutorialEvent{
    public Action onRaised;
    public void Raise() => onRaised.Invoke();

}