using Unity.VisualScripting;
using UnityEngine;

public class BillBoarding : MonoBehaviour
{
    Camera _camera;
    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Quaternion rotation = _camera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation* Vector3.up);
    }
}
