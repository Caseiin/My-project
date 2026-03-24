using System.Collections.Generic;
using UnityEditor.Callbacks;
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

    // Prevents duplication of effects stored;
    HashSet<IEffectable> _playerBuffer = new HashSet<IEffectable>();
    HashSet<IEffectable> _otherBuffer = new HashSet<IEffectable>();

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void Launch(Vector3 direction);
    protected void Activate()
    {
        FindEntitiesWithinRange(out var _playerEffectables,out var _otherEffectables);
        // Applies all effects to all effectable entities

        // player
        foreach (var effectable in _playerEffectables)
        {
            foreach(var effect in ability.effects)
            {
                effect.Apply(effectable);
                EffectPopUpManager.Instance.DisplayEffect(effect);
            }
        }

        // other entities
        foreach (var effectable in _otherEffectables)
        {
            foreach(var effect in ability.effects)
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

    void FindEntitiesWithinRange(out List<IEffectable> playerList, out List<IEffectable> othersList)
    {
        _playerBuffer.Clear();
        _otherBuffer.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, MaxEffectRadius);

        foreach (var col in colliders)
        {
            IEffectable[] found = col.GetComponents<IEffectable>();
            foreach (var effectable in found)
            {
    
                if (effectable is IPlayerEffectable)
                {
                    _playerBuffer.Add(effectable);
                }
                else
                {
                    _otherBuffer.Add(effectable);
                }
            }
        }

        playerList = new List<IEffectable>(_playerBuffer);
        othersList = new List<IEffectable>(_otherBuffer);
    }
}
