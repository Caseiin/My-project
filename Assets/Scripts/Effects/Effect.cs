
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public Sprite EffectIcon;
    public Color EffectColour;
    public string Message{get; protected set;}
    public virtual float Duration{get; protected set;} = 0f;
    public abstract bool Apply(IEffectable target);
}
