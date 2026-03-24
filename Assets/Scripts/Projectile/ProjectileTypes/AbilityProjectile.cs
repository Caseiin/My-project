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
    private readonly HashSet<IEffectable> _buffer = new HashSet<IEffectable>();

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
        Gizmos.DrawWireSphere(transform.position, maxSpawnRadius);
    }

    List<IEffectable> FindEntitiesWithinRange()
    {
        _buffer.Clear();
        Collider[] _colliders = Physics.OverlapSphere(transform.position, maxSpawnRadius);

        foreach (var collider in _colliders)
        {
            IEffectable[] found = collider.GetComponents<IEffectable>();

            foreach (var effectable in found)
            {
                _buffer.Add(effectable);
            }
        }

        return new List<IEffectable>(_buffer);
    }
}
