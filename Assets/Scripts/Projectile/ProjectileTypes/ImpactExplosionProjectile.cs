using UnityEngine;

public class ImpactExplosionProjectile : AbilityProjectile
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Activate();
        }
    }
}
