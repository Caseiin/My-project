using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUIManager : Singleton<WorldSpaceUIManager>
{
    [SerializeField] RectTransform WorldSpaceRoot;
    Camera _camera;
    readonly List<WorldSpaceUIFollower> _followerList = new();

    protected override void Awake()
    {
       base.Awake();
       _camera = Camera.main; 
    }
    public void RegisterFollower(WorldSpaceUIFollower follower)
    {
        _followerList.Add(follower);
    }
    public void UnregisterFollower(WorldSpaceUIFollower follower)
    {
        _followerList.Remove(follower);
        follower.CleanUp();
    }

    void LateUpdate()
    {
        foreach (var follower in _followerList)
        {
            follower.Tick(_camera);
        }
    }

    public T SpawnUI<T>(T prefab, Transform target) where T : WorldSpaceUIFollower
    {
        var ui = Instantiate(prefab, WorldSpaceRoot);
        ui.Initialize(target, _camera);
        RegisterFollower(ui); 
        return ui;
    }
}
