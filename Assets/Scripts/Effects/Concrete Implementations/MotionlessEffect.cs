using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MotionlessEffect : GenEffect<IMoveable>
{
    [SerializeField] float motionlessDuration = 2.5f;
    public override float Duration => motionlessDuration;

    protected override void ApplyEffect(IMoveable target)
    {
        if (target != null)
        {
            Message = $"{this.GetType()}: cannot move for {motionlessDuration} seconds";
            var _runner = target as MonoBehaviour;
            _runner.StartCoroutine(SlowedCouroutine(target));
        }
    }

    IEnumerator SlowedCouroutine(IMoveable target)
    {
        target.IsMovementBlocked = true;
        yield return new WaitForSecondsRealtime(motionlessDuration);
        target.IsMovementBlocked = false;
    }
}
