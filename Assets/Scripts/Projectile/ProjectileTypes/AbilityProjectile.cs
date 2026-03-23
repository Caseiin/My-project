using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    public float speed;
    public AbilitySO ability;
    protected Rigidbody _rb;

    protected virtual void awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public abstract void Launch(Vector3 direction);
    public abstract void Activate();
}
