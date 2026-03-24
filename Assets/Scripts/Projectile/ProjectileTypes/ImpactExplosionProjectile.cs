using UnityEngine;

public class ImpactExplosionProjectile : AbilityProjectile
{
    public override void Activate()
    {
        base.Activate();
    }

    void OnCollisionEnter(Collision collision)
    {
        Activate();
    }

    public override void Launch(Vector3 direction)
    {
    }
}
