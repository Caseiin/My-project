using UnityEngine;

public abstract class GenEffect<T> : Effect where T : class
{

    public override bool Apply(IEffectable target)
    {
        if (target is not T validTarget)
        {
            return false;
        }

        ApplyEffect(validTarget);
        return true;
    }


    protected abstract void ApplyEffect(T target);
}
