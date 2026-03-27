using UnityEngine;

public class ImpactExplosionProjectile : AbilityProjectile
{

    void OnCollisionEnter(Collision collision)
    {
        Activate();
    }

}
