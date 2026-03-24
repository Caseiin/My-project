using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    public float speed;
    public float maxSpawnRadius;
    public AbilitySO ability;
    protected Rigidbody _rb;

    protected virtual void awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void Launch(Vector3 direction);
    public void Activate()
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
        Gizmos.DrawWireSphere(transform.position, maxSpawnRadius);
    }

    List<IEffectable> FindEntitiesWithinRange()
    {
        List<IEffectable> _effectables = new List<IEffectable>();
        Collider[] _colliders = Physics.OverlapSphere(transform.position, maxSpawnRadius);

        foreach(var collider in _colliders)
        {
            IEffectable[] found = collider.GetComponents<IEffectable>();
            _effectables.AddRange(found);
        }

        return _effectables;
    }
}
