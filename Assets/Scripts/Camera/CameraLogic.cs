using Unity.Cinemachine;
using UnityEngine;

public abstract class CameraLogic
{

    protected PlayerController _player;
    protected Vector2 _currentLook;
    protected Vector2 _lookVelocity;
    protected float _xRotation;
    public CameraLogic(PlayerController player)
    {
        _player = player;
    }

    public abstract void  HandleLook();
}
