using UnityEngine;
using System;

public abstract class TutorialContextSO: ScriptableObject{
    public string TutorialName;
    public int failure;
    public float inactivityTimeout = 1f;
    public string Hints;
    public abstract TutorialData Build(PlayerController player);

    void OnEnable()
    {
        if(String.IsNullOrEmpty(TutorialName))
            TutorialName = name;
    }

    protected PressurePhasePolicy BuildPressurePolicy() =>
        new PressurePhasePolicy(
            threshold: failure,
            inactiveTime: inactivityTimeout,
            showHint: ()=> TutorialManager.Instance.ShowHint(Hints),
            hindHint: ()=> TutorialManager.Instance.HideHint()
        );
}
