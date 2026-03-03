using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(IEffectable target);
}
