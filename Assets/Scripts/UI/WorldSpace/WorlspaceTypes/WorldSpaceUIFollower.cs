// WorldSpaceUIFollower.cs
using UnityEngine;

public abstract class WorldSpaceUIFollower : MonoBehaviour
{
    protected Transform _target;
    protected Camera _camera;

    [SerializeField] protected Vector3 _offset = new Vector3(0, 2f, 0);

    // Seal Initialize so subclasses cant break the contract
    public void Initialize(Transform target, Camera camera)
    {
        _target = target;
        _camera = camera;
        OnInitialize();
    }

    // Subclasses override this instead of Initialize
    protected virtual void OnInitialize() { }

    // Sealed tick — controls the flow, subclasses hook into it
    public void Tick(Camera cam)
    {
        if (_target == null) return;

        OnTick(); // subclass-specific logic (float, pulse, etc)
        UpdatePosition();
        FaceCamera(cam);
    }

    // Subclasses override this to modify _offset or do custom logic
    // WITHOUT having to rewrite position/camera boilerplate
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

    // Override to add custom cleanup, always call base
    public virtual void CleanUp()
    {
        SetVisible(false);
        Destroy(gameObject); // Default: destroy unless pooling
    }
}