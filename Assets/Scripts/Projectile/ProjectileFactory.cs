using UnityEngine;

public class ProjectileFactory<T> where T : AbilityProjectile, new()
{
    T _projectileInstance;

    T Create(AbilitySO ability)
    {
        _projectileInstance  = new();
        var projectile = Object.Instantiate(_projectileInstance);
        return projectile;
    }
}
