// WorldSpaceUIManager.cs
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WorldSpaceUIManager : Singleton<WorldSpaceUIManager>
{
    [Header("Container")]
    [SerializeField] RectTransform WorldSpaceRoot;
    [Header("WorldSpacePrefabs & Pool Info")]
    [SerializeField] List<WorldSpaceUIFollower> worldSpaceUis;
    [SerializeField] bool collectioncheck = true;
    [SerializeField] int defaultUICapacity = 20;
    [SerializeField] int maxUICapacity = 50;
    readonly Dictionary<WorldSpaceUIFollower,ObjectPool<WorldSpaceUIFollower>> _worldSpacePools = new();
    Camera _camera;
    readonly List<WorldSpaceUIFollower> _followerList = new();
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    void Start()
    {
        InitializeDictionary();
    }

    public void RegisterFollower(WorldSpaceUIFollower follower)
    {
        if (!_followerList.Contains(follower))
            _followerList.Add(follower);
    }

    public void UnregisterFollower(WorldSpaceUIFollower follower)
    {
        _followerList.Remove(follower);
        
        if (_worldSpacePools.TryGetValue(follower.Prefab, out var pool))
            pool.Release(follower);
    }

    void LateUpdate()
    {
        for (int i = _followerList.Count - 1; i >= 0; i--)
        {
            _followerList[i].Tick(_camera);
        }
    }

    // public T SpawnUI<T>(T prefab, Transform target) where T : WorldSpaceUIFollower
    // {
    //     var ui = Instantiate(prefab, WorldSpaceRoot);
    //     ui.Initialize(target, _camera);
    //     RegisterFollower(ui);
    //     return ui;
    // }

    void InitializeDictionary()
    {
        foreach(var ui in worldSpaceUis)
        {
            if(_worldSpacePools.ContainsKey(ui)) continue;

            var pool = new ObjectPool<WorldSpaceUIFollower>(
                ()=> Instantiate(ui, WorldSpaceRoot), //create
                (obj) => obj.gameObject.SetActive(true), //OnGet
                (obj) => obj.gameObject.SetActive(false), //OnRelease
                (obj) => Destroy(obj.gameObject), //onDestroy
                collectionCheck: collectioncheck,
                defaultCapacity: defaultUICapacity,
                maxSize: maxUICapacity
            );

            _worldSpacePools[ui] = pool;

        }
    }

    public T Spawn<T>(T prefab, Transform target) where T: WorldSpaceUIFollower
    {
        if (_worldSpacePools.TryGetValue(prefab, out var pool))
        {
            var uiInstance = pool.Get();
            uiInstance.SetPrefab(prefab);
            uiInstance.Initialize(target, _camera);
            RegisterFollower(uiInstance);
            if (uiInstance is T typed)
                return typed;

            Debug.LogError($"Pool returned wrong type. Expected {typeof(T)}, got {uiInstance.GetType()}");
            return null;
        }

        Debug.LogError("Prefab has no pool");
        return null;
    }

    public void ReleaseToPools(WorldSpaceUIFollower prefab, WorldSpaceUIFollower instance)
    {
        if(_worldSpacePools.TryGetValue( prefab, out var pool))
        {
            pool.Release(instance);
        }
    }


}