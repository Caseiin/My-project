using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CooldownDisplayManager : MonoBehaviour
{
    [SerializeField] CooldownDisplayItem _itemPrefab;
    [SerializeField] Transform _container;
    [SerializeField] int _defaultCapacity = 4;
    [SerializeField] int _maxPoolSize = 8;

    IObjectPool<CooldownDisplayItem> _pool;
    readonly Dictionary<string, CooldownDisplayItem> _activeItems = new();

    void Awake()
    {
        _pool = new ObjectPool<CooldownDisplayItem>(
            createFunc:    CreateItem,
            actionOnGet:   OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolItem,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize:         _maxPoolSize
        );
    }

    void OnEnable()  => ProjectileThrow.OnCooldownActive += HandleCooldown;
    void OnDisable() => ProjectileThrow.OnCooldownActive -= HandleCooldown;

    void HandleCooldown(string label, float duration, Color color)
    {
        if (_activeItems.TryGetValue(label, out var existing))
        {
            existing.Restart(duration);
            return;
        }

        var item = _pool.Get();
        item.Activate(label, duration, OnItemComplete, color);
        _activeItems[label] = item;
    }

    void OnItemComplete(CooldownDisplayItem item)
    {
        _activeItems.Remove(item.Label);
        _pool.Release(item);
    }

    // --- Pool callbacks ---

    CooldownDisplayItem CreateItem()
    {
        var item = Instantiate(_itemPrefab, _container);
        item.gameObject.SetActive(false);
        return item;
    }

    void OnGetFromPool(CooldownDisplayItem item)
    {
        item.transform.SetParent(_container);
        item.gameObject.SetActive(true);
    }

    void OnReleaseToPool(CooldownDisplayItem item)
    {
        item.gameObject.SetActive(false);
    }

    void OnDestroyPoolItem(CooldownDisplayItem item)
    {
        Destroy(item.gameObject);
    }
}