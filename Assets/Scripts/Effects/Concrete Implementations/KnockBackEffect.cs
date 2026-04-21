using UnityEngine;

[System.Serializable]
public class KnockBackEffect : GenEffect<IMoveable>
{

    [SerializeField] float knockbackForce = 4f;
    protected override void ApplyEffect(IMoveable target, EffectContext context)
    {
        if (context.SourcePosition == null) return;

        var _direction = (target.Transform.position - context.SourcePosition.position).normalized;
        target.RB.AddForce(_direction* knockbackForce, ForceMode.Impulse);
    }
}
