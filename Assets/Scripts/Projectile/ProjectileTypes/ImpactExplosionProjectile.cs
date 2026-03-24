using UnityEngine;

public class ImpactExplosionProjectile : AbilityProjectile
{

    void OnCollisionEnter(Collision collision)
    {
        Activate();
    }

    public override void Launch(Vector3 direction)
    {
    }
}
