using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class WorldSpaceUIFollower : MonoBehaviour
{
    Transform _target;
    Camera _camera;

    [SerializeField] Vector3 _offset = new Vector3(0, 2f, 0);

    public void Initialize(Transform target, Camera camera)
    {
        _target = target;
        _camera = camera;
    }

    public void Tick(Camera cam)
    {
        if (_target == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(_target.position + _offset);
        transform.position = screenPos;
    }
}
