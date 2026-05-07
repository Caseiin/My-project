using UnityEngine;
using System;

[CreateAssetMenu(fileName ="TutorialContext", menuName ="Tutorial/context")]
public  class TutorialContextSO: ScriptableObject{
    public string TutorialName;
    public int failure;
    public float inactivityTimeout = 1f;
    public string Hints;
}
