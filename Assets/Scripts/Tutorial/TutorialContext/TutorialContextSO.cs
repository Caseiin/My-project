using UnityEngine;
using System;

[CreateAssetMenu(fileName ="TutorialContext", menuName ="Tutorial/context")]
public  class TutorialContextSO: ScriptableObject{
    public string TutorialName;
    public int failure;
    public string Hints;
}
