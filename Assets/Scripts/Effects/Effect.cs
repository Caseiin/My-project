
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public Sprite EffectIcon;
    public string Message{get; protected set;}
    public  IEffectIconDisplay EffectDisplay = null;
    public abstract void Apply(IEffectable target);
}
