using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] Transform _initialPosition;
    
    readonly Dictionary<int,ObjectPool<AbilityProjectile>> _pools = new();

    protected override void Awake()
    {
        base.Awake();
    }

    public T GetProjectile<T>(T prefab) where T: AbilityProjectile
    {
        var pool = GetOrCreatePool(prefab);
        return (T) pool.Get();
    }

    public void ReturnProjectile(AbilityProjectile proj)
    {
        if(_pools.TryGetValue(proj.PrefabInstanceID, out var pool))
        {
            pool.Release(proj);
        }
        else 
            Destroy(proj.gameObject);
    }


    ObjectPool<AbilityProjectile> GetOrCreatePool(AbilityProjectile prefab)
    {
        int key = prefab.GetInstanceID();

        if (!_pools.TryGetValue(key, out var pool))
        {
            pool = new ObjectPool<AbilityProjectile>(
                createFunc:       () => CreateProjectile(prefab, key),
                actionOnGet:      OnGetFromPool,
                actionOnRelease:  OnReleaseToPool,
                actionOnDestroy:  OnDestroyPoolObject,
                collectionCheck:  false,
                defaultCapacity:  5,
                maxSize:          20
            );
            _pools[key] = pool;
        }

        return pool;
    }

    AbilityProjectile CreateProjectile(AbilityProjectile prefab, int prefabKey)
    {
        var instance = Instantiate(prefab, _initialPosition.position, Quaternion.identity);
        instance.PrefabInstanceID = prefabKey;   // so it can find its own pool later
        instance.gameObject.SetActive(false);

        if (instance.TryGetComponent<Rigidbody>(out var rb))
            rb.isKinematic = true;

        return instance;
    }

    void OnGetFromPool(AbilityProjectile projectile)
    {
        projectile.transform.position = _initialPosition.position;
        projectile.gameObject.SetActive(true);

        if (projectile.TryGetComponent<Rigidbody>(out var rb))
            rb.isKinematic = true;
    }

    void OnReleaseToPool(AbilityProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.ResetPhysics();
    }

    void OnDestroyPoolObject(AbilityProjectile projectile)
    {
        Destroy(projectile.gameObject);
    }



}
