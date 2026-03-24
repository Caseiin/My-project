using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    public float speed;
    public float MaxEffectRadius;
    public AbilitySO ability;
    protected Rigidbody _rb;
    List<IEffectable> _playerEffectables = new List<IEffectable>();
    List<IEffectable> _otherEffectables = new List<IEffectable>();

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void Launch(Vector3 direction);
    protected void Activate()
    {
        List<IEffectable> effectables = FindEntitiesWithinRange();
        // Applies all effects to all effectable entities
        foreach (var effectable in effectables)
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
        _playerEffectables.Clear();
        _otherEffectables.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, MaxEffectRadius);

        foreach (var col in colliders)
        {
            IEffectable[] found = col.GetComponents<IEffectable>();
            foreach (var effectable in found)
            {
    
                // if ()
                // {
                //     _playerEffectables.Add(effectable);
                // }
                // else
                // {
                //     _otherEffectables.Add(effectable);
                // }
            }
        }

        playerList = _playerEffectables;
        othersList = _otherEffectables;
    }
}
