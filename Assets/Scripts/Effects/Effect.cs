using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public string Message{get; protected set;}
    public abstract void Apply(IEffectable target);
}
