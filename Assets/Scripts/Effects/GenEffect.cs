using UnityEngine;

public abstract class GenEffect<T> : Effect where T : class
{

    public override bool Apply(IEffectable target, EffectContext context = null)
    {
        if (target is not T validTarget)
        {
            return false;
        }

        ApplyEffect(validTarget, context);
        return true;
    }


    protected abstract void ApplyEffect(T target, EffectContext context);
}
