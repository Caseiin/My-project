using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MotionlessEffect : Effect
{
    [SerializeField] float motionlessDuration = 2.5f;

    public override void Apply(IEffectable target)
    {
        var _target = target as IMoveable;
        if (_target != null)
        {
            var _runner = _target as MonoBehaviour;
            _runner.StartCoroutine(SlowedCouroutine(_target));
        }
    }

    IEnumerator SlowedCouroutine(IMoveable target)
    {
        target.IsMovementBlocked = true;
        yield return new WaitForSecondsRealtime(motionlessDuration);
        target.IsMovementBlocked = false;
    }
}
