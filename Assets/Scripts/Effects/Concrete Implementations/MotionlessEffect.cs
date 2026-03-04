using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MotionlessEffect : Effect
{
    [Range(0.1f,0.9f)]
    [SerializeField] float slowCoefficient = 0.5f;
    [SerializeField] float slowDuration = 2.5f;

    public override void Apply(IEffectable target)
    {
        var _target = target as IMoveable;
        if (_target != null)
        {
            var _runner = _target as MonoBehaviour;
            _runner.StartCoroutine(SlowedCouroutine(_target));
            Debug.Log("Target has stopped");
        }
    }

    IEnumerator SlowedCouroutine(IMoveable target)
    {
        target.IsMovementBlocked = true;
        yield return new WaitForSecondsRealtime(slowDuration);
        target.IsMovementBlocked = false;
    }
}
