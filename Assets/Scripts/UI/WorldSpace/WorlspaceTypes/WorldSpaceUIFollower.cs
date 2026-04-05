// WorldSpaceUIFollower.cs
using UnityEngine;

public abstract class WorldSpaceUIFollower : MonoBehaviour
{
    public WorldSpaceUIFollower Prefab{get; private set;}
    protected Transform _target;
    protected Camera _camera;

    [SerializeField] protected Vector3 _offset = new Vector3(0, 2f, 0);

    public void Initialize(Transform target, Camera camera)
    {
        _target = target;
        _camera = camera;
        OnInitialize();
    }

    public void SetPrefab(WorldSpaceUIFollower prefab)
    {
        Prefab = prefab;
    }

    protected virtual void OnInitialize() { }

    public void Tick(Camera cam)
    {
        if (_target == null) return;

        OnTick(); 
        UpdatePosition();
        FaceCamera(cam);
    }

    protected virtual void OnTick() { }

    protected virtual void UpdatePosition()
    {
        transform.position = _target.position + _offset;
    }

    protected virtual void FaceCamera(Camera cam)
    {
        transform.forward = cam.transform.forward;
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public virtual void CleanUp()
    {
        WorldSpaceUIManager.Instance.UnregisterFollower(this);
    }
}