using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    public float speed;
    public float MaxEffectRadius = 5f;
    public AbilitySO ability;
    protected Rigidbody _rb;
    List<IEffectable> _playerEffectables;
    List<IEffectable> _otherEffectables;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void Launch(Vector3 direction);

    protected void Activate()
    {
        FindEffectablesWithinRange(out var _playerEffectables, out var _otherEffectables);

        // player
        foreach (var effectable in _playerEffectables)
        {
            foreach (var effect in ability.effects)
            {
                if (effect.Apply(effectable))
                {
                    EffectPopUpManager.Instance.DisplayEffect(effect);
                }
            }
        }

        // other
        foreach (var effectable in _otherEffectables)
        {
            foreach (var effect in ability.effects)
            {
                effect.Apply(effectable);
            }
        }


        ReturnToPool();
    }

    public virtual void ReturnToPool()
    {
        _rb.linearVelocity = Vector3.zero;
        ProjectileManager.Instance.ReturnProjectile(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MaxEffectRadius);
    }

    /// <summary>
    /// Finds all effectables in range. 
    /// Filtering by type and faction happens inside each effect.
    /// </summary>
    void FindEffectablesWithinRange(out List<IEffectable> playerList, out List<IEffectable> otherList)
    {
        var playerBuffer = new HashSet<IEffectable>();
        var otherBuffer = new HashSet<IEffectable>();

        // Track root GameObjects already processed to avoid double-hitting
        var seen = new HashSet<GameObject>();

        var colliders = Physics.OverlapSphere(transform.position, MaxEffectRadius);

        foreach (var col in colliders)
        {
            // Use the root GameObject as the unique key
            var root = col.transform.root.gameObject;
            if (!seen.Add(root)) continue; // already processed this enemy

            var effectables = root.GetComponents<IEffectable>();
            foreach (var e in effectables)
            {
                if (e is IPlayerEffectable)
                    playerBuffer.Add(e);
                else
                    otherBuffer.Add(e);
            }
        }

        playerList = new List<IEffectable>(playerBuffer);
        otherList = new List<IEffectable>(otherBuffer);
    }
}