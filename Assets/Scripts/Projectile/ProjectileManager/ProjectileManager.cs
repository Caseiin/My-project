using UnityEngine;
using UnityEngine.Pool;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] Transform _initialPosition;
    [SerializeField] GameObject projectilePrefab;
    ObjectPool<AbilityProjectile> _pool;

    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<AbilityProjectile>(
            CreateProjectile,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 10
        );
    }


    AbilityProjectile CreateProjectile()
    {
        GameObject projectileObj = Instantiate(projectilePrefab,_initialPosition);
        projectileObj.SetActive(false);
        return projectileObj.GetComponent<AbilityProjectile>();
    }

    private void OnGetFromPool(AbilityProjectile abilityProjectile)
    {
        abilityProjectile.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(AbilityProjectile abilityProjectile)
    {
        abilityProjectile.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(AbilityProjectile abilityProjectile)
    {
        Destroy(abilityProjectile.gameObject);
    }
    public AbilityProjectile GetProjectile()
    {
        return _pool.Get();
    }

    public void ReturnProjectile(AbilityProjectile projectile)
    {
        _pool.Release(projectile);
    }
}
